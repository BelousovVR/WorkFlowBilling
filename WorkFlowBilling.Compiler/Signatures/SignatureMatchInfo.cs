namespace WorkFlowBilling.Compiler.Signatures
{
    /// <summary>
    /// Информация о совпадающей сигнатуре
    /// </summary>
    public class SignatureMatchInfo
    {
        /// <summary>
        /// Сигнатура совпадает
        /// </summary>
        public bool IsMatched { get; set; }

        /// <summary>
        /// Совпавший ключ сигнатуры
        /// </summary>
        public string MatchedKey { get; set; }

        /// <summary>
        /// Индекс совпадающего ключа сигнатуры в массиве ключей
        /// </summary>
        public int MatchedKeyIndex { get; set; }

        /// <summary>
        /// Ссылка на совпавшую сигнатуру
        /// </summary>
        public ISignature MatchedSignature { get; set; }
    }
}
