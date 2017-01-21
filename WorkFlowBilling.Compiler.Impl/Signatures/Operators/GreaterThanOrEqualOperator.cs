using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор "больше или равно" (>=)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class GreaterThanOrEqualOperator : OperatorBase
    {
        public GreaterThanOrEqualOperator()
        {
            Keys = new[] { ">=" };
            Priority = 10;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
