namespace WorkflowBilling.Translator.FunctionalTests.Common
{
    /// <summary>
    /// Класс для тестирования конвертации строк с выражениями
    /// </summary>
    public class StringConvertTestData
    {
        /// <summary>
        /// Входная строка
        /// </summary>
        public string InputString { get; set; }

        /// <summary>
        /// Выходная строка
        /// </summary>
        public string OutputString { get; set; }

        public StringConvertTestData(string input, string output)
        {
            InputString = input;
            OutputString = output;
        }
    }
}
