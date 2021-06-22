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
    public class NpoiExportExcel
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
        public NpoiExportExcel(string fileName) : this(new FileInfo(fileName)) { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file"></param>
        public NpoiExportExcel(FileInfo file)
        {
            try
            {
                FileInfo = file;
                if (file.Exists) { file.Delete(); }
                var stream = new FileStream(file.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Workbook = file.Extension?.ToLower() == ".xls" ? new NPOI.HSSF.UserModel.HSSFWorkbook() : (IWorkbook)new NPOI.XSSF.UserModel.XSSFWorkbook();
            }
        }

        /// <summary>
        /// 写原始数据
        /// </summary>
        public IAlertMsg WriteTable(DataTable table)
        {
            CreateSheet(table);
            try
            {
                // 使用内存去刷新文件
                using (FileStream stream = new FileStream(FileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Workbook.Write(stream);
                }
                return new AlertMsg(true, "保存成功!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new AlertMsg(false, "保存失败!");
            }
        }
        /// <summary>
        /// 写原始数据
        /// </summary>
        public IAlertMsg WriteTable(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                CreateSheet(table);
            }
            try
            {
                // 使用内存去刷新文件
                using (FileStream stream = new FileStream(FileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Workbook.Write(stream);
                }
                return new AlertMsg(true, "保存成功!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new AlertMsg(false, "保存失败!");
            }
        }

        private void CreateSheet(DataTable table)
        {
            var name = table.TableName;
            var sheet = Workbook.CreateSheet(name);
            int rno = 0;
            var row = sheet.CreateRow(rno++);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var col = table.Columns[i];
                row.CreateCell(i).SetCellValue(col.ColumnName);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dataRow = table.Rows[i];
                row = sheet.CreateRow(rno++);
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    row.CreateCell(j).SetCellValue(dataRow.ItemArray[j]?.ToString());
                }
            }
        }
    }
}
