using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "равно" (==)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class EqualityOperator : OperatorBase
    {
        public EqualityOperator()
        {
            Keys = new[] { "==" };
            Priority = 9;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
