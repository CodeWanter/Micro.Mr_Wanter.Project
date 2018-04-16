using LZCX.Eamian.DAL;
using LZCX.Eamian.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZCX.Eamian.BLL
{
    public class WF_UserLogBLL : BaseBLL<WF_UserLog>
    {
        protected override DAL.BaseDAL<WF_UserLog> setDal()
        {
            return new WF_UserLogDAL();
        }
    }
}
