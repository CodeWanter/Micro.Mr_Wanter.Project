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
    /// 回执单信息bll
    /// </summary>
    public class WF_SinglereceiptBLL : BaseBLL<WF_Singlereceipt>
    {
        protected override DAL.BaseDAL<WF_Singlereceipt> setDal()
        {
            return new WF_SinglereceiptDAL();
        }
    }
}
