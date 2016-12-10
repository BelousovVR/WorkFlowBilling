using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Префиксный оператора увеличения
    /// ++(5 + 6) <=> 1 + (5 + 6)
    /// </summary>
    public class PrefixIncrementOperator : OperatorBase
    {
        public PrefixIncrementOperator()
        {
            Keys = new[] { "++" };
            Priority = 14;
            OperatorType = OperatorType.Unary;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
