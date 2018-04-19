using Autofac;
using Micro.Wanter.Service;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using Micro.Wanter.Interface;
using Autofac.Integration.WebApi;
using System.Web.Compilation;
using System.Linq;

namespace Micro.Mr_Wanter.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<DbContextEntity>(null);

            var builder = new ContainerBuilder();
           builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t=>t.Name.EndsWith("Service")).AsImplementedInterfaces();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            //ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());//替换默认的控制器工厂
        }
    }
}
