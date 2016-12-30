using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "логическое И" (конъюнкция)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class LogicalAndOperator : OperatorBase
    {
        public LogicalAndOperator()
        {
            Keys = new[] { "&&" };
            Priority = 5;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
