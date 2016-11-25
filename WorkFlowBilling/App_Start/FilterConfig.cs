using System.Web;
using System.Web.Mvc;
using WorkFlowBilling.Filters;

namespace WorkFlowBilling
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionLogFilter());
        }
    }
}
