using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор с условием
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class ConditionOperator : OperatorBase
    {
        public ConditionOperator()
        {
            Keys = new[] { "?", ":" };
            Priority = 3;
            OperatorType = OperatorType.Ternarу;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
