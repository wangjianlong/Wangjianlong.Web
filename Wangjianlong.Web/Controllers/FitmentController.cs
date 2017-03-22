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
    public class FitmentController : ControllerBase
    {
        // GET: Fitment
        public ActionResult Index(int page=1,int rows=20)
        {
            var parameter = new FitmentParameter
            {
                Page = new PageParameter(page, rows)
            };
            var list = Core.FitmentManager.Search(parameter);
            ViewBag.List = list;
            ViewBag.Parameter = parameter;
            return View();
        }

        public ActionResult Create(int id=0)
        {
            var model = Core.FitmentManager.Get(id);
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Fitment fitment)
        {
            if (fitment == null)
            {
                return ErrorJsonResult("未获取装修表单信息");
            }
            if (Core.FitmentManager.Exist(fitment.Name))
            {
                return ErrorJsonResult(string.Format("系统中已存在装修表单名称为{0}", fitment.Name));
            }
            if (fitment.ID > 0)
            {
                if (!Core.FitmentManager.Edit(fitment))
                {
                    return ErrorJsonResult("编辑更新失败，未找到编辑的装修表单信息");
                }
            }
            else
            {
                var id = Core.FitmentManager.Save(fitment);
                if (id <= 0)
                {
                    return ErrorJsonResult("保存装修表单失败");
                }
            }
            return SuccessJsonResult(fitment.ID);
        }

        public ActionResult Detail(int id)
        {
            var model = Core.FitmentManager.Get(id);
            ViewBag.Model = model;
            ViewBag.Positions = Core.PositionManager.GetByFitmentID(model.ID);
            return View();
        }

        public ActionResult CreatePosition(int id=0,int fitmentID = 0)
        {
            var position = Core.PositionManager.Get(id);
            if (position == null&&fitmentID!=0)
            {
                position = new Position
                {
                    FitmentID = fitmentID
                };
            }
            ViewBag.Model = position;
            return View();
        }

        [HttpPost]
        public ActionResult SavePosition(Position position)
        {
            if (position == null)
            {
                return ErrorJsonResult("未获取位置信息");
            }
            var fitemnt = Core.FitmentManager.Get(position.FitmentID);
            if (fitemnt == null)
            {
                return ErrorJsonResult(string.Format("未找到ID为{0}装修的表单", position.FitmentID));
            }
            if (Core.PositionManager.Exist(position.FitmentID, position.Name, position.Category))
            {
                return ErrorJsonResult(string.Format("当前表单中已存在类别：{0}；位置名{1}的位置信息", position.Category.GetDescription(), position.Name));
            }
            if (position.ID > 0)
            {
                if (!Core.PositionManager.Edit(position))
                {
                    return ErrorJsonResult(string.Format("编辑更新失败，未找到ID为{0}的位置信息", position.ID));
                }
            }
            else
            {
                var id = Core.PositionManager.Save(position);
                if (id <= 0)
                {
                    return ErrorJsonResult("保存失败");
                }
            }
            return SuccessJsonResult(position.FitmentID);
        }
    }
}