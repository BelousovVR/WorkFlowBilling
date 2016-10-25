﻿using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.IoC.Tests.Stubs
{
    /// <summary>
    /// Класс с AutoInjectableInstanceAttribute
    /// </summary>
    [AutoInjectableInstance]
    public class ClassWithAutoInjectableInstanceAttribute { }

    /// <summary>
    /// Класс без интерфейсов
    /// </summary>
    public class ClassWithoutInterfaces { }

    /// <summary>
    /// Класс с с интерфейсом с AutoInjectableAttribute
    /// </summary>
    public class ClassWithAutoInjectableInterface : InterfaceWithAutoInjectableAttribute { }

    /// <summary>
    /// Класс с интерфейсом без AutoInjectableAttribute
    /// </summary>
    public class ClassWithInterface : InterfaceWithoutAutoInjectableAttribute { }

    /// <summary>
    /// Класс, реализующий первый тестовый интерфейс
    /// </summary>
    [AutoInjectableInstance]
    public class InjectableClass : InjectableInterface { }
}
