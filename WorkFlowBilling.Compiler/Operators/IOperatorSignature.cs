using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Operators
{
    /// <summary>
    /// Базовый интерфейс сигнатуры оператора
    /// </summary>
    [AutoInjectable]
    public interface IOperatorSignature : ISignature
    {
        /// <summary>
        /// Тип ассоциативность оператора
        /// </summary>
        OperatorAssociativity Associativity { get; }

        /// <summary>
        /// Тип оператора
        /// </summary>
        OperatorType OperatorType { get; }

        /// <summary>
        /// Приоритет
        /// </summary>
        int Priority { get; }
    }
}
