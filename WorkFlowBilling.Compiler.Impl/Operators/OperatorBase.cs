using System.Linq;
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Signatures;
using static System.String;

namespace WorkFlowBilling.Compiler.Impl.Operators
{
    /// <summary>
    /// Базовый класс для оператора
    /// </summary>
    public abstract class OperatorBase : IOperator
    {
        /// <summary>
        /// Тип ассоциативности оператора
        /// </summary>
        public OperatorAssociativity Associativity { get; protected set; }

        /// <summary>
        /// Ключ оператора
        /// </summary>
        public string[] Keys { get; protected set; }

        /// <summary>
        /// Тип оператора
        /// </summary>
        public OperatorType OperatorType { get; protected set; }

        /// <summary>
        /// Приоритет оператора
        /// </summary>
        public int Priority { get; protected set; }

        /// <summary>
        /// Тип сигнатуры
        /// </summary>
        public SignatureType SignatureType
        {
            get
            {
                return SignatureType.Operator;
            }
        }

        /// <summary>
        /// Вернуть истину, если подстрока содержит символ/символы оператора
        /// </summary>
        /// <param name="inputString">Подстрока, начинающаяся с символа оператора</param>
        /// <returns></returns>
        public virtual bool Match(string inputString)
        {
            if (IsNullOrWhiteSpace(inputString))
                return false;

            var trimedString = inputString.TrimStart(' ');
            var key = Keys.First();
            return trimedString.StartsWith(key);
        }
    }
}
