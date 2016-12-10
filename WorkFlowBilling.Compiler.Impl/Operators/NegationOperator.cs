using WorkFlowBilling.Compiler.Operators;
namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператора логического отрицания
    /// </summary>
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
