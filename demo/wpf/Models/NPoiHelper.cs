using System.Data.Dabber;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CenIdea.Qualimetry.DbExcel
{
    /// <summary>
    /// ExcelNPoi帮助
    /// </summary>
    internal static class ExcelNPoiHelper
    {
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IWorkbook CreateWorkbook(string path)
        {
            var extType = Path.GetExtension(path).ToLower();
            IWorkbook workbook;
            switch (extType)
            {
                case ".xls":
                    workbook = new XSSFWorkbook();
                    break;
                case ".xlsx":
                    workbook = new SXSSFWorkbook();
                    break;
                default:
                    workbook = new XSSFWorkbook(path);
                    break;
            }
            return workbook;
        }
        /// <summary>
        /// 读取工作簿
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IWorkbook ReadWorkbook(string path)
        {
            var extType = Path.GetExtension(path).ToLower();
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(fs);
            return workbook;
        }
        /// <summary>
        /// 只读工作簿
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IWorkbook OnlyReadWorkbook(string path)
        {
            var extType = Path.GetExtension(path).ToLower();
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IWorkbook workbook = new XSSFWorkbook(fs);
            return workbook;
        }
        /// <summary>
        /// 创建单元格
        /// </summary>
        /// <param name="dataTbRow"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ICell CreateCell(IRow dataTbRow, int index, string value)
        {
            ICell cell = dataTbRow.CreateCell(index);
            cell.SetCellValue(value);
            return cell;
        }
        /// <summary>
        /// 创建单元格
        /// </summary>
        /// <param name="dataTbRow"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static ICell CreateCell(IRow dataTbRow, int index, string value, ICellStyle style)
        {
            ICell cell = dataTbRow.CreateCell(index);
            cell.SetCellValue(value);
            cell.CellStyle = style;
            return cell;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static int DataTableToExcel(IWorkbook workbook, DataTable data, string sheetName, bool isColumnWritten)
        {
            try
            {
                int i = 0;
                int j = 0;
                int count = 0;
                ISheet sheet = null;
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                //workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool SaveExcel(IWorkbook workbook, string filePath)
        {
            using (FileStream fsWrite = File.OpenWrite(filePath))
            {
                workbook.Write(fsWrite);
            }
            return true;
        }

        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fileFolder"></param>
        /// <returns></returns>
        public static IAlertMsg SaveGuidExcel(IWorkbook workbook, string fileFolder)
        {
            var ext = workbook is NPOI.XSSF.UserModel.XSSFWorkbook ? ".xlsx" : ".xls";
            var fileName = fileFolder + Guid.NewGuid().ToString("N") + ext;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                try
                {
                    using (FileStream fsWrite = new FileStream(fileName, FileMode.Create))
                    {
                        var array = ms.ToArray();
                        fsWrite.Write(array, 0, array.Length);
                        fsWrite.Flush();
                    }
                    return new AlertMsg(true, "保存成功") { Data = new { FileName = fileName } };
                }
                catch (Exception)
                {
                    return new AlertMsg(false, "保存失败") { Data = new { FileName = fileName } };
                }
            }
        }
        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="fileFolder"></param>
        /// <returns></returns>
        public static IAlertMsg SaveSha1Excel(IWorkbook workbook, string fileFolder)
        {
            var ext = workbook is NPOI.XSSF.UserModel.XSSFWorkbook ? ".xlsx" : ".xls";
            var fileName = fileFolder + Guid.NewGuid().ToString("N") + ext;
            try
            {
                using (FileStream fsWrite = new FileStream(fileName, FileMode.Create))
                {
                    workbook.Write(fsWrite);
                }
                workbook.Close();
                return new AlertMsg(true, "保存成功") { Data = new { FileName = fileName } };
            }
            catch (Exception)
            {
                return new AlertMsg(false, "保存失败") { Data = new { FileName = fileName } };
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(IWorkbook workbook, string sheetName, bool isFirstRowColumn)
        {
            var tabName = string.IsNullOrWhiteSpace(sheetName) ? "Sheet1" : sheetName;
            DataTable data = new DataTable(tabName);
            try
            {
                ISheet sheet = null;
                int startRow = 0;
                //fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                //    workbook = new XSSFWorkbook(fs);
                //else if (fileName.IndexOf(".xls") > 0) // 2003版本
                //    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        #region // 读
        /// <summary>
        /// 获取以第一行为列名的Excel内容列表
        /// </summary>
        /// <typeparam name="T">需要转换的模型</typeparam>
        /// <param name="filePath">包括文件名在内的全路径</param>
        /// <param name="sheetName">需要获取的Sheet名称,失败时获取第一个工作表</param>
        /// <returns></returns>
        public static List<T> GetExcelCells<T>(string filePath, string sheetName) where T : new()
        {
            var workbook = OnlyReadWorkbook(filePath);
            // 定义集合    
            List<T> ts = new List<T>();
            // 获得此模型的类型   
            Type type = typeof(T);
            // 获得此模型的公共属性      
            PropertyInfo[] propertys = type.GetProperties();
            var columsDic = propertys.Where(s => s.CanWrite).ToDictionary(s => s.Name, s => s);
            try
            {
                ISheet sheet = null;
                if (!string.IsNullOrWhiteSpace(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    var columDic = new Dictionary<int, PropertyInfo>();
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (!string.IsNullOrWhiteSpace(cellValue) && columsDic.Keys.Contains(cellValue))
                            {
                                columDic.Add(i, columsDic[cellValue]);
                            }
                        }
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = 1; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　
                        T model = new T();
                        foreach (var item in columDic)
                        {
                            var cellVal = row.GetCell(item.Key);
                            if (cellVal != null) //同理，没有数据的单元格都默认是null
                            {
                                string cellString;
                                if (cellVal.CellType == CellType.Formula)
                                {
                                    cellVal.SetCellType(CellType.String);
                                    cellString = cellVal.StringCellValue;
                                }
                                else
                                {
                                    cellString = cellVal.ToString();
                                }
                                if (!string.IsNullOrWhiteSpace(cellString))
                                {
                                    item.Value.SetValue(model, cellString, null);
                                }
                            }
                        }
                        ts.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
            return ts;
        }
        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellString(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))//日期
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.String:
                    return cell.StringCellValue.Trim();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Formula://公式
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        if (DateUtil.IsCellDateFormatted(cell))//日期
                        {
                            return cell.DateCellValue.ToString();
                        }
                        else
                        {
                            return cell.NumericCellValue.ToString();
                        }
                    }
                case CellType.Boolean:
                default:
                    return cell.ToString();
            }
        }
        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))//日期
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.String:
                    return cell.StringCellValue.Trim();
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula://公式
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        if (DateUtil.IsCellDateFormatted(cell))//日期
                        {
                            return cell.DateCellValue;
                        }
                        else
                        {
                            return cell.NumericCellValue;
                        }
                    }
                default:
                    return cell.ToString();
            }
        }
        /// <summary>
        /// 获取Excel中的Sheet为DataTable
        /// </summary>
        /// <param name="filePath">文件目录</param>
        /// <param name="sheetName">SheetName,当找不到时默认为第一个Sheet</param>
        /// <param name="isFirstColumn">是否第一行为列名,默认是</param>
        /// <returns></returns>
        public static DataTable GetExcelTable(string filePath, string sheetName, bool isFirstColumn = true)
        {
            var workbook = ReadWorkbook(filePath);
            return ExcelToDataTable(workbook, sheetName, isFirstColumn);
        }
        /// <summary>
        /// 获取单元格列表
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static List<dynamic> GetExcelCells(string filePath, string sheetName)
        {
            var workbook = ReadWorkbook(filePath);
            // 定义集合    
            List<dynamic> ts = new List<dynamic>();
            try
            {
                ISheet sheet = null;
                if (!string.IsNullOrWhiteSpace(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                if (sheet == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    var columDic = new Dictionary<int, string>();
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (!string.IsNullOrWhiteSpace(cellValue) && !columDic.Values.Contains(cellValue))
                            {
                                columDic.Add(i, cellValue);
                            }
                        }
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = 1; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　
                        dynamic mObj = new ExpandoObject();
                        var model = (IDictionary<string, object>)mObj;
                        foreach (var item in columDic)
                        {
                            var cellVal = row.GetCell(item.Key);
                            if (cellVal != null) //同理，没有数据的单元格都默认是null
                            {
                                string cellString;
                                if (cellVal.CellType == CellType.Formula)
                                {
                                    cellVal.SetCellType(CellType.String);
                                    cellString = cellVal.StringCellValue;
                                }
                                else
                                {
                                    cellString = cellVal.ToString();
                                }
                                model[item.Value] = cellString;
                            }
                            else
                            {
                                model[item.Value] = string.Empty;
                            }
                        }
                        ts.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
            return ts;
        }
        #endregion
        #region // 写
        /// <summary>
        /// 获取Excel中的Sheet为DataTable
        /// </summary>
        /// <returns></returns>
        public static bool UpdateExcelTable(string filePath, DataTable dt)
        {
            var sheetName = dt.TableName;
            var workbook = ReadWorkbook(filePath);
            // 判断是否存在Sheet
            ISheet sheet = workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                sheet = workbook.CreateSheet(sheetName);
            }
            IRow firstRow = sheet.GetRow(0);
            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
            var columnDic = new Dictionary<string, int>();
            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
            {
                ICell cell = firstRow.GetCell(i);
                if (cell != null)
                {
                    string cellValue = cell.StringCellValue;
                    if (!string.IsNullOrWhiteSpace(cellValue) && dt.Columns.Contains(cellValue))
                    {
                        columnDic.Add(cellValue, i);
                    }
                }
            }
            var rowIndex = 1;
            foreach (DataRow rowVal in dt.Rows)
            {
                IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                foreach (var item in columnDic)
                {
                    var val = rowVal[item.Key];
                    if (val == null)
                    {
                        continue;
                    }
                    var cell = row.GetCell(item.Value);
                    if (cell == null)
                    {
                        cell = row.CreateCell(item.Value);
                    }
                    cell.SetCellValue(val.ToString());
                }
                rowIndex++;
            }
            SaveExcel(workbook, filePath);
            return true;
        }
        #endregion

        /// <summary>
        /// 根据文件路径，获取表格集合
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<DataTable> GetDataTableList(string filePath)
        {
            var list = new ConcurrentBag<DataTable>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var isExcel2007 = filePath.IsExcel2007();
                var workBook = stream.GetWorkbook(isExcel2007);
                var sheetIndexList = new List<int>();
                for (int i = 0; i < workBook.NumberOfSheets; i++) sheetIndexList.Add(i);
                Parallel.ForEach(sheetIndexList, new ParallelOptions
                {
                    MaxDegreeOfParallelism = 3
                }, (source, state, index) =>
                {
                    try
                    {
                        if (!workBook.IsSheetHidden(source))
                            list.Add(GetDataTableToY(workBook, source));
                    }
                    catch (NPOI.POIFS.FileSystem.OfficeXmlFileException nopiEx)
                    {
                        Console.WriteLine($"SheetIndex:{index}\t\tException:{nopiEx.Message}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }

            return list.ToList();
        }

        /// <summary>
        /// 根据sheet索引，把数据转换为datatable,以Y轴为准
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="sheetIndex">sheet索引</param>
        /// <param name="validRowIndex"></param>
        /// <returns></returns>
        public static DataTable GetDataTableToY(IWorkbook workBook, int sheetIndex, int validRowIndex = 0)
        {
            var sheet = workBook.GetSheetAt(sheetIndex);
            var table = new DataTable(sheet.SheetName);

            //  设置最大列，默认为1
            var maxColumnNum = 1;
            //  不是有效列集合，连续超过三行不读取后续所有列
            var noValidColumnList = new List<int>();
            //  列：按照列把数据填充到datatable中，防止无限列出现
            for (var columnIndex = 0; columnIndex < maxColumnNum; columnIndex++)
            {
                var column = new DataColumn();
                table.Columns.Add(column);
                noValidColumnList.Add(columnIndex);
                //  列中所有数据都是null为true
                var isAllEmpty = true;
                //  行
                for (var rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++)
                {
                    if (columnIndex == 0) table.Rows.Add(table.NewRow());
                    var itemRow = sheet.GetRow(rowIndex);
                    if (itemRow == null) continue;
                    maxColumnNum = maxColumnNum < itemRow.LastCellNum ? itemRow.LastCellNum : maxColumnNum;
                    //  把格式转换为utf-8
                    var itemCellValue = itemRow.GetValue(columnIndex).ToString();
                    if (!string.IsNullOrWhiteSpace(itemCellValue)) isAllEmpty = false;
                    table.Rows[rowIndex][columnIndex] = itemCellValue;
                }

                //  当前列有值
                if (!isAllEmpty)
                    noValidColumnList.Clear();
                //  连续空白列超过三行 或 有空白行且当前行为最后一行
                else if (noValidColumnList.Count > 3 || (noValidColumnList.Count > 0 && columnIndex == maxColumnNum - 1))
                {
                    for (var i = noValidColumnList.Count - 1; i >= 0; i--)
                        table.Columns.RemoveAt(noValidColumnList[i]);
                    break;
                }
            }
            // 得到一个sheet中有多少个合并单元格
            int sheetMergeCount = sheet.NumMergedRegions;
            for (var i = 0; i < sheetMergeCount; i++)
            {
                // 获取合并后的单元格
                var range = sheet.GetMergedRegion(i);
                sheet.IsMergedRegion(range);
                var cellValue = string.Empty;
                for (var mRowIndex = range.FirstRow; mRowIndex <= range.LastRow; mRowIndex++)
                {
                    for (var mColumnIndex = range.FirstColumn; mColumnIndex <= range.LastColumn; mColumnIndex++)
                    {
                        var itemCellValue = table.Rows[range.FirstRow][range.FirstColumn].ToString();
                        if (!string.IsNullOrWhiteSpace(itemCellValue))
                            cellValue = itemCellValue;
                        table.Rows[mRowIndex][mColumnIndex] = cellValue;
                    }
                }
            }

            return table;
        }

        #region 公共方法

        /// <summary>
        /// 判断excel是否是2007版本：.xls
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExcel2007(this string filePath)
        {
            return Path.GetExtension(filePath)?.ToLower() == ".xls";
        }

        /// <summary>
        /// 根据版本创建IWorkbook对象
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isExcel2007"></param>
        /// <returns></returns>
        public static IWorkbook GetWorkbook(this Stream stream, bool isExcel2007)
        {
            return isExcel2007 ? (IWorkbook)new HSSFWorkbook(stream) : new XSSFWorkbook(stream);
        }
        /// <summary>
        /// 获取XSSFRow的值（全部统一转成字符串）
        /// </summary>
        /// <param name="row"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetValue(this IRow row, int index)
        {
            var rowCell = row.GetCell(index);
            return GetValueByCellStyle(rowCell, rowCell?.CellType);
        }

        /// <summary>
        /// 根据单元格的类型获取单元格的值
        /// </summary>
        /// <param name="rowCell"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetValueByCellStyle(ICell rowCell, CellType? type)
        {
            string value = string.Empty;
            switch (type)
            {
                case CellType.String:
                    value = rowCell.StringCellValue;
                    break;
                case CellType.Numeric:
                    if (DateUtil.IsCellInternalDateFormatted(rowCell))
                    {
                        value = DateTime.FromOADate(rowCell.NumericCellValue).ToString();
                    }
                    else if (DateUtil.IsCellDateFormatted(rowCell))
                    {
                        value = DateTime.FromOADate(rowCell.NumericCellValue).ToString();
                    }
                    //有些情况，时间搓？数字格式化显示为时间,不属于上面两种时间格式
                    else if (rowCell.CellStyle.GetDataFormatString() == null)
                    {
                        value = DateTime.FromOADate(rowCell.NumericCellValue).ToString();
                    }
                    else if (rowCell.CellStyle.GetDataFormatString().Contains("$"))
                    {
                        value = "$" + rowCell.NumericCellValue.ToString();
                    }
                    else if (rowCell.CellStyle.GetDataFormatString().Contains("￥"))
                    {
                        value = "￥" + rowCell.NumericCellValue.ToString();
                    }
                    else if (rowCell.CellStyle.GetDataFormatString().Contains("¥"))
                    {
                        value = "¥" + rowCell.NumericCellValue.ToString();
                    }
                    else if (rowCell.CellStyle.GetDataFormatString().Contains("€"))
                    {
                        value = "€" + rowCell.NumericCellValue.ToString();
                    }
                    else
                    {
                        value = rowCell.NumericCellValue.ToString();
                    }
                    break;
                case CellType.Boolean:
                    value = rowCell.BooleanCellValue.ToString();
                    break;
                case CellType.Error:
                    value = ErrorEval.GetText(rowCell.ErrorCellValue);
                    break;
                case CellType.Formula:
                    //  TODO: 是否存在 嵌套 公式类型
                    value = GetValueByCellStyle(rowCell, rowCell?.CachedFormulaResultType);
                    break;
            }
            return value;
        }

        #endregion


        /// <summary>
        /// 获取指定工作表
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static ISheet TryGetSheet(this IWorkbook workbook, string sheetName)
        {
            ISheet sheet = null;
            try
            {
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                if (sheet == null) // 如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                {
                    sheet = workbook.GetSheetAt(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return sheet;
        }
        /// <summary>
        /// 保存工作簿
        /// </summary>
        /// <param name="workbook"></param>
        public static void SaveWorkbook(this IWorkbook workbook)
        {
            try
            {
                // 使用内存去刷新文件
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.Write(stream);
                    stream.Flush();
                }
                workbook.Close(); // 保存完毕后关闭文件
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 保存工作簿
        /// </summary>
        /// <param name="workbook"></param>
        public static void SaveNotClose(this IWorkbook workbook)
        {
            try
            {
                // 使用内存去刷新文件
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.Write(stream);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #region // 内部类
        /// <summary>
        /// Excel信息
        /// </summary>
        public class ExcelInfo
        {
            /// <summary>
            /// 路径
            /// </summary>
            public string FileName { get; set; }
            /// <summary>
            /// xlsx还是xls
            /// </summary>
            public bool Is2007 { get; set; }
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="path"></param>
            public ExcelInfo(string path)
            {
                FileName = path;
                var extType = Path.GetExtension(path).ToLower();
                switch (extType)
                {
                    case ".xls":
                        Is2007 = false;
                        break;
                    case ".xlsx":
                    default:
                        Is2007 = true;
                        break;
                }
            }

            /// <summary>
            /// 获取
            /// </summary>
            /// <returns></returns>
            public IWorkbook GetWorkbook()
            {
                if (Is2007)
                {
                    return new XSSFWorkbook(FileName);
                }
                return new HSSFWorkbook(File.OpenRead(FileName));
            }
            /// <summary>
            /// 获取工作表
            /// </summary>
            /// <param name="sheetName"></param>
            /// <returns></returns>
            public ISheet TryGetSheet(string sheetName)
            {
                ISheet sheet = null;
                try
                {
                    var workbook = GetWorkbook();
                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        sheet = workbook.GetSheet(sheetName);
                    }
                    if (sheet == null) // 如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return sheet;
            }
            /// <summary>
            /// 获取不包含路径的企业名称
            /// </summary>
            /// <returns></returns>
            public string GetFileName()
            {
                return FileName.Substring(FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
    }
}
