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
        /// Тип сигнатуры
        /// </summary>
        SignatureType SignatureType { get; }

        /// <summary>
        /// Вернуть истину, если подстрока содержит символ/символы сигнатуры
        /// </summary>
        /// <param name="inputString">Подстрока, начинающаяся с символа оператора</param>
        /// <returns></returns>
        bool Match(string inputString);

        /// <summary>
        /// Приоритет
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Символы сигнатуры
        /// </summary>
        string[] Keys { get; }
    }
}
