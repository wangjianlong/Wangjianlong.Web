using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Common;
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

            return RedirectToAction("Index");
        }
    }
}