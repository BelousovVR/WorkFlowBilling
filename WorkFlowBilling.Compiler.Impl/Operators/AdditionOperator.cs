using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор сложения
    /// </summary>
    public class AdditionOperator : OperatorBase
    {
        public AdditionOperator()
        {
            Keys = new[] { "+" };
            Priority = 12;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
