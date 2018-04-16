using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Es6.Interface
{
    public interface IBaseESService<T> where T : class
    {
        void CreateIndex(ElasticClient client, IIndexState indexState, string indexName);

        /// <summary>
        /// 分页查询  分页最大限制（from+size<=10000）
        /// </summary>
        /// <param name="Query">查询Query对象</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sort">排序对象</param>
        /// <returns></returns>
        List<T> PageSearcher(ElasticClient client, QueryContainer Query, int pageIndex, int pageSize, List<ISort> sort, out long time);

        List<T> FactSearcher(ElasticClient client, QueryContainer Query);

        int PageCount(ElasticClient client, QueryContainer Query);


        bool DeleteRowById(ElasticClient client, DocumentPath<T> document);

        bool AddEntity(T t);

        int AddEntityList(IEnumerable<T> list);

        bool DelEntity(T t);

        bool DelEntites(IEnumerable<T> list);

        bool EditEntity(T t);
        T FindById(int id);

        T GetEntity(Expression<Func<T, bool>> WhereLambda);

        List<T> GetEntityList(Expression<Func<T, bool>> WhereLambda);
    }
}
