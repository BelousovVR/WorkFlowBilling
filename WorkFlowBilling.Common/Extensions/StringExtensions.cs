using System;
using System.Globalization;
using System.Linq;
using WorkFlowBilling.Common.Helpers;
using static System.Char;

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

        /// <summary>
        /// Попытаться получить decimal из строки. По умолчанию используется en-US CultureInfo
        /// </summary>
        /// <param name="str">Исходная</param>
        /// <param name="number">Полученное число</param>
        /// <returns></returns>
        public static bool TryGetDecimal(this string str, out decimal number, CultureInfo cultureInfo = null)
        {
            if (cultureInfo == null)
                cultureInfo = CultureInfoHelper.English_USA_Culture;

            number = 0;
            var numberDelitimer = cultureInfo.NumberFormat.NumberDecimalSeparator;

            var numberCharsArray = str.TakeWhile(_ => IsDigit(_) || _.ToString() == numberDelitimer).ToArray();
            var numberString = new string(numberCharsArray);

            return decimal.TryParse(numberString,
                                    NumberStyles.AllowDecimalPoint,
                                    cultureInfo,
                                    out number);
        }

        /// <summary>
        /// Разбить строку на составляющие, используя разделители в виде строк
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <param name="options">Опции разбиения</param>
        /// <param name="delitimer">Разделители</param>
        /// <returns></returns>
        public static string[] Split(this string str, StringSplitOptions options, params string[] delitimer)
        {
            return str.Split(delitimer, options);
        }
    }
}
