using System.Collections.Generic;
using WorkFlowBilling.Compiler.Services;
using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;
using WorkFlowBilling.Compiler.Impl.Helpers;
using WorkFlowBilling.Compiler.Impl.Converters;

namespace WorkFlowBilling.Compiler.Impl.Services
{
    [AutoInjectableInstance(InstanceLifeTime.InstancePerRequest)]
    public class WarsawNotationService : IWarsawNotationService
    {
        private readonly string _Delitimer = TranslationConstantHelper.ElementsDelitimer;

        public IEnumerable<ISignature> Signatures { get; set; }

        /// <summary>
        /// Сконвертировать строку, содержащюю выражение в инфиксной форме в постфиксную форму
        /// </summary>
        /// <param name="infixString">Строка, содержащяя выражение в инфиксной форме</param>
        /// <returns></returns>
        public string ConvertToPostfixString(string infixString)
        {
            return new PostfixConverter(infixString, _Delitimer, Signatures).Convert();
        }
    }
}
