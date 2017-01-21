using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор сложения
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
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
