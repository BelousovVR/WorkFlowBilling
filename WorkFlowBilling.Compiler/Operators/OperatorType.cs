using System.ComponentModel;

namespace WorkFlowBilling.Compiler.Operators
{
    /// <summary>
    /// Тип оператора
    /// </summary>
    public enum OperatorType
    {
        [Description("Унарный оператор")]
        Unary = 0,

        [Description("Бинарный оператор")]
        Binary = 1,

        [Description("Тернарный оператор")]
        Ternarу = 2
    }
}
