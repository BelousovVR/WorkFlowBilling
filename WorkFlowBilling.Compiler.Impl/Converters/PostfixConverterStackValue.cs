namespace WorkFlowBilling.Compiler.Impl.Converters
{
    /// <summary>
    /// Значение в стэке при конвертации из инфиксной нотации в постфиксную
    /// </summary>
    public class PostfixConverterStackValue
    {
        /// <summary>
        /// Строковое представление значения в стэке
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Оригинальное значение, помещенные в стэк
        /// </summary>
        public object OriginalValue { get; set; }
    }
}
