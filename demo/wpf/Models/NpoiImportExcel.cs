using System.Data.Dabber;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenIdea.Qualimetry.DbExcel
{
    /// <summary>
    /// Npoi导出Excel
    /// </summary>
    public class NpoiImportExcel
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo FileInfo { get; }
        /// <summary>
        /// 工作簿
        /// </summary>
        public IWorkbook Workbook { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fileName"></param>
        public NpoiImportExcel(string fileName) : this(new FileInfo(fileName)) { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file"></param>
        public NpoiImportExcel(FileInfo file)
        {
            var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            stream.Position = 0;
            if (file.Extension?.ToLower() == ".xls")
            {
                Workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(stream);
            }
            else
            {
                Workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream);
            }
        }

        /// <summary>
        /// 获取表列
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataTable GetTable(string sheetName)
        {
            var table = new DataTable(sheetName);
            var sheet = Workbook.GetSheet(sheetName);
            if (sheet == null) { return table; }
            int rno = 0;
            var row = sheet.GetRow(rno++);
            if (row == null) { return table; }
            var colIdx = new List<int>();
            for (int i = 0; i < row.LastCellNum; i++)
            {
                var col = row.GetCell(i);
                if (col == null)
                {
                    continue;
                }
                var colName = col.ToString();
                if (string.IsNullOrWhiteSpace(colName))
                {
                    continue;
                }
                colIdx.Add(i);
                table.Columns.Add(col.ToString());
            }

            for (int i = rno; i <= sheet.LastRowNum; i++)
            {
                row = sheet.GetRow(rno++);
                if (row == null) { continue; }
                var dataRow = table.NewRow();
                for (var j = 0; j < colIdx.Count; j++)
                {
                    var col = row.GetCell(colIdx[j]);
                    if(col == null) { continue; }
                    switch (col.CellType)
                    {
                        case CellType.Numeric:
                            dataRow[j] = col.NumericCellValue;
                            break;
                        case CellType.Boolean:
                            dataRow[j] = col.BooleanCellValue;
                            break;
                        case CellType.String:
                            dataRow[j] = col.StringCellValue;
                            break;
                        case CellType.Formula:
                            dataRow[j] = col.StringCellValue;
                            break;
                        case CellType.Error:
                            dataRow[j] = col.ErrorCellValue;
                            break;
                        case CellType.Blank:
                        default:
                            dataRow[j] = col.StringCellValue;
                            break;
                    }
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
