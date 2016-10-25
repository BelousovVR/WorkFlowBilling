using System.Reflection;

namespace WorkFlowBilling.IoC.Container
{
    /// <summary>
    /// Менеджер DI контейнера 
    /// </summary>
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
    }
}
