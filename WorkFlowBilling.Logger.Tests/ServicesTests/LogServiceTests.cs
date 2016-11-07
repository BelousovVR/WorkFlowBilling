using NUnit.Framework;
using WorkFlowBilling.Logger.Impl.Services;
using ILogger = WorkFlowBilling.Logger.Services.ILogger;

namespace WorkFlowBilling.Logger.Tests.ServicesTests
{
    // TODO: требуется базовый класс для Injected-тестов
    /// <summary>
    /// Тесты на сервис логгирования
    /// </summary>
    [TestFixture]
    public class LogServiceTests
    {
        // TODO: это надо делать при каждой инициализации тестового метода
        private ILogger Logger { get; set; } = new LogService();

        [Test]
        public void LogServiceTests_SaveToLog_Trace()
        {
            Logger.Trace("hi bro!1");

            Assert.True(true);
        }

    }
}
