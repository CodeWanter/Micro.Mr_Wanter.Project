using Micor.Remote.Interface;
using Micor.Remote.Model;
using Micro.Mr_Wanter.Common.Filter;
using Micro.Wanter.Common;
using PagedList;
using System;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    public class HomeController : Controller
    {
        private ISearch _iSearch = null;

        public HomeController(ISearch iSearch)
        {
            _iSearch = iSearch;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [AuthorityFilter]
        public ActionResult About(string searchString = "*", int pageIndex = 1)
        {
            int pageSize = 10;
            PageResult<ResModel> data = _iSearch.GetResList("dag", "" + searchString + "", "", "", pageIndex - 1, pageSize, "@JSON");
            StaticPagedList<ResModel> pageList = new StaticPagedList<ResModel>(data.DataList, pageIndex, pageSize, data.TotalCount);
            return View(pageList);
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult GetEsData(string searchString, int pageIndex = 0, int pageSize = 10)
        {
            searchString = string.IsNullOrEmpty(searchString) ? "*" : Server.UrlDecode(searchString);
            PageResult<ResModel> data = _iSearch.GetResList("dag", "" + searchString + "", "", "", pageIndex, pageSize, "@JSON");
            return Json(data);
        }
    }
}