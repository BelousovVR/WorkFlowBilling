using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "равно" (==)
    /// </summary>
    public class EqualityOperator : OperatorBase
    {
        public EqualityOperator()
        {
            Keys = new[] { "==" };
            Priority = 9;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
