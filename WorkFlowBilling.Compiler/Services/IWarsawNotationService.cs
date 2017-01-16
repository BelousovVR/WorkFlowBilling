using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Services
{
    /// <summary>
    /// Сервис для работы с обратной польской записью
    /// </summary>
    [AutoInjectable]
    public interface IWarsawNotationService
    {
        /// <summary>
        /// Сконвертировать строку, содержащюю выражение в инфиксной форме в постфиксную форму
        /// </summary>
        /// <param name="infixString">Строка, содержащяя выражение в инфиксной форме</param>
        /// <returns></returns>
        string ConvertToPostfixString(string infixString);
    }
}
