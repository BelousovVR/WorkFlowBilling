using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WorkFlowBilling.Logger.Enumerations;
using WorkFlowBilling.TestFramework.Common;
using ILogger = WorkFlowBilling.Logger.Services.ILogger;

namespace WorkFlowBilling.Logger.FunctionalTests.ServicesTests
{
    [TestFixture]
    public class LogServiceTests : BaseFunctionalTest
    {
        private static string GetLogFileFullPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            var uriBuilder = new UriBuilder(codeBase);
            var normalizedPath = Uri.UnescapeDataString(uriBuilder.Path);
            var binDir = Path.GetDirectoryName(normalizedPath);

            return binDir + "\\logs\\LoggerFunctionalTests.txt";
        }

        private class SaveToLogTestCase
        {
            public LogMessageLevel LogMessageLevel { get; set; }
            public string Message { get; set; }
            public string AwaitedLogMessage { get; set; }
        }

        private static SaveToLogTestCase[] SaveToLogCases = new[]
        {
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Trace,
                Message = "this is trace message",
                AwaitedLogMessage = "Level: Trace; Message: this is trace message"
            },
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Debug,
                Message = "this is debug message",
                AwaitedLogMessage = "Level: Debug; Message: this is debug message"
            },
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Info,
                Message = "this is info message",
                AwaitedLogMessage = "Level: Info; Message: this is info message"
            },
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Warning,
                Message = "this is warning message",
                AwaitedLogMessage = "Level: Warn; Message: this is warning message"
            },
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Error,
                Message = "this is error message",
                AwaitedLogMessage = "Level: Error; Message: this is error message"
            },
            new SaveToLogTestCase()
            {
                LogMessageLevel = LogMessageLevel.Fatal,
                Message = "this is fatal message",
                AwaitedLogMessage = "Level: Fatal; Message: this is fatal message"
            },
        };

        private static List<TestCaseData> GetSaveToLogTestCases()
        {
            var saveToLogTestCases = SaveToLogCases.Select(_ =>
            {
                var testCaseData = new TestCaseData(_.LogMessageLevel, _.Message, _.AwaitedLogMessage);
                testCaseData.SetName($"LogServiceTests_SaveToLog_{_.LogMessageLevel}");
                return testCaseData;
            })
            .ToList();

            return saveToLogTestCases;
        }

        // Путь к bin-файлу
        private static string LogFileFullPath = GetLogFileFullPath();

        public ILogger Logger { get; set; }

        public override void RegisterAssemblies()
        {
            ContainerManager.RegisterAssembliesTypes(typeof(Impl.AssemblyRef).Assembly);
        }

        [SetUp]
        public override void BeforeTestRun()
        {
            base.BeforeTestRun();

            if (File.Exists(LogFileFullPath))
                File.Delete(LogFileFullPath);
        }
       
        [Test]
        [Category("Functional")]
        public void LogServiceTests_Trace_TraceEnabled()
        {
            //GIVEN
            var message = "this is trace message";
            var awaitedLines = new string[]
            {
                "Level: Trace; Message: this is trace message"
            };

            //WHEN
            Logger.Trace(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Debug_DebugEnabled()
        {
            //GIVEN
            var message = "this is debug message";
            var awaitedLines = new string[]
            {
                "Level: Debug; Message: this is debug message"
            };

            //WHEN
            Logger.Debug(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Info_InfoEnabled()
        {
            //GIVEN
            var message = "this is info message";
            var awaitedLines = new string[]
            {
                "Level: Info; Message: this is info message"
            };

            //WHEN
            Logger.Info(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Warn_WarnEnabled()
        {
            //GIVEN
            var message = "this is warning message";
            var awaitedLines = new string[]
            {
                "Level: Warn; Message: this is warning message"
            };

            //WHEN
            Logger.Warning(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Error_ErrorEnabled()
        {
            //GIVEN
            var message = "this is error message";
            var awaitedLines = new string[]
            {
                "Level: Error; Message: this is error message"
            };

            //WHEN
            Logger.Error(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test]
        [Category("Functional")]
        public void LogServiceTests_Fatal_FatalEnabled()
        {
            //GIVEN
            var message = "this is fatal message";
            var awaitedLines = new string[]
            {
                "Level: Fatal; Message: this is fatal message"
            };

            //WHEN
            Logger.Fatal(message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }

        [Test, TestCaseSource(nameof(GetSaveToLogTestCases))]
        [Category("Functional")]
        public void LogServiceTests_SaveToLog_Parametrized(LogMessageLevel level, string message, string awaitedMessage)
        {
            //GIVEN
            var awaitedLines = new string[]
            {
                awaitedMessage
            };

            //WHEN
            Logger.SaveToLog(level, message);

            //THEN
            var lines = File.ReadAllLines(LogFileFullPath);
            Assert.AreEqual(awaitedLines, lines);
        }
    }
}