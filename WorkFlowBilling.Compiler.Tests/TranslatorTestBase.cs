using System.Collections.Generic;
using WorkFlowBilling.Compiler.Impl.Operators;
using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Tests
{
    /// <summary>
    /// Базовый класс для unit-тестов трансляторов. Содержит контекст и вспомогательные методы
    /// </summary>
    public class TranslatorTestBase
    {
        protected static Dictionary<string, IOperator> Operators = new Dictionary<string, IOperator>()
        {
            {"+", new AdditionOperator()},
            {"-", new SubstractionOperator()},
            {"*", new MultiplicationOperator()},
            {"/", new DivisionOperator()},
            {"++", new PrefixIncrementOperator()},
            {"--", new PrefixDecrementOperator()},
            {"!", new NegationOperator()},
            {"<", new LessThanOperator()},
            {">", new GreaterThanOperator()},
            {"<=", new LessThanOrEqualOperator()},
            {">=", new GreaterThanOrEqualOperator()},
            {"==", new EqualityOperator()},
            {"!=", new InequalityOperator()},
            {"&&", new LogicalAndOperator()},
            {"||", new LogicalOrOperator()},
            {"?", new ConditionOperator()},
        };
    }
}
