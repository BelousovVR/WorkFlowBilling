using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор умножения
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
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
