using Micor.Remote.Model;
using Micro.Wanter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micor.Remote.Interface
{
    public interface ISearch
    {
        PageResult<ResModel> GetResList(string indexName, string queryString, string sortField, string fields, int pageNumber, int pagePerNo, string fm);
    }
}
