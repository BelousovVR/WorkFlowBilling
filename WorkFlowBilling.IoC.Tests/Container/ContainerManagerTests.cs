using NUnit.Framework;
using WorkFlowBilling.IoC.Container;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace WorkFlowBilling.IoC.Tests.Extensions
{
    /// <summary>
    /// Тесты IoC контейнера
    /// </summary>
    [TestFixture]
    public class ContainerManagerTests
    {
        [Test]
        public void ContainerManager_DependencyResolver_SetCurrent()
        {
            //GIVEN
            var containerManager = new ContainerManager();
          
            //WHEN
            containerManager.SetAspNetMvcResolver();

            //THEN
            Assert.IsTrue(DependencyResolver.Current is AutofacDependencyResolver);
        }
    }
}
