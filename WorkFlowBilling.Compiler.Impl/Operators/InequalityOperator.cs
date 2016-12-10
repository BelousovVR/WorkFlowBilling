using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "не равно" (!=)
    /// </summary>
    public class InequalityOperator : OperatorBase
    {
        public InequalityOperator()
        {
            Keys = new[] { "!=" };
            Priority = 9;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
