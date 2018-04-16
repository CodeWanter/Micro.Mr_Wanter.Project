using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Micro.Wanter.IBll
{
    public interface IBaseBLL<T> where T : class
    {


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool AddEntity(T t);

        int AddEntityList(IEnumerable<T> list);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool DelEntity(T t);

        /// <summary>
        /// 删除多条信息  默认20条
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        bool DelEntites(IEnumerable<T> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool EditEntity(T t);

        /// <summary>
        /// 根据编号查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Findid(int id);

        /// <summary>
        /// 根据条件查询某一个对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象（T）</returns>
        T GetEntity(Expression<Func<T, bool>> WhereLambda);

        /// <summary>
        /// 根据条件查询某一个泛型集合对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象集合（List<T>）</returns>
        List<T> GetEntityList(Expression<Func<T, bool>> WhereLambda);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="A">排序字段</typeparam>
        /// <param name="orderLambda">排序条件</param>
        /// <param name="WhereLambda">查询条件</param>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">每页条数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="total">总条数</param>
        /// <param name="isarc">排序方式（默认升序）</param>
        /// <returns>对象集合（List<T>）</returns>
        List<T> GetEntityPagging<A>(Expression<Func<T, A>> orderLambda, Expression<Func<T, bool>> WhereLambda, int pageindex, int pagesize, out int pageCount, out int total, bool isarc = true);
    }
}
