using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new Micro.Wanter.Common.Filter.LogExceptionFilter());
        }
    }
}
