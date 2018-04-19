using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
