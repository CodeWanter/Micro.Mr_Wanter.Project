using Micro.Es6.Interface;
using Nest;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Micro.Es6.Service
{
    public class BaseService<T> : IBaseESService<T> where T : class
    {
        public void CreateIndex(ElasticClient client, IIndexState indexState, string indexName)
        {
            var indexExist = client.IndexExists(indexName.ToLower());
            if (!indexExist.Exists)
                client.CreateIndex(indexName.ToLower(), p => p.InitializeUsing(indexState).Mappings(m => m.Map<T>(mp => mp.AutoMap())));
        }
        /// <summary>
        /// 分页查询  分页最大限制（from+size<=10000）
        /// </summary>
        /// <param name="Query">查询Query对象</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sort">排序对象</param>
        /// <returns></returns>
        public List<T> PageSearcher(ElasticClient client, QueryContainer Query, int pageIndex, int pageSize, List<ISort> sort, out long time)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var result = client.Search<T>(
                new SearchRequest()
                {
                    Sort = sort,
                    Size = pageSize,
                    From = (pageIndex - 1) * pageSize,
                    Query = Query,
                });
            watch.Stop();
            time = watch.ElapsedMilliseconds;
            return result.Documents.ToList();
        }

        public virtual List<T> FactSearcher(ElasticClient client, QueryContainer Query)
        {
            int pageSize = 5;
            var result = client.Search<T>(
                new SearchRequest()
                {
                    Size = pageSize,
                    From = 0,
                    Query = Query
                });
            return result.Documents.ToList();
        }

        public int PageCount(ElasticClient client, QueryContainer Query)
        {
            var result = client.Search<T>(
               new SearchRequest()
               {
                   Query = Query,
               }).HitsMetaData.Total;
            return (int)result;
        }

        public int PageCount2(ElasticClient client, string Query, SearchDescriptor<T> query)
        {
            var result = client.Search<T>(
               x => query
                );
            if (result != null)
                return (int)(result.HitsMetaData.Total);
            return 0;
        }

        public virtual bool DeleteRowById(ElasticClient client, DocumentPath<T> document)
        {
            return client.Delete<T>(document).IsValid;
        }

        private DbContextEntity entity;
        public BaseService()
        {
            entity = new DbContextEntity();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool AddEntity(T t)
        {
            entity.Entry<T>(t).State = EntityState.Added;
            return entity.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddEntityList(IEnumerable<T> list)
        {
            if (list != null)
            {
                foreach (var obj in list)
                {
                    entity.Set<T>().Add(obj);
                }
            }
            try
            {
                return entity.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool DelEntity(T t)
        {
            entity.Set<T>().Attach(t);
            entity.Entry<T>(t).State = EntityState.Deleted;
            return entity.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除多条信息  默认20条   添加多条也是此类型方法  各组需要的话就添上
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        public virtual bool DelEntites(IEnumerable<T> list)
        {
            int result = 0;

            entity.Set<T>().RemoveRange(list);

            result = entity.SaveChanges();

            return result > 0;

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool EditEntity(T t)
        {
            entity.Set<T>().Attach(t);
            entity.Entry<T>(t).State = EntityState.Modified;
            return entity.SaveChanges() > 0;
        }
       
        /// <summary>
        /// 根据编号查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindById(int id)
        {
            return entity.Set<T>().Find(id);
        }
        /// <summary>
        /// 根据条件查询某一个对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象（T）</returns>
        public virtual T GetEntity(Expression<Func<T, bool>> WhereLambda)
        {
            return entity.Set<T>().FirstOrDefault(WhereLambda);
        }

        /// <summary>
        /// 根据条件查询某一个泛型集合对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象集合（List<T>）</returns>
        public virtual List<T> GetEntityList(Expression<Func<T, bool>> WhereLambda)
        {
            return entity.Set<T>().Where(WhereLambda).ToList();
        }
    }
}
