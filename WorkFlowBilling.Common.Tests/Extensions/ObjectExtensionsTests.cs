using NUnit.Framework;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Common.Tests.Extensions
{
    /// <summary>
    /// Тесты для расширений System.Object
    /// </summary>
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ObjectExtensions_In_ValueTypeExists()
        {
            //GIVEN
            var intValue = 22;
            var awaitedResult = true;

            //WHEN 
            var result = intValue.In(5, 6, 22);

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ObjectExtensions_In_ValueTypeNotExists()
        {
            //GIVEN
            var intValue = 22;
            var awaitedResult = false;

            //WHEN 
            var result = intValue.In(5, 6, 33);

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ObjectExtensions_In_StringExists()
        {
            //GIVEN
            var stringValue = "123";
            var awaitedResult = true;

            //WHEN 
            var result = stringValue.In("123", "55");

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ObjectExtensions_NotIn_ValueTypeExists()
        {
            //GIVEN
            var intValue = 22;
            var awaitedResult = false;

            //WHEN 
            var result = intValue.NotIn(5, 6, 22);

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ObjectExtensions_NotIn_ValueTypeNotExists()
        {
            //GIVEN
            var intValue = 22;
            var awaitedResult = true;

            //WHEN 
            var result = intValue.NotIn(5, 6, 33);

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void ObjectExtensions_NotIn_StringExists()
        {
            //GIVEN
            var stringValue = "123";
            var awaitedResult = false;

            //WHEN 
            var result = stringValue.NotIn("123", "55");

            // THEN
            Assert.AreEqual(awaitedResult, result);
        }
    }
}
