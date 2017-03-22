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
            HttpContext.SaveAuth(user);
            return SuccessJsonResult();
        }

        public ActionResult LoginOut()
        {
            HttpContext.ClearAuth();
            return RedirectToAction("Login");
        }
    }
}