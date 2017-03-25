using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wangjianlong.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            if (!Identity.IsAuthenticated)
            {
                return Redirect("/User/Login");
            }
            ViewBag.FCount = Core.FitmentManager.Count(new Models.Parameter.FitmentParameter { UserID = Identity.UserID });
            ViewBag.PCount = Core.ProjectManager.Count(new Models.Parameter.ProjectParameter());
            ViewBag.SCount = Core.SecureManager.Count(new Models.Parameter.SecureParameter() { UserID = Identity.UserID });
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}