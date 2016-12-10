using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "больше или равно" (>=)
    /// </summary>
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
