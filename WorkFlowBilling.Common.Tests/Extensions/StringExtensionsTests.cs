using NUnit.Framework;
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
    }
}
