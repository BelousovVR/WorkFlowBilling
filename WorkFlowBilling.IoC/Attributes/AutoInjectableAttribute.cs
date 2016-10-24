using System;

namespace WorkFlowBilling.IoC.Attributes
{
    /// <summary>
    /// Атрибут, помечающий интерфейс как подставляемый автоматически
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class AutoInjectableAttribute : Attribute
    {
    }
}
