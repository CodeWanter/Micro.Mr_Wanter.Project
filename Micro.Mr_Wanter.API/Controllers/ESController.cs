using Micro.Es6.Interface;
using Micro.Es6.Model;
using Micro.Es6.Service;
using Micro.Wanter.Common;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;

namespace Micro.Mr_Wanter.API.Controllers
{
    public class ESController : ApiController
    {
        private IMainRCJournalInfoService _iService;
        public ESController(IMainRCJournalInfoService iBaseService)
        {
            _iService = iBaseService;
        }

        /// <summary>
        /// 初始化es数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool InitES()
        {
            bool result = _iService.CreateIndexAndData(t => true);
            return result;
        }
        /// <summary>
        /// 获取检索数据
        /// </summary>
        /// <param name="queryString">检索条件</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        [HttpGet]
        public PageResult<MainRCJournalInfo> GetData(string queryString = "", int pageIndex = 0, int pageSize = 5)
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
            List<ISort> sort = new List<ISort> { new SortField { Field = "_score", Order = SortOrder.Descending } };
            PageResult<MainRCJournalInfo> list = _iService.PageResultSearcher(query, pageIndex, pageSize, sort);//分页查询
            return list;
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>数据映射model</returns>
        [HttpPost]
        //[HttpGet]
        public MainRCJournalInfo Detail([FromBody]int id)
        {
            MainRCJournalInfo model = _iService.FindById(id);
            return model;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="mainRCJournalInfo">添加数据对象</param>
        /// <returns></returns>
        [HttpPost]
        public int Create(MainRCJournalInfo mainRCJournalInfo)
        {
            int excuteCout = _iService.AddDataScope(mainRCJournalInfo);
            return excuteCout;
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="mainRCJournalInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Edit(MainRCJournalInfo mainRCJournalInfo)
        {
            DocumentPath<MainRCJournalInfo> deletePath = new DocumentPath<MainRCJournalInfo>(mainRCJournalInfo.ID);
            var result = _iService.EditEntityWidthScope(mainRCJournalInfo, deletePath);
            return result;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Delete(int id)
        {
            DocumentPath<MainRCJournalInfo> deletePath = new DocumentPath<MainRCJournalInfo>(id);
            bool result = _iService.DeleteRowById(deletePath);
            return result;
        }
    }
}
