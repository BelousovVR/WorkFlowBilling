using NUnit.Framework;
using System;
using System.Collections.Generic;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Common.Tests.Extensions
{
    /// <summary>
    /// Тесты для расширений Array
    /// </summary>
    [TestFixture]
    public class StackExtensionsTests
    {
        [Test]
        public void StackExtensionsTests_PopMany_PopElementsExists()
        {
            //GIVEN
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);

           var awaitedResult = new List<int> {2, 1};

            //WHEN
            var result = stack.PopMany(2);

            //THEN
            Assert.AreEqual(awaitedResult, result);
        }

        [Test]
        public void StackExtensionsTests_PopMany_PopElementsNotExists()
        {
            //GIVEN
            var stack = new Stack<int>();
            stack.Push(1);

            TestDelegate action = () => { stack.PopMany(2); };

            //WHEN / THEN
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}
