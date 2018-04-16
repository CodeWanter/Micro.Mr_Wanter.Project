using LZCX.Eamian.DAL;
using LZCX.Eamian.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZCX.Eamian.BLL
{
    public class WF_RtoMBLL : BaseBLL<WF_RtoM>
    {
        protected override DAL.BaseDAL<WF_RtoM> setDal()
        {
            return new WF_RtoMDAL();
        }
    }
}
