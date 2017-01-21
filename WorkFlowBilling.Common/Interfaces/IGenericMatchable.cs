namespace WorkFlowBilling.Common.Interfaces
{
    /// <summary>
    /// Упрощенный интерфейс для классов, поддерживающих метод Match и возвращающих логическое значение
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public interface IGenericMatchable<in TInput> : IInputOutputGenericMatchable<TInput, bool>
    {
    }
}
