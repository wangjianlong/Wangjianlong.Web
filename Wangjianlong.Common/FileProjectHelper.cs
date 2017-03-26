using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Common
{
    public static class FileProjectHelper
    {
        private static Project AnalyzeProject(IRow row,int cityID)
        {
            var cells = row.GetCells(0, 4);
            if (cells == null)
            {
                return null;
            }
            var title = cells[1].ToString().Trim();
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }
            int a = 0;
            var model = new Project()
            {
                Title = title,
                Name = cells[2].ToString().Trim(),
                Price = int.TryParse(cells[3].ToString().Trim(), out a) ? a : 0,
                Unit = cells[4].ToString().Trim(),
                CityID=cityID
            };
            return model;
        }
        public static List<Project> AnalyzeProject(string filePath,int cityID)
        {
            var list = new List<Project>();
            IWorkbook workbook = filePath.OpenExcel();
            if (workbook != null)
            {
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    for(var i = 1; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue;
                        }
                        var model = AnalyzeProject(row,cityID);
                        if (model != null)
                        {
                            list.Add(model);
                        }
                    }
                }
            }
            return list;
        }
    }
}
