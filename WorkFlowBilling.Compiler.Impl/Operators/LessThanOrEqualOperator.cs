using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "меньше или равно" (<=)
    /// </summary>
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