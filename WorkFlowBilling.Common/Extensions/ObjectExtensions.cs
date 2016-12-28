using System.Linq;

namespace WorkFlowBilling.Common.Extensions
{
    /// <summary>
    /// Класс, содержащий расширения для System.Object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Проверить, входит ли объект в список значений
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="parameters">Список значений</param>
        /// <returns></returns>
        public static bool In(this object obj, params object[] parameters)
        {
            return parameters.Any(_ => _.Equals(obj));
        }

        /// <summary>
        /// Проверить, что объект не входит в список значений
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="parameters">Список значений</param>
        /// <returns></returns>
        public static bool NotIn(this object obj, params object[] parameters)
        {
            return !obj.In(parameters);
        }
    }
}
