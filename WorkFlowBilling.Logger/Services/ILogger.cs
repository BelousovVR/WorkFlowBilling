using System;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.Logger.Enumerations;

namespace WorkFlowBilling.Logger.Services
{
    /// <summary>
    /// Интерфейс системы логгирования сообщений
    /// </summary>
    [AutoInjectable]
    public interface ILogger
    {
        /// <summary>
        /// Сохранить данные в лог
        /// </summary>
        /// <param name="messageLevel">Уровень сообщения</param>
        /// <param name="Message">Сообщение</param>
        void SaveToLog(LogMessageLevel messageLevel, string Message);

        /// <summary>
        /// Сохранить данные в лог, если логгер позволяет записывать сообщения с указанным уровнем
        /// Следует использовать для тех случаев, когда сбор данных для записи в лог
        /// является трудоемкой операцией
        /// </summary>
        /// <param name="messageLevel">Уровень сообщения</param>
        /// <param name="getMessage">Функция, возвращающая сообщение</param>
        void SaveToLogIfLevelEnabled(LogMessageLevel messageLevel, Func<string> getMessage);

        /// <summary>
        /// Сохранить данные в логе с уровнем Trace
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Trace(string message);

        /// <summary>
        /// Сохранить данные в логе с уровнем Debug
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Debug(string message);

        /// <summary>
        /// Сохранить данные в логе с уровнем Info
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Info(string message);

        /// <summary>
        /// Сохранить данные в логе с уровнем Warning
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Warning(string message);

        /// <summary>
        /// Сохранить данные в логе с уровнем Error
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Error(string message);

        /// <summary>
        /// Сохранить данные в логе с уровнем Fatal
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Fatal(string message);

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Trace
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Debug
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Info
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Warn
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Error
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Доступно ли сохранение сообщений с уровнем Fatal
        /// </summary>
        bool IsFatalEnabled { get; }
    }
}
