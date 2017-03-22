using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Common;
using Wangjianlong.Models;
using Wangjianlong.Models.Parameter;

namespace Wangjianlong.Web.Controllers
{
    public class ProjectController : ControllerBase
    {
        // GET: Project
        public ActionResult Index(string title=null,string name=null,int? minPrice=null,int? maxPrice=null, int page=1,int rows=20)
        {
            var parameter = new ProjectParameter
            {
                Title = title,
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = new PageParameter(page, rows)
            };
            var list = Core.ProjectManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult File()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnalyzeFile()
        {
            if (Request.Files.Count == 0)
            {
                throw new ArgumentException("请上传文件");
            }
            var file = HttpContext.Request.Files[0];
            var fileName = file.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("请上传文件");
            }
            var filePath = FileManager.Upload(file);
            var list = FileProjectHelper.AnalyzeProject(filePath);
            Core.ProjectManager.AddRange(list);
            return RedirectToAction("Index");
        }

        public ActionResult AddList(int positionId)
        {
            var position = Core.PositionManager.Get(positionId);
            ViewBag.Model = position;
            return View();
        }

        public ActionResult Search(string key)
        {
            List<Project> list;
            if (string.IsNullOrEmpty(key))
            {
                list = new List<Project>();
            }
            else
            {
                list = Core.ProjectManager.Search(key);
                //var parameter = new ProjectParameter
                //{
                //    Title = key,
                //    Name = key,
                //    Page = new PageParameter(1, 10)
                //};
                // list = Core.ProjectManager.Search(parameter);
            }
  
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}