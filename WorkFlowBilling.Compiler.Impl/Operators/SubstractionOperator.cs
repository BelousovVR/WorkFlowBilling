using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор вычитания
    /// </summary>
    public class SubstractionOperator : OperatorBase
    {
        public SubstractionOperator()
        {
            Keys = new[] { "-" };
            Priority = 12;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
