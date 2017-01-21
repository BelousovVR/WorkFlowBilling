using WorkFlowBilling.Common.Interfaces;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Signatures
{
    /// <summary>
    /// Интерфейс сигнатуры
    /// </summary>
    [AutoInjectable] 
    public interface ISignature : IInputOutputGenericMatchable<string, SignatureMatchInfo>
    {
        /// <summary>
        /// Ключи сигнатуры
        /// </summary>
        string[] Keys { get; }
    }
}
