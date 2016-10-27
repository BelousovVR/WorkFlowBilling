using Autofac;
using NUnit.Framework;
using System;
using WorkFlowBilling.IoC.Enumerations;
using WorkFlowBilling.IoC.Extensions;
using WorkFlowBilling.IoC.Tests.Stubs;

namespace WorkFlowBilling.IoC.Tests.Extensions
{
    /// <summary>
    /// Тесты расширений для Autofac ContainerBuilder
    /// </summary>
    [TestFixture]
    public class ContainerBuilderExtensionsTests
    {
        [Test]
        public void ContainerBuilderExtensions_GetAutoInjectableAttribute_AttributeExists()
        {
            //GIVEN
            var type = typeof(InterfaceWithAutoInjectableAttribute);

            //WHEN
            var injectableAttribute = ContainerBuilderExtensions.GetAutoInjectableAttribute(type);

            //THEN
            Assert.NotNull(injectableAttribute);
        }

        [Test]
        public void ContainerBuilderExtensions_GetAutoInjectableInstanceAttribute_AttributeExists()
        {
            //GIVEN
            var type = typeof(ClassWithAutoInjectableInstanceAttribute);

            //WHEN
            var injectableAttribute = ContainerBuilderExtensions.GetAutoInjectableInstanceAttribute(type);

            //THEN
            Assert.NotNull(injectableAttribute);
        }

        [Test]
        public void ContainerBuilderExtensions_HasAutoInjectableInterface_NoInterfaces()
        {
            //GIVEN
            var type = typeof(ClassWithoutInterfaces);

            //WHEN
            var hasAutoInjectableInterface = ContainerBuilderExtensions.HasAutoInjectableInterface(type);

            //THEN
            Assert.IsFalse(hasAutoInjectableInterface);
        }

        [Test]
        public void ContainerBuilderExtensions_HasAutoInjectableInterface_HasAutoInjectableInterface()
        {
            //GIVEN
            var type = typeof(ClassWithAutoInjectableInterface);

            //WHEN
            var hasAutoInjectableInterface = ContainerBuilderExtensions.HasAutoInjectableInterface(type);

            //THEN
            Assert.IsTrue(hasAutoInjectableInterface);
        }

        [Test]
        public void ContainerBuilderExtensions_HasAutoInjectableInterface_HasInterface()
        {
            //GIVEN
            var type = typeof(ClassWithInterface);

            //WHEN
            var hasAutoInjectableInterface = ContainerBuilderExtensions.HasAutoInjectableInterface(type);

            //THEN
            Assert.IsFalse(hasAutoInjectableInterface);
        }

        [Test]
        public void ContainerBuilderExtensions_AddAssembliesTypes_InjectableExists()
        {
            //GIVEN
            var assembly = typeof(AssemblyRef).Assembly;
            var builder = new ContainerBuilder();
            var awaitedType = typeof(InjectableClass);

            //WHEN
            builder.RegisterAssembliesTypes(assembly);
            Type resolvedType;
            using (var container = builder.Build())
            {
                resolvedType = container.Resolve<InjectableInterface>().GetType();
            }

            //THEN
            Assert.AreEqual(awaitedType, resolvedType);
        }

        [Test]
        public void ContainerBuilderExtensions_RegisterTypeExtension_InjectableExists()
        {
            //GIVEN
            var builder = new ContainerBuilder();
            var awaitedType = typeof(InjectableClass);
            var serviceType = typeof(InjectableInterface);

            //WHEN
            builder.RegisterType(awaitedType, new[] { serviceType }, InstanceLifeTime.SingleInstance);
            Type resolvedType;
            using (var container = builder.Build())
            {
                resolvedType = container.Resolve<InjectableInterface>().GetType();
            }

            //THEN
            Assert.AreEqual(awaitedType, resolvedType);
        }
    }
}
