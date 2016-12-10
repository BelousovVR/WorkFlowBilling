using System.ComponentModel;

namespace WorkFlowBilling.Compiler.Operators
{
    /// <summary>
    /// Ассоциативность оператора
    /// </summary>
    public enum OperatorAssociativity
    {
        [Description("Левоассоциативный оператор")]
        Left = 0,

        [Description("Правоассоциативный оператор")]
        Right = 1
    }
}
