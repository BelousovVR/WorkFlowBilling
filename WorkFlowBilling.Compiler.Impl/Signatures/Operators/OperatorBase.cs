using WorkFlowBilling.Compiler.Impl.Signatures;
using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Базовый класс для оператора
    /// </summary>
    public abstract class OperatorBase : SignatureBase, IOperatorSignature
    {
        /// <summary>
        /// Тип ассоциативности оператора
        /// </summary>
        public OperatorAssociativity Associativity { get; protected set; }

        /// <summary>
        /// Тип оператора
        /// </summary>
        public OperatorType OperatorType { get; protected set; }

        /// <summary>
        /// Приоритет оператора
        /// </summary>
        public int Priority { get; protected set; }
    }
}
