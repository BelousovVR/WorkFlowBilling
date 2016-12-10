using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Префиксный оператора уменьшения
    /// --(5 + 6) <=>  (5 + 6) - 1
    /// </summary>
    public class PrefixDecrementOperator : OperatorBase
    {
        public PrefixDecrementOperator()
        {
            Keys = new[] { "--" };
            Priority = 14;
            OperatorType = OperatorType.Unary;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
