using Micro.Mr_Wanter.Common.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    public class ChartController : Controller
    {
        // GET: Default
        [AuthorityFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}