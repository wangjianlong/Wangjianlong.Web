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
        public ActionResult Index(
            string title=null,string name=null,
            int? minPrice=null,int? maxPrice=null,
            int?CityID=null,ProjectOrder order=ProjectOrder.City,
            int page=1,int rows=20)
        {
            var parameter = new ProjectParameter
            {
                Title = title,
                Name = name,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CityID=CityID,
                Order=order,
                Page = new PageParameter(page, rows)
            };
            var citys = Core.CityManager.GetList();
            ViewBag.Citys = citys;
            var list = Core.ProjectManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult Create(int id = 0)
        {
            var model = Core.ProjectManager.Get(id);
            ViewBag.Model = model;
            if (model == null)
            {
                ViewBag.Citys = Core.CityManager.GetList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Save(Project project)
        {
            if (project == null)
            {
                return ErrorJsonResult("未获取项目相关信息");
            }
            if (project.CityID <= 0)
            {
                return ErrorJsonResult("请选择项目所属城市");
            }
            else
            {
                var city = Core.CityManager.Get(project.CityID);
                if (city == null)
                {
                    return ErrorJsonResult("当前选择城市已经删除，请查看城市管理");
                }
            }

            if (project.Price <= 0)
            {
                return ErrorJsonResult("单价不能小于等于0");
            }
            if(string.IsNullOrEmpty(project.Title)
                ||string.IsNullOrEmpty(project.Name)
                || string.IsNullOrEmpty(project.Unit))
            {
                return ErrorJsonResult("项目缩写、项目名称、项目单位不能为空");
            }
            if (Core.ProjectManager.Exist(project.Title, project.Name, project.Price, project.Unit))
            {
                return ErrorJsonResult("当前系统中已存在相同的项目信息");
            }
            if (project.ID > 0)
            {
                if (!Core.ProjectManager.Edit(project))
                {
                    return ErrorJsonResult("编辑项目信息失败，未找到编辑的项目信息");
                }
            }
            else
            {
                var id = Core.ProjectManager.Save(project);
                if (id <= 0)
                {
                    return ErrorJsonResult("保存项目信息失败");
                }
            }
            return SuccessJsonResult();
        }
        public ActionResult Delete(int id)
        {
            if (Core.ProjectManager.Used(id))
            {
                return ErrorJsonResult("删除失败，当前项目已作为表单使用");
            }
            if (!Core.ProjectManager.Delete(id))
            {
                return ErrorJsonResult("删除失败，未找到相关删除信息");
            }

            return SuccessJsonResult();
        }


        public ActionResult File()
        {
            var citys = Core.CityManager.GetList();
            ViewBag.Citys = citys;
            return View();
        }

        [HttpPost]
        public ActionResult AnalyzeFile(int cityID)
        {
            var city = Core.CityManager.Get(cityID);
            if (city == null)
            {
                throw new ArgumentException("请选择所属城市");
            }
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
            var list = FileProjectHelper.AnalyzeProject(filePath,cityID);
            Core.ProjectManager.AddRange(list);
            return RedirectToAction("Index");
        }

        public ActionResult AddList(int positionId,int cityID)
        {
            var position = Core.PositionManager.Get(positionId);
            ViewBag.Model = position;
            ViewBag.CityID = cityID;
            return View();
        }

        public ActionResult Search(string key,int cityID)
        {
            List<Project> list;
            if (string.IsNullOrEmpty(key))
            {
                list = new List<Project>();
            }
            else
            {
                list = Core.ProjectManager.Search(key,cityID);
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