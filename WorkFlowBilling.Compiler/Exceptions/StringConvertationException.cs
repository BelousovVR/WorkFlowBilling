﻿using System;

namespace WorkFlowBilling.Compiler.Exceptions
{
    /// <summary>
    /// Исключение при конвертации строки из инфиксной / постфиксной записи
    /// </summary>
    public class StringConvertationException : Exception
    {
        public StringConvertationException(string message) : base(message) { }
    }
}
