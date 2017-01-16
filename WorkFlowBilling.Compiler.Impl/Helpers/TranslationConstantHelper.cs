namespace WorkFlowBilling.Compiler.Impl.Helpers
{
    /// <summary>
    /// Константы трансляции
    /// </summary>
    public static class TranslationConstantHelper
    {
        /// <summary>
        /// Разделитель аргументов функции
        /// </summary>
        public static char FunctionArgumentDelitimer = ',';

        /// <summary>
        /// Открывающая скобка
        /// </summary>
        public static char LeftBracket = '(';

        /// <summary>
        /// Закрывающая скобка
        /// </summary>
        public static char RightBracket = ')';

        /// <summary>
        /// Начало переменной
        /// </summary>
        public static char VariableStart= '{';

        /// <summary>
        /// Окончание переменной
        /// </summary>
        public static char VariableEnd = '}';

        /// <summary>
        /// Строковое представление открывающей скобки
        /// </summary>
        public static string LeftBracketString = LeftBracket.ToString();

        /// <summary>
        /// Строковое представление закрывающей скобки
        /// </summary>
        public static string RightBracketString = RightBracket.ToString();

        /// <summary>
        /// Строковое представление разделителя аргументов функции
        /// </summary>
        public static string FunctionDelitimerString = FunctionArgumentDelitimer.ToString();

        /// <summary>
        /// Разделитель элементов в выходной строке, полученной в ходе преобразования постфиксной/инфиксной записи
        /// </summary>
        public static string ElementsDelitimer = " ";
    }
}
