using Micro.Wanter.Dal;
using Micro.Wanter.IBll;
using Micro.Wanter.Model;

namespace Micro.Wanter.Bll
{
    /// <summary>
    /// 查收查引bll
    /// </summary>
    public class WF_DatabaseretrievingBLL: BaseBLL<WF_Databaseretrieving>, IWF_DatabaseretrievingBLL<WF_Databaseretrieving>
    {
        protected override BaseDAL<WF_Databaseretrieving> setDal()
        {
            return new WF_DatabaseretrievingDAL();
        }
    }
}
