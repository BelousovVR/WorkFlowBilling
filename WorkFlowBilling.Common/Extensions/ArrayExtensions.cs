using System;

namespace WorkFlowBilling.Common.Extensions
{
    /// <summary>
    /// Расширения для массивов
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Определить, присутствует ли элемент в массиве
        /// </summary>
        /// <typeparam name="T">Тип элементов массива</typeparam>
        /// <param name="array">Массив</param>
        /// <param name="predicate">Предикат</param>
        /// <returns></returns>
        public static bool Exists<T>(this T[] array, Predicate<T> predicate)
        {
            return Array.Exists(array, predicate);
        }
    }
}
