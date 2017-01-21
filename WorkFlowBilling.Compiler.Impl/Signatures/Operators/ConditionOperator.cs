using System;
using System.Linq;
using WorkFlowBilling.Common.Extensions;
using WorkFlowBilling.Compiler.Operators;
using WorkFlowBilling.Compiler.Signatures;
using WorkFlowBilling.IoC.Attributes;
using WorkFlowBilling.IoC.Enumerations;
using static System.String;

namespace WorkFlowBilling.Compiler.Impl.Signatures.Operators
{
    /// <summary>
    /// Оператор с условием
    /// </summary>
    [AutoInjectableInstance(InstanceLifeTime.SingleInstance)]
    public class ConditionOperator : OperatorBase
    {
        /// <summary>
        /// Вернуть информацию о совпадающей сигнатуре, если подстрока содержит символ/символы сигнатуры
        /// </summary>
        /// <param name="inputString">Подстрока, начинающаяся с символа сигнатуры</param>
        /// <returns></returns>
        public override SignatureMatchInfo Match(string inputString)
        {
            var signatureMatchInfo = new SignatureMatchInfo
            {
                IsMatched = false,
                MatchedKeyIndex = -1
            };

            if (IsNullOrWhiteSpace(inputString))
                return signatureMatchInfo;

            var trimedString = inputString.TrimStart(' ');

            for (int i = 0; i < Keys.Length; i++)
            {
                var key = Keys[i];
                if (trimedString.StartsWith(key))
                {
                    signatureMatchInfo.IsMatched = true;
                    signatureMatchInfo.MatchedKey = key;
                    signatureMatchInfo.MatchedKeyIndex = i;
                    signatureMatchInfo.MatchedSignature = this;

                    break;
                }
            }

            return signatureMatchInfo;
        }

        public ConditionOperator()
        {
            Keys = new[] { "?", ":" };
            Priority = 3;
            OperatorType = OperatorType.Ternarу;
            Associativity = OperatorAssociativity.Right;
        }
    }
}
