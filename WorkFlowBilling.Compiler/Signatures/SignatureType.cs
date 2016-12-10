using System.ComponentModel;

namespace WorkFlowBilling.Compiler.Signatures
{
    /// <summary>
    /// Тип сигнатуры
    /// </summary>
    public enum SignatureType
    {
        [Description("Оператор")]
        Operator = 0,

        [Description("Функция")]
        Function = 1
    }
}
