using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.Common.Filter
{
    public class AuthorityFilter: AuthorizeAttribute
    {
        /// <summary>
        /// 未登录时返还的地址
        /// </summary>
        private string _LoginPath = "";
        public AuthorityFilter()
        {
            this._LoginPath = "/Account/Login";
        }

        public AuthorityFilter(string loginPath)
        {
            this._LoginPath = loginPath;
        }
        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;//表示支持控制器、action的AllowAnonymousAttribute
            }
          
             var sessionUser = HttpContext.Current.Session["CurrentUser"];//使用session
          
            if (sessionUser == null )
            {
                HttpContext.Current.Session["CurrentUrl"] = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(this._LoginPath);
            }
        }
    }
}