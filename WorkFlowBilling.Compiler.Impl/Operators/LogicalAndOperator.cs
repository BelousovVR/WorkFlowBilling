using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "логическое И" (конъюнкция)
    /// </summary>
    public class LogicalAndOperator : OperatorBase
    {
        public LogicalAndOperator()
        {
            Keys = new[] { "&&" };
            Priority = 5;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
