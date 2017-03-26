using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Index(string name=null,string number=null,string address=null,DateTime?startTime=null,DateTime? EndTime=null, int page=1,int rows=20)
        {
            var parameter = new FitmentParameter
            {
                Name=name,
                Number=number,
                Address=address,
                StartTime=startTime,
                EndTime=EndTime,
                Page = new PageParameter(page, rows),
                UserID=Identity.UserID
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
            var citys = Core.CityManager.GetList();
            ViewBag.Citys = citys;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Fitment fitment)
        {
            if (fitment == null)
            {
                return ErrorJsonResult("未获取装修表单信息");
            }

            var city = Core.CityManager.Get(fitment.CityID);
            if (city == null)
            {
                return ErrorJsonResult("未找到所属城市信息");
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
                if (Core.FitmentManager.Exist(fitment.Name))
                {
                    return ErrorJsonResult(string.Format("系统中已存在装修表单名称为{0}", fitment.Name));
                }
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
            ViewBag.Positions = Core.PositionManager.GetByFitmentID(id);
            ViewBag.Items = Core.ItemProjectPositionManager.GetFitmentID(id);
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

        public ActionResult DeletePosition(int id)
        {
            if (Core.PositionManager.Used(id))
            {
                return ErrorJsonResult("当前位置已经添加项目，故无法删除");
            }
            if (!Core.PositionManager.Delete(id))
            {
                return ErrorJsonResult("删除位置失败，未找到删除的位置信息");
            }
            return SuccessJsonResult();
        }

        public ActionResult Download(int id)
        {
            var fitment = Core.FitmentManager.Get(id);
            if (fitment == null)
            {
                throw new ArgumentException("获取装修表单信息失败");
            }
            var positions = Core.PositionManager.GetByFitmentID(fitment.ID);
            var items = Core.ItemProjectPositionManager.GetFitmentID(fitment.ID);

            IWorkbook workbook = FitmentExcelManager.SaveFitment(fitment, positions, items);
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            ms.Flush();
            byte[] fileContents = ms.ToArray();
            return File(fileContents, "application/ms-excel", string.Format("{0}.xls", fitment.Name));
        }

        public ActionResult Delete(int id)
        {
            if (Core.FitmentManager.Used(id))
            {
                return ErrorJsonResult("当前存在关联位置信息，无法删除");
            }
            if (!Core.FitmentManager.Delete(id))
            {
                return ErrorJsonResult("删除失败，未找到删除的信息");
            }
            return SuccessJsonResult();
        }
    }
}