using Micro.Es6.Model;
using Micro.Wanter.Common;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Es6.Interface
{
    public interface IMainRCJournalInfoService : IBaseESService<MainRCJournalInfo>
    {
        /// <summary>
        /// 创建索引导入数据
        /// </summary>
        /// <param name="WhereLambda"></param>
        /// <returns></returns>
        bool CreateIndexAndData(Expression<Func<MainRCJournalInfo, bool>> WhereLambda);
        
            PageResult<MainRCJournalInfo> PageResultSearcher(QueryContainer Query, int pageIndex, int pageSize, List<ISort> sort);
        PageResult<MainRCJournalInfo> PageResultSearcher2(string queryString, int pageIndex, int pageSize);

        int AddDataScope(MainRCJournalInfo param);

        bool EditEntityWidthScope(MainRCJournalInfo mainRCJournalInfo, DocumentPath<MainRCJournalInfo> deletePath);

        bool DeleteRowById(DocumentPath<MainRCJournalInfo> document);

        List<MainRCJournalInfo> FactSearcher(QueryContainer Query);
    }
}
