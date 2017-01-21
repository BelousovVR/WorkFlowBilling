using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Functions
{
    /// <summary>
    /// Интерфейс сигнатуры функции
    /// </summary>
    [AutoInjectable]
    public interface IFunctionSignature : ISignature
    {
    }
}
