using LZCX.Eamian.DAL;
using LZCX.Eamian.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZCX.Eamian.BLL
{
    public class WF_RoleBLL : BaseBLL<WF_Role>
    {
        protected override DAL.BaseDAL<WF_Role> setDal()
        {
            return new WF_RoleDAL();
        }
    }
}
