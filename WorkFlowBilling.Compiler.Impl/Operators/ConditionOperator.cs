using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор с условием
    /// </summary>
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
