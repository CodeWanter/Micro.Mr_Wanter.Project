using Micro.Es6.Service;
using Micro.Wanter.Common.Helper;
using Micro.Wanter.Common.IOCFactory;
using System;
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

            string targetDir = string.Format(@"D:\工作\elasticsearch\elasticsearch-2.4.2\bin");
            string args = string.Format(@"/K D:\工作\elasticsearch\elasticsearch-2.4.2\bin\elasticsearch.bat");
            ExcuteBat.Bat(targetDir,args);//启动es2.4.2服务
            Database.SetInitializer<DbContextEntity>(null);
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//替换默认的控制器工厂
        }
    }
}
