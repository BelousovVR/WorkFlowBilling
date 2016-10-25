using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JavaScriptEngineSwitcher.Core;
using WorkFlowBilling.App_Start;
using WorkFlowBilling.IoC.Container;

namespace WorkFlowBilling
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Создать containerManager
            IContainerManager containerManager = new ContainerManager();

            // Зарегистрировать контроллеры
            containerManager.RegisterControllers(typeof(MvcApplication).Assembly);

            // Зарегистрировать типы
            containerManager.RegisterAssembliesTypes(typeof(Compiler.Impl.AssemblyRef).Assembly);

            // Установка сопоставителя зависимостей
            containerManager.SetAspNetMvcResolver();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Добавлено для корректной работы BundleTransformer.Less
            // https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/How-to-upgrade-applications-to-version-2.X
            JsEngineSwitcherConfig.Configure(JsEngineSwitcher.Instance);
        }
    }
}
