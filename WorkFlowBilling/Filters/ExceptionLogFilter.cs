using System;
using System.Web.Mvc;
using WorkFlowBilling.Logger.Services;

namespace WorkFlowBilling.Filters
{
    public class ExceptionLogFilter : FilterAttribute, IExceptionFilter
    {
        public ILogger Logger { get; set; }

        private string CreateLogMessage(ExceptionContext filterContext)
        {
            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
            return $"exception message:{exceptionMessage} stack trace:{stackTrace}";  
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (Logger.IsErrorEnabled)
            {
                var message = CreateLogMessage(filterContext);
                Logger.Error(message);
            } 
        }
    }
}