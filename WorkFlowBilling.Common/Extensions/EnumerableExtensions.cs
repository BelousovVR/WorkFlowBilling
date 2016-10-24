using System;
using System.Collections.Generic;

namespace WorkFlowBilling.Common.Extensions
{
    /// <summary>
    /// Расширения для Enumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Выполнить действие Action для каждого элемента Enumerable
        /// </summary>
        /// <typeparam name="T">Тип членов Enumerable</typeparam>
        /// <param name="enumerable">Enumerable</param>
        /// <param name="action">Действие</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
            where T : class
        {
            foreach (var item in enumerable)
                action(item);
        }
    }
}
