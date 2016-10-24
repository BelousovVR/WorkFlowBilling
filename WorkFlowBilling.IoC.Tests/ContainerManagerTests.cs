using System;
using NUnit.Framework; 

namespace WorkFlowBilling.IoC.Tests
{
    [TestFixture]
    public class ContainerManagerTests
    {
        [Test]
        public void TestMethod1()
        {
            //GIVEN
            var a = 1;

            //WHEN
            a += 1;

            //THEN
            Assert.AreEqual(2, a);
        }
    }
}
