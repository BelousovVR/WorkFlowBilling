using System.ComponentModel;

namespace WorkFlowBilling.Compiler.Impl.Converters
{
    /// <summary>
    /// Обработанный синтаксический тип
    /// </summary>
    public enum ProcessedStringType
    {
        [Description("Неопределен")]
        Unknown,

        [Description("Число")]
        Number,

        [Description("Разделитель параметров функции")]
        FunctionArgumentDelitimer,

        [Description("Открывающая скобка")]
        LeftBracket,

        [Description("Закрывающая скобка")]
        RightBracket,

        [Description("Переменная")]
        Variable,

        [Description("Оператор")]
        Operator,

        [Description("Функция")]
        Function
    }
}
