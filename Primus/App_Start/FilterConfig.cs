using System.Web;
using System.Web.Mvc;
using Primus.Filters;

namespace Primus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // custom error handler
            filters.Add(new ErrorHandlerFilter());
        }
    }
}
