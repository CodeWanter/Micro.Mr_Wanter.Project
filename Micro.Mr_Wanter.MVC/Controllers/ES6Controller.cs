using Micro.Es6.Interface;
using Micro.Es6.Model;
using Micro.Wanter.Common;
using Nest;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    public class ES6Controller : Controller
    {
        private IMainRCJournalInfoService _iService = null;
        public ES6Controller(IMainRCJournalInfoService iBaseService)
        {
            _iService = iBaseService;
        }

        public ActionResult InitES()
        {
            bool result = _iService.CreateIndexAndData(t => true);
            return Json(new { result = result });
        }

        // GET: ES6
        public ActionResult Index(string queryString = "")
        {
            var query = new MatchQuery() //多字段查询
            {
                Field = "TITLE",
                Boost = 2.2,
                Query = queryString
            } || new MatchQuery()
            {
                Field = "PUBLISHING_PLACE",
                Boost = 0.3,
                Query = queryString
            };
            _iService.FactSearcher(query);
            ViewBag.queryString = queryString;
            return View();
        }

        public ActionResult GetData(string queryString = "", int pageIndex = 0, int pageSize = 5)
        {
            pageIndex++;
            var query = new MatchQuery() //多字段查询
            {
                Field = "TITLE",
                Boost = 2.2,
                Query = queryString
            } || new MatchQuery()
            {
                Field = "PUBLISHING_PLACE",
                Boost = 0.3,
                Query = queryString
            };
            //MatchQuery query = new MatchQuery() //单字段查询
            //{
            //    Field = "TITLE ^ 2.2",
            //    Query = queryString
            //};
            List<ISort> sort = new List<ISort> { new SortField { Field = "_score", Order = SortOrder.Descending } };
            PageResult<MainRCJournalInfo> list = _iService.PageResultSearcher(query, pageIndex, pageSize, sort);//分页查询
            PageResult<MainRCJournalInfo> lis2t = _iService.PageResultSearcher2(queryString, pageIndex, pageSize);//分页查询
            return Json(list);
        }

        public ActionResult Detail(int id)
        {
            MainRCJournalInfo model = _iService.FindById(id);
            return View(model);
        }

        public ActionResult Create(string status = "200")
        {
            ViewBag.status = status;
            return View();
        }

        [HttpPost]
        public ActionResult Create(MainRCJournalInfo mainRCJournalInfo)
        {
            int excuteCout = _iService.AddDataScope(mainRCJournalInfo);
            if (excuteCout == 0)
                return RedirectToAction("/Create", false);
            return RedirectToAction("/Index");
        }

        public ActionResult Edit(int id)
        {
            MainRCJournalInfo mainRCJournalInfo = _iService.FindById(id);
            return PartialView(mainRCJournalInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MainRCJournalInfo mainRCJournalInfo)
        {
            DocumentPath<MainRCJournalInfo> deletePath = new DocumentPath<MainRCJournalInfo>(mainRCJournalInfo.ID);
            var result = _iService.EditEntityWidthScope(mainRCJournalInfo, deletePath);
            return Json(new { result = result });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DocumentPath<MainRCJournalInfo> deletePath = new DocumentPath<MainRCJournalInfo>(id);
            bool result = _iService.DeleteRowById(deletePath);
            return Json(new { result = result });
        }
    }
}