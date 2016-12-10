using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор деления
    /// </summary>
    public class DivisionOperator : OperatorBase
    {
        public DivisionOperator()
        {
            Keys = new[] { "/" };
            Priority = 13;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
