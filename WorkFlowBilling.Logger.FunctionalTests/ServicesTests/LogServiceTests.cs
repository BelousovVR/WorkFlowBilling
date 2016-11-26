using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkFlowBilling.Logger.Enumerations;
using WorkFlowBilling.TestFramework.Common;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ILogger = WorkFlowBilling.Logger.Services.ILogger;
using WorkFlowBilling.Common.Extensions;

namespace WorkFlowBilling.Logger.FunctionalTests.ServicesTests
{
    [TestFixture]
    public class LogServiceTests : BaseFunctionalTest
    {
        private const string clearLogsQuery = "DELETE FROM dbo.Logs";
        private const string getLogsQuery = "SELECT Message, Level FROM dbo.Logs";
        private static string connectionString = ConfigurationManager.ConnectionStrings["Logs"].ConnectionString;

        /// <summary>
        /// Структура, содержащий обязательные данные по логу - уровень и сообщение
        /// </summary>
        private struct LogMainInfo
        {
            public LogMessageLevel Level { get; set; }
            public string Message { get; set; }
        }

        /// <summary>
        /// Очистить базу данных логов. 
        /// </summary>
        private void ClearLogDataBase()
        {
            // Так как Logger может записывать данные в БД напрямую, без использования фреймворков,
            // То функциональные тесты логгера не должны содержать ссылку на какой-либо фреймворк.
            // Все действия с БД должны проводиться на максимально низком уровне.
            // В данном случае используется ADO.NET

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(clearLogsQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            };
        }

        /// <summary>
        /// Получить проверяемые данные из БД
        /// </summary>
        /// <returns></returns>
        private List<LogMainInfo> GetLogMainInfos()
        {
            // Так как Logger может записывать данные в БД напрямую, без использования фреймворков,
            // То функциональные тесты логгера не должны содержать ссылку на какой-либо фреймворк.
            // Все действия с БД должны проводиться на максимально низком уровне.
            // В данном случае используется ADO.NET

            var result = new List<LogMainInfo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getLogsQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new LogMainInfo
                    {
                        Level =  ((string) reader["Level"]).ParseEnum<LogMessageLevel>(),
                        Message = (string) reader["Message"]
                    });

                   
                }
                reader.Close();
            }

            return result;
        }

          private static LogMainInfo[] SaveToLogCases = new[]
        {
            new LogMainInfo()
            {
                Level = LogMessageLevel.Trace,
                Message = "this is trace message"
            },
            new LogMainInfo()
            {
                Level = LogMessageLevel.Debug,
                Message = "this is debug message"
            },
            new LogMainInfo()
            {
                Level = LogMessageLevel.Info,
                Message = "this is info message"
            },
            new LogMainInfo()
            {
                Level = LogMessageLevel.Warning,
                Message = "this is warning message"
            },
            new LogMainInfo()
            {
                Level = LogMessageLevel.Error,
                Message = "this is error message"
            },
            new LogMainInfo()
            {
                Level = LogMessageLevel.Fatal,
                Message = "this is fatal message",
            },
        };

        private static List<TestCaseData> GetSaveToLogTestCases()
        {
            var saveToLogTestCases = SaveToLogCases.Select(_ =>
            {
                var testCaseData = new TestCaseData(_.Level, _.Message);
                testCaseData.SetName($"LogServiceTests_SaveToLog_{_.Level}");
                return testCaseData;
            })
            .ToList();

            return saveToLogTestCases;
        }

        /// <summary>
        /// Перед запуском каждого теста
        /// </summary>
        [SetUp]
        public override void BeforeTestRun()
        {
            base.BeforeTestRun();
            ClearLogDataBase();
        }

        /// <summary>
        /// Регистрация типов для DI
        /// </summary>
        public override void RegisterAssemblies()
        {
            ContainerManager.RegisterAssembliesTypes(typeof(Impl.AssemblyRef).Assembly);
        }

        public ILogger Logger { get; set; }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Trace_TraceEnabled()
        {
            //GIVEN
            var message = "this is trace message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Trace
                }
            };

            //WHEN
            Logger.Trace(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Debug_DebugEnabled()
        {
            //GIVEN
            var message = "this is debug message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Debug
                }
            };

            //WHEN
            Logger.Debug(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Info_InfoEnabled()
        {
            //GIVEN
            var message = "this is info message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Info
                }
            };

            //WHEN
            Logger.Info(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Warn_WarnEnabled()
        {
            //GIVEN
            var message = "this is warning message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Warning
                }
            };

            //WHEN
            Logger.Warning(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Error_ErrorEnabled()
        {
            //GIVEN
            var message = "this is error message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Error
                }
            };

            //WHEN
            Logger.Error(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Fatal_FatalEnabled()
        {
            //GIVEN
            var message = "this is fatal message";
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = LogMessageLevel.Fatal
                }
            };

            //WHEN
            Logger.Fatal(message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }

        [Test, TestCaseSource(nameof(GetSaveToLogTestCases))]
        [Category("Functional")]
        public void LogServiceTests_SaveToLog_Parametrized(LogMessageLevel level, string message)
        {
            var awaitedResult = new List<LogMainInfo> {
                new LogMainInfo()
                {
                    Message = message,
                    Level = level
                }
            };

            //WHEN
            Logger.SaveToLog(level, message);

            //THEN
            var logs = GetLogMainInfos();

            Assert.AreEqual(awaitedResult, logs);
        }
    }
}