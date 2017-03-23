using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Common
{
    public static class FitmentExcelManager
    {
        private static string _modelExcelPath { get; set; }
        static FitmentExcelManager()
        {
            _modelExcelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excels", System.Configuration.ConfigurationManager.AppSettings["FIT"]);
        }
        public static IWorkbook SaveFitment(Fitment fitment,List<Position> positions,List<ItemProjectPosition> items)
        {
            IWorkbook workbook = _modelExcelPath.OpenExcel();
            if (workbook != null)
            {
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    var row = sheet.GetRow(0);
                    if (row != null)
                    {
                        var cell = ExcelManager.GetCell(row, 1);
                        cell.SetCellValue(fitment.Name);
                        ExcelManager.GetCell(row, 3).SetCellValue(fitment.Number);
                    }
                    row = sheet.GetRow(1);
                    if (row != null)
                    {
                        ExcelManager.GetCell(row, 1).SetCellValue(fitment.Address);
                    }
                    var modelRow = sheet.GetRow(4);
                    var line = 3;
                    var dict = positions.GroupBy(e => e.Category).ToDictionary(e => e.Key, e => e.ToList());
                    foreach(var entry in dict)
                    {
                        row = sheet.GetRow(line);
                        if (row == null)
                        {
                            row = sheet.CreateRow(line);
                        }
                    
                        var cell = ExcelManager.GetCell(row, 0,modelRow);
                        cell.SetCellValue(entry.Key.GetDescription());
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(line, line, 0, 5));
                        line++;
                        foreach (var position in entry.Value)
                        {
                            var list = items.Where(e => e.PositionID == position.ID).ToList();
                            var serial = 1;
                            var startline = line;
                            var merge = .0;
                            foreach(var item in list)
                            {
                                row = sheet.GetRow(line);
                                if (row == null)
                                {
                                    row = sheet.CreateRow(line);
                                }
                                line++;
                                ExcelManager.GetCell(row, 0,modelRow).SetCellValue(serial++);
                                ExcelManager.GetCell(row, 1,modelRow).SetCellValue(string.Format("{0}({1})",item.Name,item.Unit));
                                ExcelManager.GetCell(row, 2,modelRow).SetCellValue(item.Price);
                                ExcelManager.GetCell(row, 3,modelRow).SetCellValue(item.Number);
                                ExcelManager.GetCell(row, 4,modelRow).SetCellValue(item.Sum);
                                ExcelManager.GetCell(row, 5, modelRow);
                                merge += item.Sum;
                            }
                            row = sheet.GetRow(line);
                            if (row == null)
                            {
                                row = sheet.CreateRow(line);
                            }
                            ExcelManager.GetCell(row, 0,modelRow).SetCellValue("合计");
                            for(var i = 1; i < 4; i++)
                            {
                                ExcelManager.GetCell(row, i, modelRow);
                            }
                            ExcelManager.GetCell(row, 4,modelRow).SetCellValue(merge);
                            row = sheet.GetRow(startline);
                            ExcelManager.GetCell(row, 5,modelRow).SetCellValue(position.Name);
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(startline, line, 5, 5));
                            line++;
                        }
                    }
                }
            }
            return workbook;
        }
    }
}
