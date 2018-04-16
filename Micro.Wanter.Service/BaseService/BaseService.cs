using Micro.Wanter.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Micro.Wanter.Service
{

    /// <summary>
    /// 实现对数据库的操作(增删改查)的基类
    /// </summary>
    /// <typeparam name="T">定义泛型，约束其是一个类</typeparam>
    public class BaseService : IBaseService
    {
        //protected DbContextEntity context { get; private set; }
        ///// <summary>
        ///// 构造函数注入
        ///// </summary>
        ///// <param name="context"></param>
        //public BaseService(DbContextEntity context)
        //{
        //    this.context = context;
        //}

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
        public virtual bool AddEntity<T>(T t) where T : class
        {
            entity.Entry<T>(t).State = EntityState.Added;
            return entity.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddEntityList<T>(IEnumerable<T> list) where T : class
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
        public virtual bool DelEntity<T>(T t) where T : class
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
        public virtual bool DelEntites<T>(IEnumerable<T> list) where T : class
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
        public virtual bool EditEntity<T>(T t) where T : class
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
        public virtual T FindById<T>(int id) where T : class
        {
            return entity.Set<T>().Find(id);
        }
        /// <summary>
        /// 根据条件查询某一个对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象（T）</returns>
        public virtual T GetEntity<T>(Expression<Func<T, bool>> WhereLambda) where T : class
        {
            return entity.Set<T>().FirstOrDefault(WhereLambda);
        }

        /// <summary>
        /// 根据条件查询某一个泛型集合对象
        /// </summary>
        /// <param name="WhereLambda">条件</param>
        /// <returns>对象集合（List<T>）</returns>
        public virtual List<T> GetEntityList<T>(Expression<Func<T, bool>> WhereLambda) where T : class
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
        public virtual List<T> GetEntityPagging<T, A>(Expression<Func<T, A>> orderLambda, Expression<Func<T, bool>> WhereLambda, int pageindex, int pagesize, out int pageCount, out int total, bool isarc = true) where T : class
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

        public void Dispose()
        {
            if (this.entity != null)
            {
                this.entity.Dispose();
            }
        }
    }
}
