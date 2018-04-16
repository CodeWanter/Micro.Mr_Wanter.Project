using Micro.Mr_Wanter.Common.Filter;
using Micro.Wanter.Common;
using Micro.Wanter.Common.ViewResult;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    [AuthorityFilter]
    public class SalaryController : Controller
    {
        private ISalaryService salaryService = null;

        public SalaryController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }
        /// <summary>
        /// 薪水页面加载
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            S_User user = Session["CurrentUser"] as S_User;
            List<Salary> list = salaryService.GetEntityList<Salary>(s => s.UserId == user.id);

            ViewBag.TotalSalary = list.Sum(s => s.TotalSalary);
            ViewBag.FinalSalary = list.Sum(s => s.FinalSalary);
            return View();
        }
        //请求薪水列表数据
        public ActionResult GetSalary(int pageIndex = 0, int pageSize = 10)
        {
            S_User user = Session["CurrentUser"] as S_User;
            PageResult<Salary> list = salaryService.GetResList<Salary, DateTime>(o => o.SalaryTime, o => o.UserId==user.id, pageIndex * pageSize, pageSize, false);
            return Json(list);
        }
        /// <summary>
        /// 添加薪水信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string status = "200")
        {
            ViewBag.status = status;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Salary salary)
        {
            S_User user = Session["CurrentUser"] as S_User;
            salary.CreateTime = DateTime.Now;
            salary.UserId = user.id;
            bool result = salaryService.AddEntity(salary);
            if (!result)
                return RedirectToAction("/Create", result);
            return RedirectToAction("/Index");
        }

        public ActionResult Edit(int id)
        {
            Salary salary = salaryService.FindById<Salary>(id);
            return PartialView(salary);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Salary salary)
        {
            bool result = salaryService.EditEntity(salary);
            return Json(new { result = result });
        }

        public ActionResult Detail(int id)
        {
            Salary salary = salaryService.FindById<Salary>(id);
            return View(salary);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Salary salary = salaryService.FindById<Salary>(id);
            bool result = salaryService.DelEntity(salary);
            return Json(new { result = result });
        }
        //导出excel
        public ExcelResult<Salary> ExportToExcel()
        {
            List<Salary> list = salaryService.GetEntityList<Salary>(s => true);
            return new ExcelResult<Salary>(list);
        }
    }
}