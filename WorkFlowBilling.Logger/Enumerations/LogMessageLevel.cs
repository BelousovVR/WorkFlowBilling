﻿using System.ComponentModel;

namespace WorkFlowBilling.Logger.Enumerations
{
    /// <summary>
    /// Уровни сообщений лога
    /// </summary>
    public enum LogMessageLevel
    {
        /// <summary>
        /// Трассировочное сообщение. Используется при отладке приложения
        /// содержит очень подробную информацию
        /// </summary>
        [Description("Трассировочное сообщение")]
        Trace = 0,

        /// <summary>
        /// Отладочное сообщение. Используется при отладке приложения
        /// </summary>
        [Description("Отладочное сообщение")]
        Debug = 1,

        /// <summary>
        /// Информационное сообщение. Используется для получения информации
        /// о состоянии приложения
        /// </summary>
        [Description("Информационное сообщение")]
        Info = 2,

        /// <summary>
        /// Предупреждение. Сообщение о ситуации, потенциально приводящей к ошибке
        /// </summary>
        [Description("Предупреждение")]
        Warning = 3,

        /// <summary>
        /// Ошибка. Сообщение об ошибке, не нарушающей работоспособности приложения
        /// </summary>
        [Description("Ошибка")]
        Error = 4,

        /// <summary>
        /// Критическая ошибка. Сообщение об ошибке, нарушающей работоспособность приложения
        /// </summary>
        [Description("Критическая ошибка")]
        Fatal = 5
    }
}
