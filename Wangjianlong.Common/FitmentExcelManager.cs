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
        private static string _modelExcelPath2 { get; set; }
        static FitmentExcelManager()
        {
            _modelExcelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excels", System.Configuration.ConfigurationManager.AppSettings["FIT"]);
            _modelExcelPath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Excels", System.Configuration.ConfigurationManager.AppSettings["FIT2"]);

        }
        public static IWorkbook SaveFitment2(Fitment fitment,List<Position> positions,List<ItemProjectPosition> items)
        {
            IWorkbook workbook = _modelExcelPath2.OpenExcel();
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
                    var all = .0;
                    foreach (var entry in dict)
                    {
                        row = sheet.GetRow(line);
                        if (row == null)
                        {
                            row = sheet.CreateRow(line);
                        }
                        row.Height = modelRow.Height;
                        var cell = ExcelManager.GetCell(row, 0, modelRow);
                        cell.SetCellValue(entry.Key.GetDescription());
                        for (var i = 1; i <= 7; i++)
                        {
                            ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                        }
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(line, line, 0, 7));
                        line++;
                      
                        #region 
                        foreach (var position in entry.Value)
                        {
                            var list = items.Where(e => e.PositionID == position.ID).ToList();
                            var serial = 1;
                            var startline = line;
                            var merge = .0;
                            foreach (var item in list)
                            {
                                row = sheet.GetRow(line);
                                if (row == null)
                                {
                                    row = sheet.CreateRow(line);
                                }
                                row.Height = modelRow.Height;
                                line++;
                                ExcelManager.GetCell(row, 0, modelRow).SetCellValue(serial++);
                                ExcelManager.GetCell(row, 1, modelRow).SetCellValue(item.Name);
                                ExcelManager.GetCell(row, 2, modelRow).SetCellValue(item.Price);
                                ExcelManager.GetCell(row, 3, modelRow).SetCellValue(item.Number);
                                ExcelManager.GetCell(row, 4, modelRow).SetCellValue(item.Unit);
                                ExcelManager.GetCell(row, 5, modelRow).SetCellValue(string.Format("{0}%", item.NewOld));
                                ExcelManager.GetCell(row, 6, modelRow).SetCellValue(item.Sum);
                                ExcelManager.GetCell(row, 7, modelRow);
                                merge += item.Sum;
                            }
                            row = sheet.GetRow(line)??sheet.CreateRow(line);
                            row.Height = modelRow.Height;
                            line++;
                            ExcelManager.GetCell(row, 0, modelRow).SetCellValue("小计");
                            for(var i = 1; i < 8; i++)
                            {
                                ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                            }
                            ExcelManager.GetCell(row, 6, modelRow).SetCellValue(merge);
                            all += merge;
                            row = sheet.GetRow(startline);
                            row.Height = modelRow.Height;
                            ExcelManager.GetCell(row, 7, modelRow).SetCellValue(position.Name);
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(startline, line-1, 7, 7));
                        }
                        #endregion
                    }
                    row = sheet.GetRow(line) ?? sheet.CreateRow(line);
                    row.Height = modelRow.Height;
                    ExcelManager.GetCell(row, 0, modelRow).SetCellValue("合计");
                    for (var i = 1; i < 6; i++)
                    {
                        ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                    }
                    ExcelManager.GetCell(row, 6, modelRow).SetCellValue(all);
                    ExcelManager.GetCell(row, 7, modelRow).SetCellValue("");
                }
            }
            return workbook;
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
                        for(var i = 1; i <= 7; i++)
                        {
                            ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                        }
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(line, line, 0, 7));
                        line++;
                        var all = .0;
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
                                ExcelManager.GetCell(row, 1,modelRow).SetCellValue(item.Name);
                                ExcelManager.GetCell(row, 2,modelRow).SetCellValue(item.Price);
                                ExcelManager.GetCell(row, 3,modelRow).SetCellValue(item.Number);
                                ExcelManager.GetCell(row, 4, modelRow).SetCellValue(item.Unit);
                                ExcelManager.GetCell(row, 5, modelRow).SetCellValue(string.Format("{0}%", item.NewOld));
                                ExcelManager.GetCell(row, 6,modelRow).SetCellValue(item.Sum);
                                ExcelManager.GetCell(row, 7, modelRow);
                                merge += item.Sum;
                            }
                            row = sheet.GetRow(line);
                            if (row == null)
                            {
                                row = sheet.CreateRow(line);
                            }
                            ExcelManager.GetCell(row, 0,modelRow).SetCellValue(entry.Key!=Category.Appendix?"小计":"合计");
                            for(var i = 1; i < 6; i++)
                            {
                                ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                            }
                            ExcelManager.GetCell(row, 6,modelRow).SetCellValue(merge);
                            all += merge;
                            ExcelManager.GetCell(row, 7, modelRow).SetCellValue("");
                            row = sheet.GetRow(startline);
                            ExcelManager.GetCell(row, 7,modelRow).SetCellValue(position.Name);
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(startline, line, 7, 7));
                            line++;
                        }
                        if (entry.Key != Category.Appendix)
                        {
                            row = sheet.GetRow(line) ?? sheet.CreateRow(line);
                            ExcelManager.GetCell(row, 0, modelRow).SetCellValue("合计");
                            for(var i = 1; i < 6; i++)
                            {
                                ExcelManager.GetCell(row, i, modelRow).SetCellValue("");
                            }
                            ExcelManager.GetCell(row, 6, modelRow).SetCellValue(all);
                            ExcelManager.GetCell(row, 7, modelRow).SetCellValue("");
                            line++;
                        }
                    }
                }
            }
            return workbook;
        }
    }
}
