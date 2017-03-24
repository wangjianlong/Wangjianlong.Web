using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Common;
using Wangjianlong.Models;

namespace Wangjianlong.Web.Controllers
{
    [UserAuthorize(false)]
    public class UserController : ControllerBase
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
            

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name,string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return ErrorJsonResult("用户名与密码不能为空");
            }
            var user = Core.UserManager.Login(name, password);
            if (user == null)
            {
                return ErrorJsonResult("登陆失败，请核对用户名和密码");
            }

            if (user.Approve == false)
            {
                return ErrorJsonResult("当前用户未授权登录，请联系管理员");
            }
            var securebase = new SecureBase
            {
                Address = Request.UserHostAddress,
                HostName = Request.UserHostName,
                Browser = Request.Browser.Browser,
                Version = Request.Browser.Version,
                Platform = Request.Browser.Platform,
                Type = Request.Browser.Type
            };
            System.Web.HttpContext.Current.Application.Login(user, securebase);
            var secure = new Secure(securebase);
            secure.UserID = user.ID;
            HttpContext.SaveAuth(user);
            Core.SecureManager.Save(secure);
            return SuccessJsonResult();
        }

        public ActionResult LoginOut()
        {
            System.Web.HttpContext.Current.Application.LogOut(Identity.UserID);
            HttpContext.ClearAuth();
            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangePassword(string oldPassword,string newpassword,string copyPassword)
        {

            return SuccessJsonResult();
        }
    }
}