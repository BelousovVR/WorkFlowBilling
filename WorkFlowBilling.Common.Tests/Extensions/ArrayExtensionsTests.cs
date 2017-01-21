using NUnit.Framework;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Common.Tests.Extensions
{
    /// <summary>
    /// Тесты для расширений Array
    /// </summary>
    [TestFixture]
    public class ArrayExtensionsTests
    {
        [Test]
        public void ArrayExtensionsTests_Exists_SearchedValueExists()
        {
            //GIVEN
            var array = new[] { 1, 2, 3 };
            var searchedValue = 2;
            var awaitedResult = true;

            //WHEN
            var result = array.Exists(_ => _ == searchedValue);

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ArrayExtensionsTests_Exists_SearchedValueNotExists()
        {
            //GIVEN
            var array = new[] { 1, 2, 3 };
            var searchedValue = 100500;
            var awaitedResult = false;

            //WHEN
            var result = array.Exists(_ => _ == searchedValue);

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }
    }
}
