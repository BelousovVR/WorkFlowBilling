using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Extensions;
using System;

namespace WorkFlowBilling.IoC.Container
{
    /// <summary>
    /// Менеджер DI контейнера 
    /// </summary>
    [AutoInjectableInstance]
    public class ContainerManager : IContainerManager
    {
        private ContainerBuilder Builder;
        private IContainer Container;

        /// <summary>
        /// Зарегистрировать контроллеры
        /// </summary>
        /// <param name="assembly">Сборка, содержащяя контроллеры</param>
        /// <param name="propertiesAutowired">Подставлять свойства не используя конструктор</param>
        public void RegisterControllers (Assembly assembly, bool propertiesAutowired = true)
        {
            if (propertiesAutowired)
                Builder.RegisterControllers(assembly).PropertiesAutowired();
            else
                Builder.RegisterControllers(assembly);
        }

        /// <summary>
        /// Зарегистрировать типы из сборки
        /// </summary>
        /// <param name="assembly">Сборка</param>
        public void RegisterAssembliesTypes(Assembly assembly)
        {
            Builder.RegisterAssembliesTypes(assembly);
        }

        /// <summary>
        /// Установить DependencyResolver
        /// </summary>
        public void SetAspNetMvcResolver()
        {
            if (Container == null)
                Container = Builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        /// <summary>
        /// Подставить значения свойства
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Inject<T>(T obj) where T : class
        {
            if (Container == null)
                Container = Builder.Build();

            Container.InjectProperties(obj);
        }

        public ContainerManager()
        {
            Builder = new ContainerBuilder();
        }
    }
}
