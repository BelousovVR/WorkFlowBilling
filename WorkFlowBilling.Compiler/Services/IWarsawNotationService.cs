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
        /// Преобразовать строку с выражением в инфиксной форме в постфиксную
        /// </summary>
        /// <param name="infixString">Выражение в инфиксной форме</param>
        /// <returns></returns>
        string ConvertToPostfixString(string infixString);
    }
}
