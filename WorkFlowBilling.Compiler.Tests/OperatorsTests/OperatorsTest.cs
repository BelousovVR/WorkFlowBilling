using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WorkFlowBilling.Compiler.Operators;

namespace WorkFlowBilling.Compiler.Tests.OperatorsTests
{
    [TestFixture]
    public class OperatorsTest : TranslatorTestBase
    {

        /// <summary>
        /// Получить список тест-кейсов для методов Match операторов, когда на вход подается null
        /// </summary>
        /// <returns></returns>
        private static List<TestCaseData> GetOperatorsMatchWithNullStringTestCases()
        {
            return Operators.Select(o =>
            {
                var operatorValue = o.Value;
                var operatorName = operatorValue.GetType().Name;
                var testCaseData = new TestCaseData(operatorValue);
                testCaseData.SetName($"Operators_{operatorName}_NullString");

                return testCaseData;
            })
           .ToList();
        }

        /// <summary>
        /// Получить тест-кейсы для проверки Match операторов
        /// </summary>
        /// <returns></returns>
        private static List<TestCaseData> GetOperatorsMatchTestCases()
        {
            return Operators.Select(o =>
            {
                var operatorValue = o.Value;
                var operatorKey = o.Key;
                var operatorName = operatorValue.GetType().Name;
                var testCaseData = new TestCaseData(operatorValue, operatorKey);
                testCaseData.SetName($"Operators_{operatorName}_Match");

                return testCaseData;
            })
           .ToList();
        }

        [Test, TestCaseSource(nameof(GetOperatorsMatchWithNullStringTestCases))]
        public void Operators_NullInputString(IOperatorSignature operatorObj)
        {
            // GIVEN
            string inputString = null;

            //WHEN
            var isMatched = operatorObj.Match(inputString).IsMatched;

            //THEN
            Assert.AreEqual(false, isMatched);
        }

        [Test, TestCaseSource(nameof(GetOperatorsMatchTestCases))]
        public void Operators_Match(IOperatorSignature operatorObj, string operatorKey)
        {
            //WHEN
            var isMatched = operatorObj.Match(operatorKey).IsMatched;

            //THEN
            Assert.AreEqual(true, isMatched);
        }
    }
}
