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
        {
            foreach (var item in enumerable)
                action(item);
        }

        /// <summary>
        /// Проверить последовательность на null и существование элементов
        /// </summary>
        /// <typeparam name="T">Тип членов Enumerable</typeparam>
        /// <param name="enumerable">Enumerable</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            var itemsExists = false;

            foreach (var item in enumerable)
            {
                itemsExists = true;
                break;
            }

            return !itemsExists;
        }
    }
}
