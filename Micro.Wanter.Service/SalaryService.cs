using Micro.Wanter.Common;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;
using System;
using System.Linq.Expressions;

namespace Micro.Wanter.Service
{
    public class SalaryService : BaseService, ISalaryService
    {
        public PageResult<Salary> GetResList<T, A>(Expression<Func<Salary, A>> orderLambda, Expression<Func<Salary, bool>> WhereLambda, int pageindex, int pagesize,bool isarc)
        {
            int pageCount = 0, total = 0;
            PageResult<Salary> pageResult = new PageResult<Salary>()
            {
                DataList = GetEntityPagging(orderLambda, WhereLambda, pageindex, pagesize, out pageCount, out total, isarc),
                TotalCount = GetEntityList(WhereLambda).Count,
                PageIndex = pageindex,
                PageSize = pagesize
            };
            return pageResult;
        }
    }
}
