using System.ComponentModel;

namespace WorkFlowBilling.IoC.Enumerations
{
    /// <summary>
    /// Время жизни экземпляра класса
    /// </summary>
    public enum InstanceLifeTime
    {
        [Description("Единый экземпляр")]
        SingleInstance = 0,

        [Description("Новый экземлпяр на контекст")]
        InstancePerLifeTimeScope = 1,

        [Description("Новый экземпляр на запрос")]
        InstancePerRequest = 2
    }
}
