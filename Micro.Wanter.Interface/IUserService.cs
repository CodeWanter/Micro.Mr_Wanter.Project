namespace Micro.Wanter.Interface
{
    public interface IUserService: IBaseService
    {
        bool CheckUserName(string username);
    }
}
