using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    //TODO TESTS
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    class NegatationNumberOperator : OperatorBase
    {
        public NegatationNumberOperator()
        {
            Keys = new[] { "-" };
            Priority = 14;
            OperatorType = OperatorType.Unary;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
