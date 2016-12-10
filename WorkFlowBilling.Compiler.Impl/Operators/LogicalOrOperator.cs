using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "логическое ИЛИ" (дизъюнкция)
    /// </summary>
    public class LogicalOrOperator : OperatorBase
    {
        public LogicalOrOperator()
        {
            Keys = new[] { "||" };
            Priority = 4;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
