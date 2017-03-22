using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Common
{
    public static class ExcelManager
    {
        public static ICell GetCell(IRow row, int line, IRow modelRow = null)
        {
            ICell cell = row.GetCell(line);
            if (cell == null)
            {
                if (modelRow != null)
                {
                    cell = row.CreateCell(line, modelRow.GetCell(line).CellType);
                    cell.CellStyle = modelRow.GetCell(line).CellStyle;
                }
                else
                {
                    cell = row.CreateCell(line);
                }
            }
            return cell;
        }
        public static IWorkbook OpenExcel(this string filePath)
        {
            IWorkbook workbook = null;
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = WorkbookFactory.Create(fs);
            }
            return workbook;
        }
        public  static ICell[] GetCells(this IRow row, int startline, int endline)
        {
            var cells = new ICell[endline - startline + 1];
            var j = 0;
            for (var i = startline; i <= endline; i++)
            {
                var cell = row.GetCell(i);
                if (cell == null)
                {
                    return null;
                }
                cells[j++] = cell;
            }
            return cells;
        }
        public  static string[] GetString(this IRow row, int startline, int endline)
        {
            var results = new string[endline - startline + 1];
            var j = 0;
            for (var i = startline; i <= endline; i++)
            {
                var cell = row.GetCell(i);
                if (cell == null)
                {
                    return null;
                }
                results[j++] = cell.ToString().Trim();
            }
            return results;
        }
    }
}
