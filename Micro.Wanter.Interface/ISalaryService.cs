using Micro.Wanter.Common;
using Micro.Wanter.Model;
using System;
using System.Linq.Expressions;

namespace Micro.Wanter.Interface
{
    public interface ISalaryService : IBaseService
    {
        PageResult<Salary> GetResList<T, A>(Expression<Func<Salary, A>> orderLambda, Expression<Func<Salary, bool>> WhereLambda, int pageindex, int pagesize,bool isarc);
    }
}
