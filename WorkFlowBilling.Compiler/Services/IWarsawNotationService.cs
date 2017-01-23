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

        /// <summary>
        /// Оптимизировать строку, содержащую выражение в постфиксной форме 
        /// </summary>
        /// <param name="postfixString">Строка с выражением в постфиксной форме</param>
        /// <returns></returns>
        string OptimizePostfixString(string postfixString);
    }
}
