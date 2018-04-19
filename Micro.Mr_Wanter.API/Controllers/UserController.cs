using Micro.Wanter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Micro.Mr_Wanter.API.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _iUserService = null;
        public UserController(IUserService iWF_UserService)
        {
            _iUserService = iWF_UserService;
        }
    }
}
