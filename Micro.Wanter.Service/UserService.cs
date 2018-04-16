using System;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;

namespace Micro.Wanter.Service
{
    public class UserService : BaseService, IUserService
    {
        public bool CheckUserName(string username)
        {
            S_User user = base.GetEntity<S_User>(s => s.UserName == username);
            return user != null ? true : false;
        }
    }
}
