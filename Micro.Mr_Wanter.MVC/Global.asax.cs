using Micro.Es6.Service;
using Micro.Wanter.Common.IOCFactory;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Micro.Mr_Wanter.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<DbContextEntity>(null);

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//替换默认的控制器工厂
        }
    }
}
