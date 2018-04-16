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
    public class WF_NoveltyNumberBLL : BaseBLL<WF_NoveltyNumber>
    {
        protected override DAL.BaseDAL<WF_NoveltyNumber> setDal()
        {
            return new WF_NoveltyNumberDAL();
        }
    }
}
