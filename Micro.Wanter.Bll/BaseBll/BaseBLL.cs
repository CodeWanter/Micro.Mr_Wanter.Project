using Micro.Wanter.Dal;
using Micro.Wanter.IBll;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Micro.Wanter.Bll
{
    public abstract class BaseBLL<T> : IBaseBLL<T> where T : class
    {
      
        /// <summary>
        /// 实例化BaseDAL  
        /// </summary>
       protected BaseDAL<T> dal;
        /// <summary>
        /// 构造方法设置dal实例
        /// </summary>
       public BaseBLL ()
       {
           dal = setDal();
       } 
        /// <summary>
        /// 抽象方法 实现此方法，完成dal实例的设置
        /// </summary>
        /// <returns>dal实例</returns>
       protected abstract BaseDAL<T> setDal();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool AddEntity(T t)
        {
            return dal.AddEntity(t); 
        }

        public int AddEntityList(IEnumerable<T> list)
        {
            return dal.AddEntityList(list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool DelEntity(T t)
        {
            return dal.DelEntity(t);
        }

        /// <summary>
        /// 删除多条信息  默认20条
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        public virtual bool DelEntites(IEnumerable<T> list)
        {
           
            return dal.DelEntites(list);

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool EditEntity(T t)
        {
            return dal.EditEntity(t);
        }
        /// <summary>
        /// 根据编号查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Findid(int id)
        {
            return dal.FindById(id);
        }
        /// <summary>
        /// 根据条件查询某一个对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象（T）</returns>
        public virtual T GetEntity(Expression<Func<T, bool>> WhereLambda)
        {
            return dal.GetEntity(WhereLambda);
        }

        /// <summary>
        /// 根据条件查询某一个泛型集合对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象集合（List<T>）</returns>
        public virtual List<T> GetEntityList(Expression<Func<T, bool>> WhereLambda)
        {
            return dal.GetEntityList(WhereLambda);
        }

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
        public virtual List<T> GetEntityPagging<A>(Expression<Func<T, A>> orderLambda, Expression<Func<T, bool>> WhereLambda, int pageindex, int pagesize, out int pageCount, out int total, bool isarc = true)
        {
            return dal.GetEntityPagging(orderLambda, WhereLambda, pageindex, pagesize, out pageCount, out total, isarc);
        }
    }
}
