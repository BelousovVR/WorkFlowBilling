using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowBilling.Compiler.Impl.Operators;
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
        public void Operators_NullInputString(IOperator operatorObj)
        {
            // GIVEN
            string inputString = null;

            //WHEN
            var isMatched = operatorObj.Match(inputString);

            //THEN
            Assert.AreEqual(false, isMatched);
        }

        [Test, TestCaseSource(nameof(GetOperatorsMatchTestCases))]
        public void Operators_Match(IOperator operatorObj, string operatorKey)
        {
            //WHEN
            var isMatched = operatorObj.Match(operatorKey);

            //THEN
            Assert.AreEqual(true, isMatched);
        }

    }
}
