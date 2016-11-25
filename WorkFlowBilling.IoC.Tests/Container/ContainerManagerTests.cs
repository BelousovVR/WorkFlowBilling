using NUnit.Framework;
using WorkFlowBilling.IoC.Container;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using WorkFlowBilling.IoC.Tests.Stubs;
using System.Web;
using System.IO;

namespace WorkFlowBilling.IoC.Tests.Extensions
{
    /// <summary>
    /// Тесты IoC контейнера
    /// </summary>
    [TestFixture]
    public class ContainerManagerTests
    {
        private HttpContext CreateHttpContextStub()
        {
            var httpRequest = new HttpRequest("", "http://tempuri.org", "");
            var httpResponse = new HttpResponse(new StringWriter());
            return new HttpContext(httpRequest, httpResponse);
        }

        /// <summary>
        /// Устанавливаем заглушку в DependencyResolver.Current перед каждым тестом
        /// </summary>
        [SetUp]
        public void BeforeTestRun()
        {
            var currentDependencyResolver = DependencyResolver.Current;
            if (currentDependencyResolver != null)
            {
                var stubObject = new Moq.Mock<IDependencyResolver>().Object;
                DependencyResolver.SetResolver(stubObject);
            }
        }

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

        [Test]
        public void ContainerManager_DependencyResolver_ResolveCorrect()
        {
            //GIVEN
            var containerManager = new ContainerManager();
            containerManager.RegisterAssembliesTypes(typeof(AssemblyRef).Assembly);
            containerManager.SetAspNetMvcResolver();
            HttpContext.Current = CreateHttpContextStub();
            var awaitedType = typeof(InjectableClass);

            //WHEN
            var resolvedType = DependencyResolver.Current.GetService<InjectableInterface>().GetType();
            
            //THEN
            Assert.AreEqual(awaitedType, resolvedType);
        }

        [Test]
        public void ContainerManager_Inject_InjectCorrect()
        {
            //GIVEN
            var containerManager = new ContainerManager();
            containerManager.RegisterAssembliesTypes(typeof(AssemblyRef).Assembly);

            var propertyInjectedClass = new PropertyInjectedClass();

            //WHEN
            containerManager.Inject(propertyInjectedClass);

            //THEN
            Assert.NotNull(propertyInjectedClass.InjectableInstance);
        }

        [Test]
        public void ContainerManager_InjectGlobalFilters_InjectCorrect()
        {
            //GIVEN
            var containerManager = new ContainerManager();
            containerManager.RegisterAssembliesTypes(typeof(AssemblyRef).Assembly);
            var globalFilter = new GlobalFilter();
            var globalFilterCollection = new GlobalFilterCollection()
            {
                globalFilter
            };

            //WHEN
            containerManager.InjectGlobalFilters(globalFilterCollection);

            //THEN
            Assert.NotNull(globalFilter.InjectableInstance);
        }
    }
}
