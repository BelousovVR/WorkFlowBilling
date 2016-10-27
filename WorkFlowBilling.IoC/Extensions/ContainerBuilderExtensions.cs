using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// Получить AutoInjectableAttribute для указанного типа
        /// </summary>
        /// <param name="t">Тип</param>
        /// <returns></returns>
        public static AutoInjectableAttribute GetAutoInjectableAttribute(Type t)
        {
            return t.GetCustomAttribute<AutoInjectableAttribute>();
        }

        /// <summary>
        /// Получить AutoInjectableInstanceAttribute для указанного типа
        /// </summary>
        /// <param name="t">Тип</param>
        /// <returns></returns>
        public static AutoInjectableInstanceAttribute GetAutoInjectableInstanceAttribute(Type t)
        {
            return t.GetCustomAttribute<AutoInjectableInstanceAttribute>();
        }

        /// <summary>
        /// Проверить, реализует ли тип интерфейс, помеченный аттрибутом AutoInjectable
        /// </summary>
        /// <param name="t">Тип</param>
        /// <returns></returns>
        public static bool HasAutoInjectableInterface(Type t)
        {
            return t.GetInterfaces().Any(_ =>  GetAutoInjectableAttribute(_) != null);
        }

        /// <summary>
        /// Зарегистрировать тип
        /// </summary>
        /// <param name="builder">Билдер</param>
        /// <param name="type">Тип</param>
        /// <param name="services">зарегистрировать как</param>
        /// <param name="instanceLifeTime">Время жизни объекта</param>
        public static void RegisterType(this ContainerBuilder builder, 
                                        Type type, 
                                        Type[] services,
                                        InstanceLifeTime instanceLifeTime)
        {
            switch (instanceLifeTime)
            {
                case InstanceLifeTime.SingleInstance:
                    builder.RegisterType(type).As(services).PropertiesAutowired().SingleInstance();
                    break;
                case InstanceLifeTime.InstancePerLifeTimeScope:
                    builder.RegisterType(type).As(services).PropertiesAutowired().InstancePerLifetimeScope();
                    break;
                case InstanceLifeTime.InstancePerRequest:
                    builder.RegisterType(type).As(services).PropertiesAutowired().InstancePerDependency();
                    break;
            }
        }

        /// <summary>
        /// Зарегистрировать типы из сборки
        /// </summary>
        /// <param name="builder">Билдер</param>
        /// <param name="assembly">Сборка</param>
        public static void RegisterAssembliesTypes(this ContainerBuilder builder, Assembly assembly)
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
            injectInfo.ForEach(_ => builder.RegisterType(_.TypeValue, 
                                                        _.InjectableInterfaces, 
                                                        _.InstanceAttribute.InstanceLifeTime)
            );
        }
    }
}
