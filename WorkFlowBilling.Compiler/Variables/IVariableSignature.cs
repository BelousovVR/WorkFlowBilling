using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Variables
{
    /// <summary>
    /// Сигнатура переменной
    /// </summary>
    [AutoInjectable]
    public interface IVariableSignature : ISignature
    {
    }
}
