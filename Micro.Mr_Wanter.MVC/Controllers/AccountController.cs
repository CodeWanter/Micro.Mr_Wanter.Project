using Micro.Wanter.Common.DEncrypt;
using Micro.Wanter.Common.VerifyHelper;
using Micro.Wanter.Interface;
using Micro.Wanter.Model;
using Newtonsoft.Json;
using Ruanmou.Redis.Service;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Micro.Mr_Wanter.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _iUserService = null;
        public AccountController(IUserService iWF_UserService)
        {
            _iUserService = iWF_UserService;
        }

        // GET: Account
        public ActionResult Login()
        {
            S_User s_user = new S_User();
            var memberValidation = Request.Cookies.Get("_token");//使用cookie
            if (memberValidation != null && memberValidation.HasKeys)
            {
                s_user = JsonConvert.DeserializeObject<S_User>(memberValidation["name"]);
                s_user.Password = DEncrypt.Decrypt(s_user.Password, "zhang");
            }
            return View(s_user);
        }
        //验证码验证，区分大小写
        public Boolean ValidateCode(string code)
        {
            string sessionCode = Session["CheckCode"] == null ? new Guid().ToString() : Session["CheckCode"].ToString();
            //Session["CheckCode"] = new Guid();
            if (sessionCode != code)
                return false;
            return true;
        }
        //刷新验证码
        public ActionResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }

        [HttpPost]
        public ActionResult Login(string userName, string userPsw, string loginkeeping)
        {
            string psw = DEncrypt.Encrypt(userPsw, "zhang");
            S_User currentUser = _iUserService.GetEntity<S_User>(l => l.UserName == userName && l.Password == psw);
            if (currentUser != null)
            {
                //用户数据插入redis
                //using (RedisHashService service = new RedisHashService())
                //{
                //    service.SetEntryInHash("user", currentUser.id.ToString(), JsonConvert.SerializeObject(currentUser));
                //};
                if (loginkeeping != null)
                {
                    HttpCookie cookie = new HttpCookie("_token");
                    cookie.Values.Add("name", JsonConvert.SerializeObject(currentUser));
                    cookie.Expires = DateTime.Now.AddYears(2);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpContext.Response.Cookies["_token"].Expires = DateTime.Now.AddDays(-1);
                }
                Session["CurrentUser"] = currentUser;
                if (this.HttpContext.Session["CurrentUrl"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string url = this.HttpContext.Session["CurrentUrl"].ToString();
                    this.HttpContext.Session["CurrentUrl"] = null;
                    return Redirect(url);
                }
            }
            else
            {
                ModelState.AddModelError("failed", "密码不正确");
                return View(new S_User() { UserName = userName });
            }
        }

        public Boolean CheckUserName(string usernamesignup)
        {
            return _iUserService.CheckUserName(usernamesignup);
        }

        public ActionResult Register(string usernamesignup, string passwordsignup)
        {
            S_User user = new S_User()
            {
                UserName = usernamesignup,
                Password = DEncrypt.Encrypt(passwordsignup, "zhang"),
                CreateTime = DateTime.Now
            };
            _iUserService.AddEntity(user);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;//表示将制定的键的值清空，并释放掉
            Session.Remove("CurrentUser");
            return RedirectToAction("Index", "Home");
        }
    }
}