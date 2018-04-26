using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using Micro.Wanter.Model;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Micro.Wanter.Interface;
using Micro.Wanter.Service;
using System.Web;

namespace Micro.Mr_Wanter.MVC.SignalR
{
    [HubName("chartHub")]
    public class UserHub : Hub
    {
        private static IUserService _iUserService = new UserService();
        public static List<S_User> users = new List<S_User>();
        //_iUserService.GetEntityList<S_User>(s=>true);
        //发送消息
        public void SendMessage(string connectionId, string message,string username)
        {
            Clients.All.hello();
            var user = users.Where(s => s.ConnectionID == connectionId).FirstOrDefault();
            if (user != null)
            {
                //给指定用户发送,把自己的ID传过去
                Clients.Client(connectionId).addMessage("<div style='height: 80px; '><div style='text-align:center;'>" + DateTime.Now + "</div><div style='float:left;position: relative;margin-top: 30px; '><div class='uname'>" + username+ "</div><div  class='msg'>" + message + "</div></div></div>", Context.ConnectionId);
                //给自己发送，把用户的ID传给自己
                Clients.Client(Context.ConnectionId).addMessage("<div style='height: 80px; '><div style='text-align:center;'>" + DateTime.Now + "</div><div style='float:right;position: relative;margin-top: 30px;'><div  class='uname' style=''>" + username + "</div><div class='msg'>" + message + "</div></div></div>", connectionId);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("该用户已离线");
            }
        }
        //[HubMethodName("exitChat")]
        public void GetName(string name)
        {
            //查询用户
            var user = users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
            if (user != null)
            {
                user.UserName = name;
                Clients.Client(Context.ConnectionId).showId(Context.ConnectionId);
            }
            var user2 = users.SingleOrDefault(u => u.UserName == name && u.ConnectionID != Context.ConnectionId);
            users.Remove(user2);
            GetUsers();
        }

        /// <summary>
        /// 重写连接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            //查询用户
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合
            if (user == null)
            {
                user = new S_User("", Context.ConnectionId);
                users.Add(user);
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = users.Where(p => p.ConnectionID == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，存在则删除
            if (user != null)
            {
                //删除用户
                users.Remove(user);
            }
            GetUsers();//获取所有用户的列表
            return base.OnDisconnected(stopCalled);
        }
        //获取所有用户在线列表
        private void GetUsers()
        {
            var list = users.Select(s => new { s.UserName, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
    }
}