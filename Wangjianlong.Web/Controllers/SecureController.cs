using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Models.Parameter;

namespace Wangjianlong.Web.Controllers
{
    public class SecureController : ControllerBase
    {
        // GET: Secure
        public ActionResult Index(int page = 1,int rows=20)
        {
            var parameter = new SecureParameter
            {
                Page = new PageParameter(page, rows),
                UserID=Identity.UserID
            };
            var list = Core.SecureManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }
    }
}