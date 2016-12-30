using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "больше" (>)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class GreaterThanOperator : OperatorBase
    {
        public GreaterThanOperator()
        {
            Keys = new[] { ">" };
            Priority = 10;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
