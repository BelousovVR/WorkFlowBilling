using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WorkFlowBilling.Common.Extensions;
using WorkFlowBilling.Common.Helpers;
using WorkFlowBilling.Compiler.Exceptions;
using WorkFlowBilling.Compiler.Functions;
using WorkFlowBilling.Compiler.Impl.Helpers;
using WorkFlowBilling.Compiler.Impl.Signatures.Operators;
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Signatures;
using static System.Char;
using static WorkFlowBilling.Compiler.Impl.Converters.PostfixConverterProccessHelper;
using static System.String;
using WorkFlowBilling.Compiler.Variables;

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
        private void AppendToOutput(string value)
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
                    AppendToOutput(stackStringValue);
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
                    AppendToOutput(stackTopStringValue);
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
                    AppendToOutput(stackStringValue);
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

            AppendToOutput(numberString);
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

            var matchedVariableSignature = _Signatures.OfType<IVariableSignature>()
                                                          .Select(_ => _.Match(variableStr))
                                                          .Where(_ => _.IsMatched)
                                                          .FirstOrDefault();

            if (matchedVariableSignature == null)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} неизвестная сигнатура переменной");

            AppendToOutput(variableStr);

            _LastProcessedType = ProcessedStringType.Variable;

            return variableStr.Length;
        }

        /// <summary>
        /// Обработать сигнатуру функции и получить ее длину
        /// </summary>
        /// <param name="chr">Символ начала сигнатуры функции</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        /// <param name="functionSignature">Сигнатура функции</param>
        private int ProcessFunctionAndGetLength(char chr, int charIndex, IFunctionSignature functionSignature)
        {
            var processAlowed = CheckFunctionProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var funcKey = functionSignature.Keys.First();

            _Stack.Push(new PostfixConverterStackValue
            {
                StringValue = funcKey,
                OriginalValue = functionSignature
            });

            _LastProcessedType = ProcessedStringType.Function;

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
        private void AppendOperatorsToOutput(IOperatorSignature operatorSignature, Func<int, int, bool> breakOperatorPriorityPredicate)
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

                AppendToOutput(stackStringValue);
                _Stack.Pop();
            }
        }

        /// <summary>
        /// Обработать оператор в зависимости от его ассоциативности
        /// </summary>
        /// <param name="operatorSignature">Сигнатура оператора</param>
        /// <param name="matchedKey">Ключ совпавшей сигнатуры</param>
        private void ProccessOperatorByAssociativity(IOperatorSignature operatorSignature, string matchedKey)
        {
            if (operatorSignature.Associativity == OperatorAssociativity.Right)
            {
                Func<int, int, bool> breakOperatorPriorityPredicate =
                    (operatorPriority, stackOperatorPriority) => operatorPriority >= stackOperatorPriority;

                AppendOperatorsToOutput(operatorSignature, breakOperatorPriorityPredicate);
            }
            else
            {
                Func<int, int, bool> breakOperatorPriorityPredicate =
                    (operatorPriority, stackOperatorPriority) => operatorPriority > stackOperatorPriority;

                AppendOperatorsToOutput(operatorSignature, breakOperatorPriorityPredicate);
            }

            _Stack.Push(new PostfixConverterStackValue
            {
                StringValue = matchedKey,
                OriginalValue = operatorSignature
            });
        }

        /// <summary>
        /// Обработать составной оператор
        /// </summary>
        /// <param name="operatorSignature">Сигнатура оператора</param>
        /// <param name="operatorKey">Совпавший ключ оператора</param>
        /// <param name="matchedIndex">Индекс совпавшего ключа оператора</param>
        private void ProcessCompositeOperator(IOperatorSignature operatorSignature, string operatorKey, int matchedIndex)
        {
            if (matchedIndex == 0)
            {
                ProccessOperatorByAssociativity(operatorSignature, operatorKey);
            }
            else
            {
                var isOperatorPartFinded = false;
                var operatorFirstPart = operatorSignature.Keys.First();

                while (_Stack.Count > 0)
                {
                    var stackValue = _Stack.Peek();

                    var stackOperatorSignature = stackValue.OriginalValue as IOperatorSignature;

                    if (stackOperatorSignature != null
                        && stackOperatorSignature.GetType() == operatorSignature.GetType()
                        && operatorFirstPart == stackValue.StringValue)
                    {
                        isOperatorPartFinded = true;
                        stackValue.StringValue = stackValue.StringValue + stackOperatorSignature.Keys[1];
                        break;
                    }
                    else
                    {
                        AppendToOutput(stackValue.StringValue);
                        _Stack.Pop();
                    }
                }

                if (!isOperatorPartFinded)
                    throw new StringConvertationException($"Не обнаружена составная часть оператора: {operatorFirstPart}");
            }
        }

        /// <summary>
        /// Обработать оператор и получить его длину
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="charIndex">Индекс символа в исходной строке</param>
        /// <param name="signatureMatchInfo">Информация о совпадении оператора</param>
        /// <returns></returns>
        private int ProcessOperatorAndGetLength(char chr, int charIndex, SignatureMatchInfo signatureMatchInfo)
        {
            var processAlowed = CheckOperatorProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var operatorSignature = signatureMatchInfo.MatchedSignature as IOperatorSignature;

            // если это оператор минус (число), то преобразовываем строку в (0 - число)
            if (operatorSignature is NegatationNumberOperator)
                AppendToOutput("0");

            var operatorKey = signatureMatchInfo.MatchedKey;

            if (operatorSignature.OperatorType.In(OperatorType.Unary, OperatorType.Binary))
            {
                ProccessOperatorByAssociativity(operatorSignature, operatorKey);
            }
            else
            {
                var matchedIndex = signatureMatchInfo.MatchedKeyIndex;
                ProcessCompositeOperator(operatorSignature, operatorKey, matchedIndex);
            }

            _LastProcessedType = ProcessedStringType.Operator;

            return operatorKey.Length;
        }

        /// <summary>
        /// Выбрать оператор на основании ожидаемого типа оператора
        /// </summary>
        /// <param name="operatorsSignatureMatchInfo">Список информации о совпадающих операторов</param>
        /// <returns></returns>
        private SignatureMatchInfo SelectOperatorMatchInfo(IEnumerable<SignatureMatchInfo> signatureMatchInfos)
        {
            if (signatureMatchInfos.Count() == 1)
                return signatureMatchInfos.First();

            if (_LastProcessedType.In(ProcessedStringType.Unknown,
                                      ProcessedStringType.LeftBracket,
                                      ProcessedStringType.FunctionArgumentDelitimer,
                                      ProcessedStringType.Operator))
            {
                return signatureMatchInfos
                        .Where(_ =>  (_.MatchedSignature as IOperatorSignature).OperatorType == OperatorType.Unary)
                        .OrderByDescending(_ => _.MatchedKey.Length)
                        .FirstOrDefault();
            }
            else
            {
                return signatureMatchInfos
                    .Where(_ => (_.MatchedSignature as IOperatorSignature).OperatorType != OperatorType.Unary)
                    .OrderByDescending(_ => _.MatchedKey.Length)
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
                                        .Select(_ =>  _.Match(stringForSearch))
                                        .Where(_ => _.IsMatched)
                                        .OrderByDescending(_ => _.MatchedKey.Length)
                                        .Select(_ => _.MatchedSignature as IFunctionSignature)
                                        .FirstOrDefault();
        }

        /// <summary>
        /// Получить подходящие сигнатуры операторов и информацию о совпадении
        /// </summary>
        /// <param name="stringForSearch">Подстрока оригинальной строки, содержащяя сигнатуру оператора или функции</param>
        /// <returns></returns>
        private List<SignatureMatchInfo> GetOperatorSignatureMatchInfos(string stringForSearch)
        {
            return _Signatures.OfType<IOperatorSignature>()
                                        .Select(_ => _.Match(stringForSearch))
                                        .Where(_ => _.IsMatched)
                                        .ToList();
        }

        /// <summary>
        /// Обработать оператор или функцию и вернуть длину ее сигнатуры
        /// </summary>
        /// <param name="chr">Символ начала сигнатуры оператора или функции</param>
        /// <param name="charIndex">Индекс символа в оригинальной строке</param>
        /// <param name="stringForSearch">Подстрока оригинальной строки, содержащяя сигнатуру оператора или функции</param>
        /// <returns></returns>
        private int ProcessOperatorOrFunctionAndGetLength(char chr, int charIndex)
        {
            var stringForSearch = _InputString.Substring(charIndex);
            int signatureLength = 0;

            var matchedFunctionSignature = GetMatchedFunctionSignature(stringForSearch);
            var matchedOperatorsInfos = GetOperatorSignatureMatchInfos(stringForSearch);

            if (matchedFunctionSignature != null)
            {
                signatureLength = ProcessFunctionAndGetLength(chr, charIndex, matchedFunctionSignature);
            }
            else if (matchedOperatorsInfos.Count != 0)
            {
                var operatorMatchInfo = SelectOperatorMatchInfo(matchedOperatorsInfos);
                signatureLength = ProcessOperatorAndGetLength(chr, charIndex, operatorMatchInfo);
            }
            else
            {
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} неизвестная сигнатура");
            }

            return signatureLength;
        }

        /// <summary>
        /// Добавить оставшиеся значения из стэка в выходную строку
        /// </summary>
        private void ProcessRemainingStackValues()
        {
            while (_Stack.Count > 0)
            {
                var stackValue = _Stack.Pop();

                var matchedSignature = stackValue.OriginalValue as ISignature;

                if (matchedSignature == null)
                    throw new StringConvertationException("В выражении несогласованны скобки");

                AppendToOutput(stackValue.StringValue);
            };
        }

        /// <summary>
        /// Преобразовать строку, содержащюю выражение в инфиксной форме в постфиксную форму 
        /// </summary>
        /// <returns></returns>
        public string Convert(string infixString)
        {
            if (IsNullOrWhiteSpace(infixString))
                throw new ArgumentNullException("Пустая входная строка");

            _Stack = new Stack<PostfixConverterStackValue>();
            _StringBuilder = new StringBuilder();
            _LastProcessedType = ProcessedStringType.Unknown;
            _InputString = infixString.Replace(" ", string.Empty);

            var index = 0;
            while (index < _InputString.Length)
            {
                //Читаем очередной символ
                var chr = _InputString[index];

                if (chr == TranslationConstantHelper.LeftBracket)
                {
                    ProcessLeftBracket(chr, index);
                    index++;
                }
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
                    var signatureLength = ProcessOperatorOrFunctionAndGetLength(chr, index);
                    index += signatureLength;
                }
            }

            ProcessRemainingStackValues();

            return GetResult();
        }

        public PostfixConverter(string delitimer, IEnumerable<ISignature> signatures)
        {
            if (IsNullOrEmpty(delitimer))
                throw new ArgumentNullException("Пустой разделитель");

            if (signatures.IsNullOrEmpty())
                throw new ArgumentNullException("Пустой список сигнатур");

            _Signatures = signatures;
            _Delitimer = delitimer;
        }
    }
}
 