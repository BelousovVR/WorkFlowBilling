using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор вычитания
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
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
