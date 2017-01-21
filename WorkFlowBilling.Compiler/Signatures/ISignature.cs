using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Signatures
{
    /// <summary>
    /// Интерфейс сигнатуры
    /// </summary>
    [AutoInjectable] 
    public interface ISignature
    {
        /// <summary>
        /// Вернуть информацию о совпадении сигнатуры, если подстрока содержит символ/символы сигнатуры
        /// </summary>
        /// <param name="inputString">Подстрока, начинающаяся с символа оператора</param>
        /// <returns></returns>
        SignatureMatchInfo Match(string inputString);

        /// <summary>
        /// Ключи сигнатуры
        /// </summary>
        string[] Keys { get; }
    }
}
