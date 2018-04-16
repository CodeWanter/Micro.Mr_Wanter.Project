using Micor.Remote.Model;
using Micor.Remote.Service.RmoteSearchService;
using Micro.Wanter.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Micor.Remote.Service
{
    public class SearchService: Interface.ISearch
    {
        private int TotalCout;
        SearchClient esClient;
        public SearchService()
        {
            esClient = new SearchClient();
        }
        /// <summary>
        /// ES查询数据
        /// </summary>
        /// <param name="indexName">索引名</param>
        /// <param name="queryString">检索词</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="fields">返回字段</param>
        /// <param name="pageNumber">第几页</param>
        /// <param name="pagePerNo">显示条数</param>
        /// <param name="fm">返回值类型</param>
        /// <returns></returns>
        public PageResult<ResModel> GetResList(string indexName, string queryString, string sortField, string fields, int pageNumber, int pagePerNo, string fm)
        {
            PageResult<ResModel> list = new PageResult<ResModel>();

            string searchResponse = esClient.EsSearch("", "", indexName, queryString, sortField, fields, pageNumber, pagePerNo, "", fm);

            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = Int32.MaxValue;
            Dictionary<string, object> dic = json.Deserialize<Dictionary<string, object>>(searchResponse);
            List<ResModel> reslist = GetResList(dic,out TotalCout);
            list.DataList = reslist;
            list.TotalCount = TotalCout;
            list.PageSize = pagePerNo;
            list.PageIndex = pageNumber;
            return list;
        }

        public List<ResModel> GetResList(Dictionary<string, object> dic,out int TotalCout)
        {
            List<ResModel> resList = new List<ResModel>();
            int tempCount = 0;
            if (dic != null && dic.Count > 0)
            {
                Dictionary<string, object> hits = dic["hits"] as Dictionary<string, object>;
                if (hits != null && hits.Count > 0)
                {
                    ArrayList hitss = hits["hits"] as ArrayList;
                    tempCount = int.Parse(hits["total"].ToString());
                    if (hitss != null && hitss.Count > 0)
                    {
                        for (int i = 0; i < hitss.Count; i++)
                        {
                            Dictionary<string, object> source = hitss[i] as Dictionary<string, object>;
                            if (source != null && source.Count > 0)
                            {
                                Dictionary<string, object> _source = source["_source"] as Dictionary<string, object>;
                                if (_source != null && _source.Count > 0)
                                {
                                    ResModel res = new ResModel();
                                    res.f_id = Int32.Parse(_source["_ID"].ToString());
                                    res.mid = _source["f_mid"].ToString();
                                    res.Title = _source["f_Title"] == null ? "" : _source["f_Title"].ToString();
                                    res.Author = _source["f_Author"] == null ? "" : _source["f_Author"].ToString();
                                    res.Cbdw = _source["f_Cbdw"] == null ? "" : _source["f_Cbdw"].ToString();
                                    res.Cbsj = _source["f_Cbsj"] == null ? "" : _source["f_Cbsj"].ToString();
                                    res.Bzjg = _source["f_Bzjg"] == null ? "" : _source["f_Bzjg"].ToString();
                                    res.Bzsj = _source["f_Bzsj"] == null ? "" : _source["f_Bzsj"].ToString();
                                    res.Xklb = _source["f_Xklb"] == null ? "" : _source["f_Xklb"].ToString();
                                    res.Type = _source["f_Type"] == null ? "" : _source["f_Type"].ToString();
                                    res.TextAddress = _source["f_TextAddress"] == null ? "" : _source["f_TextAddress"].ToString();
                                    res.PicAddress = _source["f_PicAddress"] == null ? "" : _source["f_PicAddress"].ToString();
                                    res.Introduce = _source["f_Introduce"] == null ? "" : _source["f_Introduce"].ToString();
                                    resList.Add(res);
                                }
                            }
                        }
                    }
                }
            }
            TotalCout = tempCount;
            return resList;
        }
    }
}
