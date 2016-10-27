using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.IoC.Tests.Stubs
{
    /// <summary>
    /// Интерфейс с AutoInjectableAttribute
    /// </summary>
    [AutoInjectable]
    public interface InterfaceWithAutoInjectableAttribute { }

    /// <summary>
    /// Интерфейс без AutoInjectableAttribute
    /// </summary>
    public interface InterfaceWithoutAutoInjectableAttribute { }

    /// <summary>
    /// Тестовый интерфейс для класса, имеющего AutoInjectableInstanceAttribute
    /// </summary>
    [AutoInjectable]
    public interface InjectableInterface { }
}
