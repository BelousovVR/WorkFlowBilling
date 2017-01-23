using System;

namespace WorkFlowBilling.Compiler.Exceptions
{
    /// <summary>
    /// Исключение при оптимизации строки, хранящей выражение в постфиксной форме
    /// </summary>
    public class StringOptimizationException : Exception
    {
        public StringOptimizationException(string message) : base(message) { }
    }
}
