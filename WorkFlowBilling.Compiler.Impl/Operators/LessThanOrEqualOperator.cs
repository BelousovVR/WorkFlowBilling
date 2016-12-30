using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "меньше или равно" (<=)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class LessThanOrEqualOperator : OperatorBase
    {
        public LessThanOrEqualOperator()
        {
            Keys = new[] { "<=" };
            Priority = 10;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}