using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkFlowBilling.Common.Extensions;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.IoC.Extensions
{
    /// <summary>
    /// Расширения для Autofac ContainerBuilder
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        // TODO: тесты
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static AutoInjectableAttribute GetAutoInjectableAttribute(Type t)
        {
            return t.GetCustomAttribute<AutoInjectableAttribute>();
        }

        // TODO: тесты
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static AutoInjectableInstanceAttribute GetAutoInjectableInstanceAttribute(Type t)
        {
            return t.GetCustomAttribute<AutoInjectableInstanceAttribute>();
        }

        // TODO: тесты
        /// <summary>
        /// Проверить, реализует ли тип интерфейс, помеченный аттрибутом AutoInjectable
        /// </summary>
        /// <param name="t">Тип</param>
        /// <returns></returns>
        public static bool HasAutoInjectableInterface(Type t)
        {
            return t.GetInterfaces().Any(_ =>  GetAutoInjectableAttribute(_) != null);
        }

        // TODO: тесты (с моками и т.д.)
        /// <summary>
        /// Зарегистрировать типы из сборки
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        public static void AddAssembliesTypes(this ContainerBuilder builder, Assembly assembly)
        {
            if (assembly == null)
            {
                throw new NullReferenceException("assembly null reference");
            }

            // получить типы, помеченные аттрибутом AutoInjectableInstanceAttribute
            // и связать их с интерфейсами, помеченными AutoInjectableAttribute
            var injectInfo = assembly
                .GetTypes()
                .Where(_ => GetAutoInjectableInstanceAttribute(_) != null && HasAutoInjectableInterface(_))
                .Select(_ => new
                {
                    TypeValue = _,
                    InstanceAttribute = GetAutoInjectableInstanceAttribute(_),
                    InjectableInterfaces = _.GetInterfaces()
                                            .Where(i => GetAutoInjectableAttribute(i) != null)
                                            .ToArray()
                });

            // зарегистрировать типы
            injectInfo.ForEach(_ =>
            {
                switch (_.InstanceAttribute.InstanceLifeTime)
                {
                    case InstanceLifeTime.SingleInstance:
                        builder.RegisterType(_.TypeValue).As(_.InjectableInterfaces).PropertiesAutowired().SingleInstance();
                        break;
                    case InstanceLifeTime.InstancePerLifeTimeScope:
                        builder.RegisterType(_.TypeValue).As(_.InjectableInterfaces).PropertiesAutowired().InstancePerLifetimeScope();
                        break;
                    case InstanceLifeTime.InstancePerRequest:
                        builder.RegisterType(_.TypeValue).As(_.InjectableInterfaces).PropertiesAutowired().InstancePerDependency();
                        break;
                }
            });
        }
    }
}
