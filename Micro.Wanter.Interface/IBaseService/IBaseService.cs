using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Micro.Wanter.Interface
{
    public interface IBaseService:IDisposable
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool AddEntity<T>(T t) where T : class;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int AddEntityList<T>(IEnumerable<T> list) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool DelEntity<T>(T t) where T : class;

        /// <summary>
        /// 删除多条信息  默认20条   添加多条也是此类型方法  各组需要的话就添上
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        bool DelEntites<T>(IEnumerable<T> list) where T : class;

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool EditEntity<T>(T t) where T : class;

        /// <summary>
        /// 根据编号查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById<T>(int id) where T : class;

        /// <summary>
        /// 根据条件查询某一个对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象（T）</returns>
        T GetEntity<T>(Expression<Func<T, bool>> WhereLambda) where T : class;

        /// <summary>
        /// 根据条件查询某一个泛型集合对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象集合（List<T>）</returns>
        List<T> GetEntityList<T>(Expression<Func<T, bool>> WhereLambda) where T : class;

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
        List<T> GetEntityPagging<T, A>(Expression<Func<T, A>> orderLambda, Expression<Func<T, bool>> WhereLambda, int pageindex, int pagesize, out int pageCount, out int total, bool isarc = true) where T : class;
    }
}
