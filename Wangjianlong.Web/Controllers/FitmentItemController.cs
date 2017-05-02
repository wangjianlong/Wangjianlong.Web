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
        public ActionResult Edit(int id,string Formula, double newold)
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
            if (!string.IsNullOrEmpty(Formula))
            {
                var val = .0;
                if(double.TryParse(Formula,out val))
                {
                    model.Number = val;
                }
                else
                {
                    var dt = new DataTable();
                    if (double.TryParse(dt.Compute(Formula.Replace("（", "(").Replace("）", ")"), null).ToString(), out val))
                    {
                        model.Number = Math.Round(val, 2);
                    }
                }
            }
            model.NewOld = newold;
            model.Formula = Formula;
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

        [HttpPost]
        public ActionResult SaveItem(FitmentItem item,string TitleKey,int cityId)
        {
            if (string.IsNullOrEmpty(TitleKey))
            {
                return ErrorJsonResult("请选择材料");
            }
            var array = TitleKey.Split('+');
            if (array.Length != 3)
            {
                return ErrorJsonResult("未识别当前材料信息");
            }
            var str = array[2].Replace("元", "");
            var temp = str.Split('/');
            var price = 0;
            if(int.TryParse(temp[0],out price))
            {
                var project = Core.ProjectManager.Get(array[0], array[1],price,temp[1], cityId);
                if (project == null)
                {
                    return ErrorJsonResult("项目信息错误");
                }
                item.ProjectID = project.ID;
            }
            else
            {
                return ErrorJsonResult("未识别材料信息");
            }
           
            var val = .0;
            if(double.TryParse(item.Formula,out val))
            {
                item.Number = Math.Round(val, 2);
            }
            else
            {
                var dt = new DataTable();
                if (double.TryParse(dt.Compute(item.Formula.Replace("（","(").Replace("）",")"), null).ToString(), out val))
                {
                    item.Number = Math.Round(val, 2);
                }
            }
            var IID = Core.FitmentItemManager.Save(item);
            if (IID <= 0)
            {
                return ErrorJsonResult("保存失败");
            }
            return SuccessJsonResult();
        }
    }
}