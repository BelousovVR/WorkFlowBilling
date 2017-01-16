using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowBilling.Common.Extensions;
using WorkFlowBilling.Common.Helpers;
using WorkFlowBilling.Compiler.Exceptions;
using WorkFlowBilling.Compiler.Functions;
using WorkFlowBilling.Compiler.Impl.Helpers;
using WorkFlowBilling.Compiler.Impl.Operators;
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Signatures;
using static System.Char;
using static WorkFlowBilling.Compiler.Impl.Converters.PostfixConverterProccessHelper;

namespace WorkFlowBilling.Compiler.Impl.Converters
{
    /// <summary>
    /// Вспомогательный класс для конвертации строки, содержащей выражение в постфиксную запись
    /// </summary>
    public class PostfixConverter
    {
        private string _InputString;

        private Stack<PostfixConverterStackValue> _Stack;

        private string _Delitimer;

        private IEnumerable<ISignature> _Signatures;

        private StringBuilder _StringBuilder;

        private ProcessedStringType _LastProcessedType;

        private CultureInfo _CultureInfo = CultureInfoHelper.English_USA_Culture;

        /// <summary>
        /// Добавить строковое значение к выходной строке
        /// </summary>
        /// <param name="value"></param>
        private void AppendToOutputStringBuilder(string value)
        {
            _StringBuilder.Append($"{value}{_Delitimer}");
        }

        /// <summary>
        /// Получить результат конвертации в чистом виде
        /// </summary>
        private string GetResult()
        {
            return _StringBuilder.ToString().Trim();
        }

        /// <summary>
        /// Обработать левую скобку 
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        private void ProcessLeftBracket(char chr, int charIndex)
        {
            var processAlowed = CheckLeftBracketProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            _Stack.Push(new PostfixConverterStackValue
            {
                StringValue = chr.ToString(),
                OriginalValue = chr
            });

            _LastProcessedType = ProcessedStringType.LeftBracket;
        }

        /// <summary>
        /// Обработать правую скобку
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        private void ProcessRightBracket(char chr, int charIndex)
        {
            var processAlowed = CheckRightBracketProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            if (_Stack.Count == 0)
                throw new InvalidOperationException("Ошибка при получении выражения между скобками");

            var isLeftBracketFinded = false;

            while (_Stack.Count > 0)
            {
                var stackStringValue = _Stack.Pop().StringValue;

                if (stackStringValue == TranslationConstantHelper.LeftBracketString)
                {
                    isLeftBracketFinded = true;
                    break;
                }
                else
                    AppendToOutputStringBuilder(stackStringValue);
            }

            if (!isLeftBracketFinded)
                throw new StringConvertationException($"В строке {_InputString} обнаружены несогласованные скобки");

            //Если после этого шага на вершине стека оказывается символ функции, выталкиваем его в выходную строку
            if (_Stack.Count > 0)
            {
                var stackTopStringValue = _Stack.Peek().StringValue;
                var matchedFunction = GetMatchedFunctionSignature(stackTopStringValue);
                if (matchedFunction != null)
                {
                    _Stack.Pop();
                    AppendToOutputStringBuilder(stackTopStringValue);
                }
            }

            _LastProcessedType = ProcessedStringType.RightBracket;
        }

        /// <summary>
        /// Обработать разделитель аргументов функции
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        private void ProcessArgumentDelitimer(char chr, int charIndex)
        {
            var processAlowed = CheckFunctionArgumentDelitimerProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var isLeftBracketFinded = false;
            while (_Stack.Count > 0)
            {
                var stackStringValue = _Stack.Peek().StringValue;

                if (stackStringValue == TranslationConstantHelper.LeftBracketString)
                {
                    isLeftBracketFinded = true;
                    break;
                }
                else
                {
                    AppendToOutputStringBuilder(stackStringValue);
                    _Stack.Pop();
                }
            }

            if (!isLeftBracketFinded)
                throw new StringConvertationException($"В строке {_InputString} обнаружены несогласованные скобки");

            _LastProcessedType = ProcessedStringType.FunctionArgumentDelitimer;
        }

        /// <summary>
        /// Обработать число и получить его строковую длину
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        private int ProcessNumberAndGetLength(char chr, int charIndex)
        {
            var processAlowed = CheckNumberProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var substr = _InputString.Substring(charIndex);

            decimal number;
            var isCorrectNumber = substr.TryGetDecimal(out number, _CultureInfo);

            if (!isCorrectNumber)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} некорректный формат числа");

            var numberString = number.ToString(_CultureInfo);

            AppendToOutputStringBuilder(numberString);
            _LastProcessedType = ProcessedStringType.Number;

            return numberString.Length;
        }

        /// <summary>
        /// Обработать сигнатуру переменной и вернуть ее длину
        /// </summary>
        /// <param name="chr">Символ начала переменной</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        /// <returns></returns>
        private int ProcessVariableAndGetLength(char chr, int charIndex)
        {
            var processAlowed = CheckVariableProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var variableEndIndex = _InputString.IndexOf(TranslationConstantHelper.VariableEnd, charIndex + 1);

            if (variableEndIndex == -1)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} ожидается закрывающий символ переменной");

            var variableStr = _InputString.Substring(charIndex, variableEndIndex + 1);

            AppendToOutputStringBuilder(variableStr);

            _LastProcessedType = ProcessedStringType.Variable;

            return variableStr.Length;
        }

        /// <summary>
        /// Обработать сигнатуру функции и получить ее длину
        /// </summary>
        /// <param name="chr">Символ начала сигнатуры функции</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        private int ProcessFunctionAndGetLength(char chr, int charIndex, ISignature function)
        {
            var processAlowed = CheckFunctionProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var funcKey = function.Keys.First();

            AppendToOutputStringBuilder(funcKey);

            return funcKey.Length;
        }

        /// <summary>
        /// Получить приоритет оператора из значения стэка
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetOperatorPriorityByStackValue(PostfixConverterStackValue stackValue)
        {
            var opr = stackValue.OriginalValue as IOperatorSignature;

            if (opr == null)
                throw new StringConvertationException(
                    $"При конвертации {_InputString} произошла ошибка стэка - ожидался оператор, а обнаружен {stackValue.StringValue}");

            return opr.Priority;
        }

        /// <summary>
        /// Обработать оператор с использованием стэка
        /// </summary>
        /// <param name="operatorSignature">Сигнатура оператора</param>
        /// <param name="breakOperatorPriorityPredicate">Функция сравнения приоритетов операторов, 
        /// при которой выталкивание из стэка должно завершиться
        /// </param>
        private void ProcessOperatorWithStack(IOperatorSignature operatorSignature, Func<int, int, bool> breakOperatorPriorityPredicate)
        {
            while (_Stack.Count > 0)
            {
                var stackValue = _Stack.Peek();
                var stackStringValue = stackValue.StringValue;

                // если разделитель - прерываем выталкивание
                if (stackStringValue.In(TranslationConstantHelper.LeftBracketString,
                    TranslationConstantHelper.FunctionArgumentDelitimer))
                    break;

                var stackOperatorPriority = GetOperatorPriorityByStackValue(stackValue);

                if (breakOperatorPriorityPredicate(operatorSignature.Priority, stackOperatorPriority))
                    break;

                AppendToOutputStringBuilder(stackStringValue);
                _Stack.Pop();
            }
        }

        /// <summary>
        /// Обработать оператор и получить его длину
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        private int ProcessOperatorAndGetLength(char chr, int charIndex, IOperatorSignature operatorSignature)
        {
            var processAlowed = CheckOperatorProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            // если это оператор минус (число), то преобразовываем строку в (0 - число)
            if (operatorSignature is NegatationNumberOperator)
                AppendToOutputStringBuilder("0");

            if (operatorSignature.Associativity == OperatorAssociativity.Right)
            {
                Func<int, int, bool> breakOperatorPriorityPredicate = 
                    (operatorPriority, stackOperatorPriority) => operatorPriority >= stackOperatorPriority;

                ProcessOperatorWithStack(operatorSignature, breakOperatorPriorityPredicate);
            }
            else
            {
                Func<int, int, bool> breakOperatorPriorityPredicate = 
                    (operatorPriority, stackOperatorPriority) => operatorPriority > stackOperatorPriority;

                ProcessOperatorWithStack(operatorSignature, breakOperatorPriorityPredicate);
            }
              
            var operatorKey= operatorSignature.Keys.First();

            _Stack.Push(new PostfixConverterStackValue
            {
                StringValue = operatorKey,
                OriginalValue = operatorSignature
            });

            return operatorKey.Length;
        }

        /// <summary>
        /// Выбрать оператор на основании ожидаемого типа оператора
        /// </summary>
        /// <param name="operators">Список операторов</param>
        /// <returns></returns>
        private IOperatorSignature SelectOperator(IEnumerable<IOperatorSignature> operators)
        {
            if (operators.Count() == 1)
                return operators.First();

            if (_LastProcessedType.In(ProcessedStringType.Unknown,
                                      ProcessedStringType.LeftBracket,
                                      ProcessedStringType.FunctionArgumentDelitimer,
                                      ProcessedStringType.Operator))
            {
                return operators
                        .Where(_ => _.OperatorType == OperatorType.Unary)
                        .OrderByDescending(_ => _.Keys.First().Length)
                        .FirstOrDefault();
            } else
            {
                return operators
                        .Where(_ => _.OperatorType != OperatorType.Unary)
                        .OrderByDescending(_ => _.Keys.First().Length)
                        .FirstOrDefault();
            }
        }

        /// <summary>
        /// Получить подходящую сигнатуру функции
        /// </summary>
        /// <param name="stringForSearch">Подстрока оригинальной строки, содержащяя сигнатуру оператора или функции</param>
        /// <returns></returns>
        private IFunctionSignature GetMatchedFunctionSignature(string stringForSearch)
        {
            return _Signatures.OfType<IFunctionSignature>()
                                        .Where(_ => _.Match(stringForSearch))
                                        .OrderByDescending(_ => _.Keys.First().Length)
                                        .FirstOrDefault();
        }

        /// <summary>
        /// Получить подходящие сигнатуры операторов
        /// </summary>
        /// <param name="stringForSearch">Подстрока оригинальной строки, содержащяя сигнатуру оператора или функции</param>
        /// <returns></returns>
        private List<IOperatorSignature> GetMatchedOperatorSignatures(string stringForSearch)
        {
            return _Signatures.OfType<IOperatorSignature>()
                                        .Where(_ => _.Match(stringForSearch))
                                        .ToList();
        }

        /// <summary>
        /// Обработать оператор или функцию и вернуть длину ее сигнатуры
        /// </summary>
        /// <param name="chr">Символ начала сигнатуры оператора или функции</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        /// <param name="stringForSearch">Подстрока оригинальной строки, содержащяя сигнатуру оператора или функции</param>
        /// <returns></returns>
        private int ProcessOperatorOrFunction(char chr, int charIndex)
        {
            var stringForSearch = _InputString.Substring(charIndex);
            int processedLength = 0;

            var matchedFunction = GetMatchedFunctionSignature(stringForSearch);

            var matchedOperators = GetMatchedOperatorSignatures(stringForSearch);

            if (matchedFunction != null)
            {
                processedLength = ProcessFunctionAndGetLength(chr, charIndex, matchedFunction);
                _LastProcessedType = ProcessedStringType.Function;
            }
            else if (matchedOperators.Count != 0)
            {
                var operatorSignature = SelectOperator(matchedOperators);

                if (operatorSignature == null)
                    throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} не удалось определить оператор");

                processedLength = ProcessOperatorAndGetLength(chr, charIndex, operatorSignature);
                _LastProcessedType = ProcessedStringType.Operator;
            }
            else
            {
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} неизвестная сигнатура");
            }

            return processedLength;
        }

        /// <summary>
        /// Преобразовать строку, содержащюю выражение в инфиксной форме в постфиксную форму 
        /// </summary>
        /// <returns></returns>
        public string Convert()
        {
            _Stack = new Stack<PostfixConverterStackValue>();
            _StringBuilder = new StringBuilder();
            _LastProcessedType = ProcessedStringType.Unknown;

            var index = 0;
            while (index < _InputString.Length)
            {
                //Читаем очередной символ
                var chr = _InputString[index];

                //Если символ является открывающей скобкой, помещаем его в стек.
                if (chr == TranslationConstantHelper.LeftBracket)
                {
                    ProcessLeftBracket(chr, index);
                    index++;
                }
                // TODO: проверить, что кидается исключение при несогласованных скобках
                else if (chr == TranslationConstantHelper.RightBracket)
                {
                    ProcessRightBracket(chr, index);
                    index++;
                }
                else if (chr == TranslationConstantHelper.FunctionArgumentDelitimer)
                {
                    ProcessArgumentDelitimer(chr, index);
                    index++;
                }
                else if (IsDigit(chr))
                {
                    int numberStringLength = ProcessNumberAndGetLength(chr, index);
                    index += numberStringLength;
                }
                else if (chr == TranslationConstantHelper.VariableStart)
                {
                    int numberStringLength = ProcessVariableAndGetLength(chr, index);
                    index += numberStringLength;
                    
                }
                else
                {
                    var signatureLength = ProcessOperatorOrFunction(chr, index);
                    index += signatureLength;
                }
            }

            //когда входная строка закончилась, выталкиваем все символы из стека в выходную строку.
            //В стеке должны были остаться только символы операторов; если это не так, значит в выражении не согласованы скобки.

            // TODO: проверить, есть ли там не операторы (подумать, что функциями)
            while (_Stack.Count > 0)
                AppendToOutputStringBuilder(_Stack.Pop().StringValue);
     
            return GetResult();
        }

        public PostfixConverter(string inputString, string delitimer, IEnumerable<ISignature> signatures)
        {
            _InputString = inputString.Replace(" ", string.Empty);
            _Signatures = signatures;
            _Delitimer = delitimer;
        }
    }
}