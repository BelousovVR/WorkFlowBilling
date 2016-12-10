using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор умножения
    /// </summary>
    public class MultiplicationOperator : OperatorBase
    {
        public MultiplicationOperator()
        {
            Keys = new[] { "*" };
            Priority = 13;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
