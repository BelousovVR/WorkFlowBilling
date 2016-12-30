using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Оператор "логическое ИЛИ" (дизъюнкция)
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class LogicalOrOperator : OperatorBase
    {
        public LogicalOrOperator()
        {
            Keys = new[] { "||" };
            Priority = 4;
            OperatorType = OperatorType.Binary;
            Associativity = OperatorAssociativity.Left;
        }
    }
}
