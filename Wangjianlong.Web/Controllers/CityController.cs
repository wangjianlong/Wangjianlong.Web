using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Models;

namespace Wangjianlong.Web.Controllers
{
    public class CityController : ControllerBase
    {
        // GET: City
        public ActionResult Index()
        {
            var list = Core.CityManager.GetList();
            ViewBag.List = list;
            return View();
        }

        public ActionResult Create(int id=0)
        {
            var model = Core.CityManager.Get(id);
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult Save(City city)
        {
            if (city == null)
            {
                return ErrorJsonResult("未获取城市相关信息");
            }
            if (string.IsNullOrEmpty(city.Name))
            {
                return ErrorJsonResult("城市名称不能为空");
            }
            if (Core.CityManager.Exist(city.Name))
            {
                return ErrorJsonResult("当前系统中已存在相同名字的城市，请核对");
            }
            if (city.ID > 0)
            {
                if (!Core.CityManager.Edit(city))
                {
                    return ErrorJsonResult("编辑更新城市失败");
                }
            }
            else
            {
                var id = Core.CityManager.Save(city);
                if (id <= 0)
                {
                    return ErrorJsonResult("保存城市失败");
                }
            }
            return SuccessJsonResult();
        }

        public ActionResult Delete(int id)
        {
            if (Core.CityManager.Used(id))
            {
                return ErrorJsonResult("当前城市已经录入装修项目，无法删除");
            }

            if (!Core.CityManager.Delete(id))
            {
                return ErrorJsonResult("删除失败，未找到删除城市信息");
            }

            return SuccessJsonResult();
        }
    }
}