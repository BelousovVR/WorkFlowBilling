using static System.String;
using WorkFlowBilling.Compiler.Signatures;
using System.Linq;

namespace WorkFlowBilling.Compiler.Impl.Signatures
{
    /// <summary>
    /// Базовый класс сигнатуры
    /// </summary>
    public abstract class SignatureBase : ISignature
    {
        /// <summary>
        /// Ключи сигнатуры
        /// </summary>
        public string[] Keys { get; protected set;}

        /// <summary>
        /// Вернуть информацию о совпадающей сигнатуре, если подстрока содержит символ/символы сигнатуры
        /// </summary>
        /// <param name="inputString">Подстрока, начинающаяся с символа сигнатуры</param>
        /// <returns></returns>
        public virtual SignatureMatchInfo Match(string inputString)
        {
            if (IsNullOrWhiteSpace(inputString))
                return new SignatureMatchInfo
                {
                    IsMatched = false,
                    MatchedKeyIndex = -1,
                };

            var trimedString = inputString.TrimStart(' ');
            var key = Keys.First();
            var isMatched = trimedString.StartsWith(key);

            return new SignatureMatchInfo
            {
                IsMatched = isMatched,
                MatchedKey = key,
                MatchedKeyIndex = 0,
                MatchedSignature = this
            };
        }
    }
}
