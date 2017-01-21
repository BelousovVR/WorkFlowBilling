using NUnit.Framework;
using System.Collections.Generic;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Common.Tests
{
    /// <summary>
    /// Тесты для расширений  Enumerable
    /// </summary>
    [TestFixture]
    public class EnumerableExtensions
    {
        [Test]
        public void EnumerableExtensions_ForEach_EmptyEnumerable()
        {
            //GIVEN
            IEnumerable<int> list = new List<int>();

            //WHEN
            var action = new TestDelegate(() => list.ForEach(_ => _.ToString()));

            //THEN
            Assert.DoesNotThrow(action, "Метод ForEach не должен вызывать исключения при обработке Empty Enumerable");
        }

        [Test]
        public void EnumerableExtensions_ForEach_ActionExecuted()
        {
            //GIVEN
            IEnumerable<int> inputList = new List<int>() { 1, 2, 3 };
            var outputList = new List<int>();
            var awaitedList = new List<int>() { 11, 12, 13 };

            //WHEN
            inputList.ForEach(_ => outputList.Add(_ + 10));

            //THEN
            Assert.AreEqual(awaitedList, outputList);
        }

        [Test]
        public void EnumerableExtensions_IsNullOrEmpty_NotEmptyArray()
        {
            //GIVEN
            var array = new [] { 1, 2, 3};
            var awaitedResult = false;

            //GIVEN
            var result = array.IsNullOrEmpty();

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void EnumerableExtensions_IsNullOrEmpty_EmptyArray()
        {
            //GIVEN
            var array = new int[0];
            var awaitedResult = true;

            //GIVEN
            var result = array.IsNullOrEmpty();

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void EnumerableExtensions_IsNullOrEmpty_NullEnumerable()
        {
            //GIVEN
            IEnumerable<int> enumerable = null;
            var awaitedResult = true;

            //GIVEN
            var result = enumerable.IsNullOrEmpty();

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }
    }
}
