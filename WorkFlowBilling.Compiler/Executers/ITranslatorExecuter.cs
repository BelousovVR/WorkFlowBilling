using WorkFlowBilling.Common.Interfaces;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Executers
{
    /// <summary>
    /// Исполнитель операторов / функций
    /// </summary>
    [AutoInjectable]
    public interface ITranslatorExecuter : IGenericMatchable<string>
    {
        /// <summary>
        /// Выполнить вычисления
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns></returns>
        object Execute(params string[] args);

        /// <summary>
        /// Количество аргументов, требующихся для выполнения
        /// </summary>
        int ParamsCount { get; }
    }
}
