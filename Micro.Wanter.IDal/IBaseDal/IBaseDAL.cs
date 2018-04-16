using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZCX.Eamian.Model;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;

namespace Micro.Wanter.Dal
{

    /// <summary>
    /// 实现对数据库的操作(增删改查)的基类
    /// </summary>
    /// <typeparam name="T">定义泛型，约束其是一个类</typeparam>
    public class BaseDAL<T> where T : class
    {
        private DbContextEntity entity;
        public BaseDAL()
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
            entity.Entry<T>(t).State = System.Data.Entity.EntityState.Added;
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
            entity.Entry<T>(t).State = System.Data.Entity.EntityState.Deleted;
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
            entity.Entry<T>(t).State = System.Data.Entity.EntityState.Modified;
            return entity.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据编号查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindById(int id)
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

            total = entity.Set<T>().Where(WhereLambda).Count();
            pageCount = Convert.ToInt32(Math.Ceiling(total / (decimal)pagesize));

            if (isarc)
            {
                return entity.Set<T>().OrderBy(orderLambda).Where(WhereLambda).Skip(pageindex).Take(pagesize).ToList();
            }
            else
            {
                return entity.Set<T>().OrderByDescending(orderLambda).Where(WhereLambda).Skip(pageindex).Take(pagesize).ToList();
            }
        }
    }
}
