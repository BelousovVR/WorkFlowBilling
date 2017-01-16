using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Compiler.Impl.Converters
{
    /// <summary>
    /// Хелпер для проверок корректной обработки элементов
    /// </summary>
    public static class PostfixConverterProccessHelper
    {
        /// <summary>
        /// Проверить, может ли мы обработать левую скобку
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        public static bool CheckLeftBracketProcessAllowed(ProcessedStringType lastProcessedType)
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
        public static bool CheckRightBracketProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.FunctionArgumentDelitimer,
                                           ProcessedStringType.Function);
        }

        /// <summary>
        /// Проверить, может ли мы обработать число
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        public static bool CheckNumberProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }

        /// <summary>
        /// Проверить, может ли мы обработать переменную
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        public static bool CheckVariableProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }

        /// <summary>
        /// Проверить, может ли мы обработать функцию
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        public static bool CheckFunctionProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.RightBracket,
                                           ProcessedStringType.Function,
                                           ProcessedStringType.Variable);
        }

        /// <summary>
        /// Проверить, может ли мы обработать оператор
        /// </summary>
        /// <param name="lastProcessedType">Последний обработанный синтаксический тип</param>
        /// <returns></returns>
        public static bool CheckOperatorProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.Function);
        }

        /// <summary>
        /// Проверить, может ли мы обработать разделитель аргументов функции
        /// </summary>
        /// <param name="lastProcessedType"></param>
        /// <returns></returns>
        public static bool CheckFunctionArgumentDelitimerProcessAllowed(ProcessedStringType lastProcessedType)
        {
            return lastProcessedType.NotIn(ProcessedStringType.LeftBracket,
                                            ProcessedStringType.Operator,
                                            ProcessedStringType.Unknown);
        }
    }
}
