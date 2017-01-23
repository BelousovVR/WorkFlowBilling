using NUnit.Framework;
using System;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Common.Tests.Extensions
{
    /// <summary>
    /// Тесты для расширений String
    /// </summary>
    [TestFixture]
    public class StringExtensionsTests
    {
        private enum TestEnum
        {
            First,
            Second
        };

        [Test]
        public void StringExtensions_ParseEnum_CorrectEnumString()
        {
            //GIVEN
            var awaitedEnum = TestEnum.Second;
            var enumName = "Second";

            //WHEN
            var resultEnum = enumName.ParseEnum<TestEnum>();

            //THEN
            Assert.AreEqual(awaitedEnum, resultEnum);
        }

        [Test]
        public void StringExtensions_TryGetDecimal()
        {
            //GIVEN
            var inputString = "22.12abs";
            decimal awaitedDecimal = 22.12M;

            //WHEN
            decimal decimalValue;
            var convertSuccess = inputString.TryGetDecimal(out decimalValue);

            //THEN
            Assert.AreEqual(awaitedDecimal, decimalValue);
        }

        [Test]
        public void StringExtensions_Split()
        {
            //GIVEN
            var inputString = "1 2 3 4";
            var awaitedResult = new[] { "1", "2", "3", "4" };

            //WHEN
            var result = inputString.Split(StringSplitOptions.None, " ");

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }
    }
}
