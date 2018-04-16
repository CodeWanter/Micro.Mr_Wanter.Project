namespace LZCX.Eamian.BLL
{
    /// <summary>
    /// 科技查新bll
    /// </summary>
    public class WF_NoveltysearchBLL : BaseBLL<WF_Noveltysearch>
    {
        protected override DAL.BaseDAL<WF_Noveltysearch> setDal()
        {
            return new WF_NoveltysearchDAL();
        }
    }
}
