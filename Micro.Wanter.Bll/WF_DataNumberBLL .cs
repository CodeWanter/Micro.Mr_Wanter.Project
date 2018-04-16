using LZCX.Eamian.DAL;
using LZCX.Eamian.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZCX.Eamian.BLL
{
    /// <summary>
    /// 科技查新编号bll
    /// </summary>
    public class WF_DataNumberBLL : BaseBLL<WF_DataNumber>
    {
        protected override DAL.BaseDAL<WF_DataNumber> setDal()
        {
            return new WF_DataNumberDAL();
        }
    }
}
