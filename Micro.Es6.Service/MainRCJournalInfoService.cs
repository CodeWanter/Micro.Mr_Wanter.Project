using Micro.Es6.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Micro.Wanter.Common;
using Micro.Es6.Model;
using System.Linq.Expressions;

namespace Micro.Es6.Service
{
    public class MainRCJournalInfoService : BaseService<MainRCJournalInfo>, IMainRCJournalInfoService
    {
        private static ElasticClient client = ElasticsearchHelper.GetElasticClient("mainrcjournalinfo");//索引选择

        public bool CreateIndexAndData(Expression<Func<MainRCJournalInfo, bool>> WhereLambda)
        {
            string IndexName = "MainRCJournalInfo";
            var list = base.GetEntityList(WhereLambda);
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 0,//副本数
                    NumberOfShards = -1//分片数
                }
            };
            CreateIndex(client, indexState, IndexName);
            var addResult = client.IndexMany(list).IsValid;//添加多条记录
            return addResult;
        }

        /// <summary>
        /// 获取分页数据对象PageResult 并添加高亮
        /// </summary>
        /// <param name="client"></param>
        /// <param name="Query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public PageResult<MainRCJournalInfo> PageResultSearcher(QueryContainer Query, int pageIndex, int pageSize, List<ISort> sort)
        {
            var s_result = client.Search<MainRCJournalInfo>(
            new SearchRequest()
            {
                Sort = sort,
                Size = pageSize,
                From = (pageIndex - 1) * pageSize,
                Query = Query,
                Highlight = new Highlight()
                {
                    PreTags = new[] { "<b>" },
                    PostTags = new[] { "</b>" },
                    Encoder = "Html",
                    Fields = new Dictionary<Field, IHighlightField>()
                    {
                            {
                                "TITLE",new HighlightField
                                {
                                    Type=HighlighterType.Plain,
                                    ForceSource = true,
                                    FragmentSize = 150,
                                    NumberOfFragments =3,
                                    NoMatchSize=150
                                }
                            },
                             {
                                "PUBLISHING_PLACE",new HighlightField
                                {
                                    Type=HighlighterType.Plain,
                                    ForceSource = true,
                                    FragmentSize = 150,
                                    NumberOfFragments =3,
                                    NoMatchSize=150
                                }
                            }
                    }
                }
            });
            var list = s_result.Hits.Select(c => new MainRCJournalInfo()
            {
                ID = c.Source.ID,
                TITLE = c.Highlights == null ? c.Source.TITLE : c.Highlights.Keys.Contains("TITLE") ? string.Join("", c.Highlights["TITLE"].Highlights) : c.Source.TITLE,
                PUBLISHING_PLACE = c.Highlights == null ? c.Source.TITLE : c.Highlights.Keys.Contains("PUBLISHING_PLACE") ? string.Join("", c.Highlights["PUBLISHING_PLACE"].Highlights) : c.Source.TITLE,
                PUBLISHER = c.Source.PUBLISHER,
                PUBLISHING_PERIOD = c.Source.PUBLISHING_PERIOD,
                Score = c.Score.ToString()
            });
            PageResult<MainRCJournalInfo> result = new PageResult<MainRCJournalInfo>()
            {
                TotalCount = PageCount(client, Query),
                PageIndex = pageIndex,
                PageSize = pageSize,
                DataList = list.ToList()
            };
            return result;
        }

        /// <summary>
        /// 高亮设置失败的方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="queryString"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageResult<MainRCJournalInfo> PageResultSearcher2(string queryString, int pageIndex, int pageSize)
        {
            var query = new SearchDescriptor<MainRCJournalInfo>();
            query.PostFilter(x => x.Match(f => f.Field(obj => obj.TITLE).Query(queryString)));
            query.Highlight(h => h
                                        .PreTags("<b>")
                                        .PostTags("</<b>")
                                        .Encoder("Html")
                                        .Fields(
                                                f => f.Field(obj => obj.TITLE),
                                                f => f.Field(obj => obj.PUBLISHING_PLACE),
                                                f => f.Field("_all")
                                        )
            );
            query.Sort(x => x.Field("_score", SortOrder.Descending));
            var s_result = client.Search<MainRCJournalInfo>(x => query.Size(pageSize).From((pageIndex - 1) * pageSize));

            var list = s_result.Hits.Select(c => new MainRCJournalInfo()
            {
                ID = c.Source.ID,
                TITLE = c.Highlights == null ? c.Source.TITLE : c.Highlights.Keys.Contains("TITLE") ? string.Join("", c.Highlights["TITLE"].Highlights) : c.Source.TITLE,
                PUBLISHING_PLACE = c.Highlights == null ? c.Source.TITLE : c.Highlights.Keys.Contains("publishing_place") ? string.Join("", c.Highlights["publishing_place"].Highlights) : c.Source.TITLE,
                PUBLISHER = c.Source.PUBLISHER,
                PUBLISHING_PERIOD = c.Source.PUBLISHING_PERIOD,
            });
            PageResult<MainRCJournalInfo> result = new PageResult<MainRCJournalInfo>()
            {
                TotalCount = PageCount2(client, queryString, new SearchDescriptor<MainRCJournalInfo>()),
                PageIndex = pageIndex,
                PageSize = pageSize,
                DataList = list.ToList()
            };
            return result;
        }

        public bool DeleteRowById(DocumentPath<MainRCJournalInfo> document)
        {
            return base.DeleteRowById(client, document);
        }

        public List<MainRCJournalInfo> FactSearcher(QueryContainer Query)
        {
            int pageSize = 5000;
            var result = client.Search<MainRCJournalInfo>
                (
                m => m
                .Query(q => Query)
                .From(0)
            .Size(pageSize)
            .Aggregations(ag => ag
            .Terms("place_Group", p => p.Field(fd => fd.PUBLISHING_PLACE))
            .Cardinality("place_Group_count", dy => dy.Field(fd => fd.PUBLISHING_PLACE))
            .ValueCount("Count", mr => mr.Field(fd => fd.PUBLISHING_PLACE))
            )
            );
            //new SearchRequest()
            //{
            //    Size = pageSize,
            //    From = 0,
            //    Query = Query,
            //    Aggregations = new TermsAggregation("classify")
            //    {
            //        Field = "PUBLISHING_PLACE",
            //        MinimumDocumentCount = 2,
            //        Size = 5,
            //        ShowTermDocCountError = true,
            //        ExecutionHint = TermsAggregationExecutionHint.Map,
            //        Missing = "n/a",
            //        Order = new List<TermsOrder>
            //        {
            //            TermsOrder.TermAscending,
            //            TermsOrder.CountDescending
            //        },
            //        Meta = new Dictionary<string, object>
            //        {
            //            { "foo","bar"}
            //        }
            //    }
            //});
            return result.Documents.ToList();
        }
        /// <summary>
        /// 如果es修改不成功，则不修改数据库数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="client"></param>
        /// <param name="deletePath"></param>
        /// <returns></returns>
        public virtual bool EditEntityWidthScope(MainRCJournalInfo mainRCJournalInfo, DocumentPath<MainRCJournalInfo> deletePath)
        {
            var result = client.Update(deletePath, (p) => p.Doc(mainRCJournalInfo)).IsValid;
            if (result)
                EditEntity(mainRCJournalInfo);
            return result;
        }
        //添加数据
        public int AddDataScope(MainRCJournalInfo param)
        {
            int indexCnt = 0;
            bool result = AddEntity(param);
            if (result)
            {
                if (param.ID > 0)
                {
                    var response = client.Index(param, i => i.Type(param.GetType()));
                    if (response.IsValid)
                    {
                        indexCnt++;
                    }
                    else
                    {
                        // ("创建" + IncrementIndexName + "索引，发生异常:SID:" + param.ID.ToString() + "," + response.DebugInformation, "");
                    }
                }
            }
            return indexCnt;
        }
    }
}
