using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Models;

namespace Wangjianlong.Web.Controllers
{
    public class SystemUserController : ControllerBase
    {
        // GET: SystemUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(User user,string copypassword)
        {
            if (user == null)
            {
                return ErrorJsonResult("未获取用户相关信息");
            }
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.DisplayName))
            {
                return ErrorJsonResult("系统登陆名以及名称不能为空");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return ErrorJsonResult("密码不能为空");
            }
            if (user.Password != copypassword)
            {
                return ErrorJsonResult("两次密码不一致");
            }
            var id = Core.UserManager.Save(user);
            if (id <= 0)
            {
                return ErrorJsonResult("保存用户失败");
            }
            return SuccessJsonResult();
        }
    }
}