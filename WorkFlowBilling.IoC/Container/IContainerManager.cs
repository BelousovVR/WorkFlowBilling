using System.Reflection;
using System.Web.Mvc;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.IoC.Container
{
    /// <summary>
    /// Менеджер DI контейнера 
    /// </summary>
    [AutoInjectable] 
    public interface IContainerManager
    {
        /// <summary>
        /// Зарегистрировать контроллеры
        /// </summary>
        /// <param name="assembly">Сборка, содержащяя контроллеры</param>
        /// <param name="propertiesAutowired">Подставлять свойства не используя конструктор</param>
        void RegisterControllers(Assembly assembly, bool propertiesAutowired = true);

        /// <summary>
        /// Зарегистрировать типы из сборки
        /// </summary>
        /// <param name="assembly">Сборка</param>
        void RegisterAssembliesTypes(Assembly assembly);

        /// <summary>
        /// Установить DependencyResolver
        /// </summary>
        void SetAspNetMvcResolver();

        /// <summary>
        /// Подставить свойства объекта
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="obj">Объект</param>
        void Inject<T>(T obj) where T : class;

        /// <summary>
        /// Подставить свойства в глобальные фильтры
        /// </summary>
        /// <param name="filterCollection">Список глобальных фильтров</param>
        void InjectGlobalFilters(GlobalFilterCollection filterCollection);

        /// <summary>
        /// Зарегистрировать фильтры
        /// </summary>
        void RegisterAspNetMvcFilterProvider();
    }
}
