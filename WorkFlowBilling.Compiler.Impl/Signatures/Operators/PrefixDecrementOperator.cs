using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Префиксный оператора уменьшения
    /// --(5 + 6) <=>  (5 + 6) - 1
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
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
