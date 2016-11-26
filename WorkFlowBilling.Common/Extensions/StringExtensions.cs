using System;

namespace WorkFlowBilling.Common.Extensions
{
    /// <summary>
    /// Расширения для string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Получить перечисление из строки
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(this string str)
            where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), str);
        }
    }
}
