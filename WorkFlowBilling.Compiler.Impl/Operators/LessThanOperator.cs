using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "меньше" (<)
    /// </summary>
    public class LessThanOperator : OperatorBase
    {
        public LessThanOperator()
        {
            Keys = new[] { "<" };
            Priority = 10;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
