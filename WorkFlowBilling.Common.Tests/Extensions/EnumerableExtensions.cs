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
            var list = new List<int>();

            //WHEN
            var action = new TestDelegate(() => list.ForEach(_ => _.ToString()));

            //THEN
            Assert.DoesNotThrow(action, "Метод ForEach не должен вызывать исключения при обработке Empty Enumerable");
        }

        [Test]
        public void EnumerableExtensions_ForEach_ActionExecuted()
        {
            //GIVEN
            var inputList = new List<int>() { 1, 2, 3 };
            var outputList = new List<int>();
            var awaitedList = new List<int>() { 11, 12, 13 };

            //WHEN
            inputList.ForEach(_ => outputList.Add(_ + 10));

            //THEN
            Assert.AreEqual(awaitedList, outputList);
        }
    }
}
