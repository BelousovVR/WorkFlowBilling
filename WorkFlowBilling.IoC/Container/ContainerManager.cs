using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Extensions;

namespace WorkFlowBilling.IoC.Container
{
    /// <summary>
    /// Менеджер DI контейнера 
    /// </summary>
    [AutoInjectableInstance]
    public class ContainerManager : IContainerManager
    {
        private ContainerBuilder Builder;

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
            var contaner = Builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(contaner));
        }

        public ContainerManager()
        {
            Builder = new ContainerBuilder();
        }
    }
}
