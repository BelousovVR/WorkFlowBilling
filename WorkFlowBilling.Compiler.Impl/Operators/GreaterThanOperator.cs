using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "больше" (>)
    /// </summary>
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
