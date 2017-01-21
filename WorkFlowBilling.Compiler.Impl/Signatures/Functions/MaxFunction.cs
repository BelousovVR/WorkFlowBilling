using WorkFlowBilling.Compiler.Functions;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Functions
{
    /// <summary>
    /// Функция, определяющая максимальное значение из двух значений
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class MaxFunction : FunctionBase, IFunctionSignature
    {
        public MaxFunction()
        {
            Keys = new [] { "Max" };
        }
    }
}
