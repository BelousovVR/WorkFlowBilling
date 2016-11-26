using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowBilling.IoC.Container;

namespace WorkFlowBilling.TestFramework.Common
{
    /// <summary>
    /// Базовый класс для функциональных тестов
    /// Производит Inject свойств
    /// </summary>
    [TestFixture]
    public class BaseFunctionalTest
    {
        public IContainerManager ContainerManager { get; private set; }

        public BaseFunctionalTest()
        {
            ContainerManager = new ContainerManager();
        }

        public virtual void RegisterAssemblies()
        {
            return;
        }

        [OneTimeSetUp]
        public virtual void BeforeTestsRun()
        {
            RegisterAssemblies();
            ContainerManager.Inject(this);
        }

        [OneTimeTearDown]
        public virtual void AfterTestsRun()
        {
            return;
        }

        [SetUp]
        public virtual void BeforeTestRun()
        {
            return;
        }

        [TearDown]
        public virtual void AfterTestRun()
        {
            return;
        }
    }
}
