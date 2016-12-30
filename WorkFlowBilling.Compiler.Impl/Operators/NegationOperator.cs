using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператора логического отрицания
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class NegationOperator : OperatorBase
    {
        public NegationOperator()
        {
            Keys = new[] { "!" };
            Priority = 14;
            OperatorType = OperatorType.Unary;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
