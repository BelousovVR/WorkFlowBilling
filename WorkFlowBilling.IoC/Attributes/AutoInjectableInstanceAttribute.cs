using System;
using WorkFlowBilling.IoC.Enumerations;

namespace WorkFlowBilling.IoC.Attributes
{
    /// <summary>
    /// Атрибут, помечающий класс, как подставляемый автоматически,
    /// для интерфейса, помеченного AutoInjectable аттрибутом
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AutoInjectableInstanceAttribute : Attribute
    {
        public InstanceLifeTime InstanceLifeTime { get; private set; }

        public AutoInjectableInstanceAttribute(InstanceLifeTime lifeTime = InstanceLifeTime.SingleInstance)
        {
            InstanceLifeTime = lifeTime;
        }
    }
}
