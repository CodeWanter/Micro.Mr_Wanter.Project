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
    /// 论文清单bll
    /// </summary>
    public class WF_PapersBLL : BaseBLL<WF_Papers>
    {
        protected override DAL.BaseDAL<WF_Papers> setDal()
        {
            return new WF_PapersDAL();
        }
    }
}
