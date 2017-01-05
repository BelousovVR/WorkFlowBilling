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
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Signatures;
using static System.Char;

namespace WorkFlowBilling.Compiler.Impl.Converters
{
    /// <summary>
    /// Вспомогательный класс для конвертации строки, содержащей выражение в постфиксную запись
    /// </summary>
    public class PostfixConverter
    {
        private string _InputString;

        private Stack<string> _Stack;

        private string _Delitimer;

        private IEnumerable<ISignature> _Signatures;

        private StringBuilder _StringBuilder;

        private ProcessedStringType _LastProcessedType;

        private CultureInfo _CultureInfo = CultureInfoHelper.English_USA_Culture;

        // TODO: тоже в класс потом вынести

        /// <summary>
        /// Проверить, можем ли мы обработать левую скобку
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        private bool CheckLeftBracketProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.In(ProcessedStringType.LeftBracket, 
                                        ProcessedStringType.Function,
                                        ProcessedStringType.Operator,
                                        ProcessedStringType.Unknown);
        }

        /// <summary>
        /// Проверить, можем ли мы обработать правую скобку
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        private bool CheckRightBracketProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.FunctionDelitimer,
                                           ProcessedStringType.Function);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastProcessedType"></param>
        /// <returns></returns>
        private bool CheckNumberProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastProcessedType"></param>
        /// <returns></returns>
        private bool CheckVariableProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastProcessedType"></param>
        /// <returns></returns>
        private bool CheckFunctionProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastProcessedType"></param>
        /// <returns></returns>
        private bool CheckOperatorProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.Function);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void Append(string value)
        {
            _StringBuilder.Append($"{value}{_Delitimer}");
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetResult()
        {
            return _StringBuilder.ToString().Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
        private void ProcessLeftBracket(char chr, int charIndex)
        {
            var processAlowed = CheckLeftBracketProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            _Stack.Push(chr.ToString());

            _LastProcessedType = ProcessedStringType.LeftBracket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
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
                var stackValue = _Stack.Pop();

                if (stackValue == TranslationConstantHelper.LeftBracketString)
                {
                    isLeftBracketFinded = true;
                    break;
                }
                else
                    Append(stackValue);
            }

            if (!isLeftBracketFinded)
                throw new StringConvertationException($"В строке {_InputString} обнаружены несогласованные скобки");

            _LastProcessedType = ProcessedStringType.RightBracket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
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

            Append(numberString);
            _LastProcessedType = ProcessedStringType.Number;

            return numberString.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
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

            Append(variableStr);

            _LastProcessedType = ProcessedStringType.Variable;

            return variableStr.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
        private int ProcessFunctionAndGetLength(char chr, int charIndex, ISignature function)
        {
            var processAlowed = CheckFunctionProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            var funcKey = function.Keys.First();

            Append(funcKey);

            return funcKey.Length;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetOperatorPriorityByKey(string key)
        {
            var opr = _Signatures.OfType<IOperatorSignature>().Where(_ => _.Keys.First() == key).FirstOrDefault();

            if (opr == null)
                throw new StringConvertationException($"При конвертации {_InputString} произошла ошибка стэка - нет оператора с ключом {key}");

            return opr.Priority;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        private int ProcessOperatorAndGetLength(char chr, int charIndex, IOperatorSignature operatorValue)
        {
            var processAlowed = CheckOperatorProcessAllowed(_LastProcessedType);
            if (!processAlowed)
                throw new StringConvertationException($"В строке {_InputString} символ: {charIndex} обнаружен неожиданный символ '{chr}'");

            //Если текущий оператор - левоассоциативный, то выталкиваем все операторы из стека, пока не найдется оператор,
            //приоритет которого меньше, чем у текущего.
            //Если текущий оператор - правоассоциативный, то выталкиваем все операторы из стека, пока не найдется оператор,
            //приоритет которого либо меньше, либо равен текущему.
            //После этого добавляем текущий оператор в стек.
            //Процесс выталкивания может выполняться, пока не будет достигнут разделитель (скобка, запятая), неполная
            //сигнатура оператора или дно стека

            /*
                Если символ является оператором о1, тогда:
                         1) пока…
                         … (если оператор o1 право - ассоциированный) приоритет o1 меньше приоритета оператора, находящегося на вершине стека…
                         … (если оператор o1 ассоциированный, либо лево-ассоциированный) приоритет o1 меньше либо равен приоритету оператора, находящегося на вершине стека…
                           1.1 … выталкиваем верхний элемент стека в выходную строку;
                         2) помещаем оператор o1 в стек.
                    
                        */


            if (operatorValue.Associativity == OperatorAssociativity.Right)
            {
                // TODO: Тут надо юзать Is NegatationNumberOperator 
                if (operatorValue.Keys.First() == "-")
                    Append("0");

                //TODO: в метод
                while (_Stack.Count > 0)
                {
                    var stackValue = _Stack.Peek();

                    // если разделитель - прерываем выталкивание
                    if (stackValue.In(TranslationConstantHelper.LeftBracketString,
                        TranslationConstantHelper.DelitimerString))
                        break;

                    var stackOperatorPriority = GetOperatorPriorityByKey(stackValue);
                    
                    if (operatorValue.Priority >= stackOperatorPriority)
                        break;

                    Append(stackValue);
                    _Stack.Pop();
                }
            }
            else
            {
                //TODO: в метод
                while (_Stack.Count > 0)
                {
                    var stackValue = _Stack.Peek();

                    // если разделитель - прерываем выталкивание
                    if (stackValue.In(TranslationConstantHelper.LeftBracketString,
                        TranslationConstantHelper.DelitimerString))
                        break;

                    var stackOperatorPriority = GetOperatorPriorityByKey(stackValue);

                    if (operatorValue.Priority > stackOperatorPriority)
                        break;

                    Append(stackValue);
                    _Stack.Pop();
                }
            }

            var operatorKey= operatorValue.Keys.First();

            _Stack.Push(operatorKey);

            return operatorKey.Length;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="operators"></param>
        /// <returns></returns>
        private IOperatorSignature SelectOperator(IEnumerable<IOperatorSignature> operators)
        {
            if (operators.Count() == 1)
                return operators.First();


            // TODO: здесь нужен анализ для типа -3 - -3, так как он воспринимает - - как --
            // Видимо следует сначала определить, какой оператор мы потенциально ожидаем
            // Если до этого ProcessedStringType.Unknown / ProcessedStringType.LeftBracket / ProcessedStringType.FunctionDelitimer / ProcessedStringType.Operator
            // То будем смотреть только унарные операторы ( и унарный операторы надо преобразовывать в какую- то хуйню, которую )
            
            if (_LastProcessedType.In(ProcessedStringType.Unknown,
                                      ProcessedStringType.LeftBracket,
                                      ProcessedStringType.FunctionDelitimer,
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


            /*TODO ТУТ ПЕРВОНАЧАЛЬНАЯ ВЕРСИЯ
            // Выбираем операторы с максимально длинной сигнатурой
            var matchedOperatorsWithMaxKey = operators.GroupBy(_ => _.Keys.First())
                                                .OrderByDescending(_ => _.Key.Length)
                                                .First()
                                                .Select(_ => _)
                                                .ToList();
 
            if (matchedOperatorsWithMaxKey.Count == 1)
                return matchedOperatorsWithMaxKey.First();

            // Если есть несколько операторов с одной сигнатурой (например бинарный "-" и унарный "-") 
            // тогда выбираем в зависимости от предыдущего обработанного типа
            if (_LastProcessedType.In(ProcessedStringType.Unknown,
                                      ProcessedStringType.LeftBracket,
                                      ProcessedStringType.FunctionDelitimer,
                                      ProcessedStringType.Operator))
                return matchedOperatorsWithMaxKey.Where(_ => _.OperatorType == OperatorType.Unary).FirstOrDefault();
            else
                return matchedOperatorsWithMaxKey.Where(_ => _.OperatorType != OperatorType.Unary).FirstOrDefault();
            */
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="chr"></param>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        private int ProcessOperatorOrFunction(char chr, int charIndex, string stringForSearch)
        {
            int processedLength = 0;

            // ФУНКЦИЯ
            //Если символ является символом функции, помещаем его в стек (тут сравниваем с сигнатурой).
            // выбираем наиболее подходящую функцию
            var matchedFunction = _Signatures.OfType<IFunctionSignature>()
                                        .Where(_ => _.Match(stringForSearch))
                                        .OrderByDescending(_ => _.Keys.First().Length)
                                        .FirstOrDefault();

            var matchedOperators = _Signatures.OfType<IOperatorSignature>()
                                        .Where(_ =>_.Match(stringForSearch))
                                        .ToList();

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
        /// 
        /// </summary>
        /// <returns></returns>
        public string Convert()
        {
            _Stack = new Stack<string>();
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
                //Если символ - закрывающая скобка: До тех пор, пока верхним элементом стека не станет открывающая скобка,
                //выталкиваем элементы из стека в выходную строку. При этом открывающая скобка удаляется из стека, но в выходную строку не добавляется. 
                //Если стек закончился раньше, чем мы встретили открывающую скобку, это означает, 
                //что в выражении либо неверно поставлен разделитель, либо не согласованы скобки.
                else if (chr == TranslationConstantHelper.RightBracket)
                {
                    ProcessRightBracket(chr, index);
                    index++;
                }
                // TODO: надо добавить еще обработку запятых (разделителей в функции)

                //Если символ является числом, добавляем его к выходной строке до тех пор, пока не встретим что-то, что не является числом
                else if (IsDigit(chr))
                {
                    int numberStringLength = ProcessNumberAndGetLength(chr, index);
                    index += numberStringLength;
                }
                // ПЕРЕМЕННАЯ
                // Если символ является признаком начала переменной - добавляем ее к выходной строке
                else if (chr == TranslationConstantHelper.VariableStart)
                {
                    int numberStringLength = ProcessVariableAndGetLength(chr, index);
                    index += numberStringLength;
                    
                }
                else
                {
                    var leftTrimedString = _InputString.Substring(index);
                    var signatureLength = ProcessOperatorOrFunction(chr, index, leftTrimedString);
                    index += signatureLength;
                }
            }

            //когда входная строка закончилась, выталкиваем все символы из стека в выходную строку.В стеке должны были остаться только символы операторов; если это не так, значит в выражении не согласованы скобки.
            while (_Stack.Count > 0)
                Append(_Stack.Pop());
     
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



/*
 * Если элемент является константой или переменной, добавляем его к результирующей строке.

  4. Если элемент является разделителем аргументов функции (запятая), то выталкиваем элементы из стека
     в выходную строку до тех пор, пока верхним элементом стека не станет открывающаяся скобка.
     Если открывающаяся скобка не встретилась, это означает, что в выражении либо неверно поставлен
     разделитель, либо несогласованы скобки.
  5. Если элемент является одной из зарегистрированных сигнатур[*], то:
     - Если элемент соответствует сигнатуре унарного оператора и этот элемент - первый после начала выражения
       (либо начало входной последовательности, либо сразу после открывающей скобки), мы интерпретируем его как
       унарный оператор и заносим в стек в соответствии с его приоритетом и ассоциативностью[**]. Далее
       осуществляется незамедлительный переход к обработке следующего эл-та (continue).
     - Если элемент соответствует сигнатуре бинарного оператора, то заносим его в стек в соответствии с его
       приоритетом и ассоциативностью. Далее осуществляется незамедлительный переход к обработке следующего эл-та (continue).
     - Если элемент соответствует сигнатуре одной из составных частей оператора, принимающего 3 или более
       операндов, то определяем номер этой составной части (для двоеточия в тернарном операторе ?: это будет
       номер 1, для знака вопроса - номер 2). Если номер равен 1, то заносим его в стек в соответствии с его приоритетом
       и ассоциативностью[**]. Если номер больше 1, то выталкиваем в результирующее выражение все элементы,
       пока не найдем составную часть с предыдущим номером.
       Если оказывается, что обработанный таким способом элемент замыкает сигнатуру оператора, то соединяем в стеке
       его части в один логический оператор. Если предыдущий не найден - синтаксическая ошибка.
*/