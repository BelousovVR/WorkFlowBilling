using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;

namespace WorkFlowBilling.App_Start
{
    /// <summary>
    /// Конфиг переключателя между преобразователями Less-кода
    /// https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/How-to-upgrade-applications-to-version-2.X
    /// </summary>
    public class JsEngineSwitcherConfig
    {
        public static void Configure(JsEngineSwitcher engineSwitcher)
        {
            engineSwitcher.EngineFactories.AddV8();
            engineSwitcher.DefaultEngineName = V8JsEngine.EngineName;
        }
    }
}