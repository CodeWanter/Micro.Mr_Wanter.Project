using Micro.Mr_Wanter.Common.Filter;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    [AuthorityFilter]
    public class StaticController : Controller
    {
        private ISalaryService salaryService = null;
        public StaticController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }
        // GET: Static 统计图表页面加载
        public ActionResult Index()
        {
            S_User user = Session["CurrentUser"] as S_User;
            List<Salary> list = salaryService.GetEntityList<Salary>(s => s.UserId == user.id);
            ViewBag.TotalSalary = list.Sum(s => s.TotalSalary);
            ViewBag.FinalSalary = list.Sum(s => s.FinalSalary);
            return View();
        }
        /// <summary>
        /// 获取图表加载数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        {
            S_User user = Session["CurrentUser"] as S_User;
            List<Salary> list = salaryService.GetEntityList<Salary>(ss => ss.UserId == user.id);
            ArrayList totalBox = new ArrayList();
            foreach (Salary item in list)
            {
                ArrayList temp = new ArrayList();
                temp.Add(getUTCTime(item.SalaryTime));
                temp.Add(item.FinalSalary);
                totalBox.Add(temp);
            }
            ArrayList finalBox = new ArrayList();
            foreach (Salary item in list)
            {
                ArrayList temp = new ArrayList();
                temp.Add(getUTCTime(item.SalaryTime));
                temp.Add(item.TotalSalary);
                finalBox.Add(temp);
            }
            return Json(new { f_data = totalBox, t_data = finalBox }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取发薪时间的UTC毫秒值（highstock使用）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public long getUTCTime(DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}