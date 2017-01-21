namespace WorkFlowBilling.Common.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, поддерживающих метод Match
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IInputOutputGenericMatchable<in TInput, out TOutput>
    {
        /// <summary>
        /// Проверить совпадение и вернуть информацию о совпадении
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TOutput Match(TInput input);
    }
}
