using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Services;
using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;
using WorkFlowBilling.Compiler.Exceptions;
using System.Globalization;
using WorkFlowBilling.Common.Helpers;
using WorkFlowBilling.Common.Extensions;
using WorkFlowBilling.Compiler.Impl.Helpers;
using WorkFlowBilling.Compiler.Impl.Converters;

namespace WorkFlowBilling.Compiler.Impl.Services
{
    [AutoInjectableInstance(InstanceLifeTime.InstancePerRequest)]
    public class WarsawNotationService : IWarsawNotationService
    {
        // TODO: это возможно надо убрать. А результат возвращать в виде листа элементов
        private const string _Delitimer = " ";

        public IEnumerable<ISignature> Signatures { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public string ConvertToPostfixString(string infixString)
        {
            return new PostfixConverter(infixString, _Delitimer, Signatures).Convert();
        }
    }
}
