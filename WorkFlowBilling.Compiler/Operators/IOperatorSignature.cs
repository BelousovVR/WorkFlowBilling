using WorkFlowBilling.Compiler.Signatures;

namespace WorkFlowBilling.Compiler.Operators
{
    /// <summary>
    /// Базовый интерфейс оператора
    /// </summary>
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
    }
}
