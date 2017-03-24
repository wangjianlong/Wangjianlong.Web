﻿using System;
using System.Collections.Generic;
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
            double[] numbers = HttpContext.GetDouble("Number");
            if (projectIDs.Length > 0 && numbers.Length > 0 && projectIDs.Length == numbers.Length)
            {
                var list = new List<FitmentItem>();
                for(var i = 0; i < projectIDs.Length; i++)
                {
                    list.Add(new FitmentItem
                    {
                        ProjectID = projectIDs[i],
                        Number = numbers[i],
                        PositionID = positionID
                    });
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
        public ActionResult Edit(int id,double number)
        {
            var model = Core.FitmentItemManager.Get(id);
            if (model == null)
            {
                return ErrorJsonResult("编辑项目失败，未找到项目信息");
            }
            model.Number = number;
            if (!Core.FitmentItemManager.Edit(model))
            {
                return ErrorJsonResult("编辑失败，未找到编辑的项目信息");
            }
            
            return SuccessJsonResult();
        }
    }
}