using System.Collections.Generic;

namespace WorkFlowBilling.Common.Extensions
{
    /// <summary>
    /// Расширения для стэка
    /// </summary>
    public static class StackExtensions
    {
        /// <summary>
        /// Вытолкнуть из стэка заданное количество элементов
        /// </summary>
        /// <typeparam name="T">Тип элементов стэка</typeparam>
        /// <param name="stack">Стэк</param>
        /// <param name="count">Количество элементов, которое необходимо вытолкнуть</param>
        /// <returns></returns>
        public static List<T> PopMany<T>(this Stack<T> stack, int count)
        {
            var result = new List<T>();

            for (int i = 0; i < count; i++)
            {
                result.Add(stack.Pop());
            }

            return result;
        }
    }
}
