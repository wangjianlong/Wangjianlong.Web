using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wangjianlong.Common;
using Wangjianlong.Models;

namespace Wangjianlong.Web.Controllers
{
    public class FitmentItemController : ControllerBase
    {
        // GET: FitmentItem
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveItems(int positionID)
        {
            var position = Core.PositionManager.Get(positionID);
            if (position == null)
            {
                return ErrorJsonResult("位置ID不正确,请刷新重试");
            }
            int[] projectIDs = HttpContext.Get("ProjectID");
            string[] formulas = HttpContext.GetString("Formula");
            //double[] numbers = HttpContext.GetDouble("Number");
            double[] newold = HttpContext.GetDouble("NewOld");
            if (projectIDs.Length > 0 && formulas.Length > 0 &&newold.Length>0&& projectIDs.Length == formulas.Length&&formulas.Length==newold.Length)
            {
                var list = new List<FitmentItem>();
                var dt = new DataTable();
                for(var i = 0; i < projectIDs.Length; i++)
                {
                    var no = newold[i];
                    if (string.IsNullOrEmpty(formulas[i])|| no < 0 || no > 100)
                    {
                        continue;
                    }
                    var formula = formulas[i];
                    double number = .0;
                    if(double.TryParse(dt.Compute(formula, null).ToString(),out number))
                    {
                        list.Add(new FitmentItem
                        {
                            ProjectID = projectIDs[i],
                            Formula = formulas[i],
                            Number = Math.Round(number, 2),
                            NewOld = newold[i],
                            PositionID = positionID
                        });
                    }
                  
                }
                if (list.Count > 0)
                {
                    Core.FitmentItemManager.AddRange(list);
                }
                return SuccessJsonResult(position.FitmentID);
            }
            return ErrorJsonResult("获取项目清单失败");
        }

        public ActionResult Delete(int id)
        {
            if (!Core.FitmentItemManager.Delete(id))
            {
                return ErrorJsonResult("未找到删除的项目信息");
            }
            return SuccessJsonResult();
        }

        public ActionResult Edit(int id)
        {
            var model = Core.FitmentItemManager.Get(id);
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id,double number,double newold)
        {
            if (newold <= 0 || newold > 100)
            {
                return ErrorJsonResult("成新值在0~100之间，请核对");
            }
            var model = Core.FitmentItemManager.Get(id);
            if (model == null)
            {
                return ErrorJsonResult("编辑项目失败，未找到项目信息");
            }
            model.Number = number;
            model.NewOld = newold;
            if (!Core.FitmentItemManager.Edit(model))
            {
                return ErrorJsonResult("编辑失败，未找到编辑的项目信息");
            }
            
            return SuccessJsonResult();
        }

        public ActionResult PositionItem(int id)
        {
            var position = Core.PositionManager.Get(id);
            ViewBag.Model = position;
            return View();
        }
    }
}