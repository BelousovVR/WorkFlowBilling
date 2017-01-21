using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор деления
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
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
