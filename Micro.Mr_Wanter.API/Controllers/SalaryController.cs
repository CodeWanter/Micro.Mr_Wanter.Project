using Micro.Wanter.Common;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;
using Micro.Wanter.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Micro.Mr_Wanter.API.Controllers
{
    public class SalaryController : ApiController
    {
        private ISalaryService salaryService ;

        public SalaryController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        [HttpGet]
        //[EnableCors(origins: "http://localhost:8002/Salary/index", headers: "*", methods: "GET,POST,PUT,DELETE")]
        public PageResult<Salary> GetSalary(int userid,int pageIndex = 0, int pageSize = 10)
        {
            
            PageResult<Salary> list = salaryService.GetResList<Salary, DateTime>(o => o.SalaryTime, o => o.UserId == userid, pageIndex * pageSize, pageSize, false);
            return list;
        }

        [HttpPost]
        public bool Create(int userid,Salary salary)
        {
            salary.CreateTime = DateTime.Now;
            salary.UserId = userid;
            bool result = salaryService.AddEntity(salary);
            return result;
        }

        [HttpPost]
        public Salary detail(int id)
        {
            Salary salary = salaryService.FindById<Salary>(id);
            return salary;
        }

        [HttpPost]
        [System.Web.Mvc.ValidateInput(false)]
        public bool Edit(Salary salary)
        {
            bool result = salaryService.EditEntity(salary);
            return result;
        }
    }
}
