using System.Data.SQLiteCipher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWPFUI.SQLiteCipher.Models;
using TestWPFUI.SQLiteCipher.ViewModels;
using System.Data;
using System.Data.Dabber;
using CenIdea.Qualimetry.DbExcel;
using System.Windows.Controls.Primitives;
using TestWPFUI.SQLiteCipher.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestWPFUI.SQLiteCipher.UControls
{
    /// <summary>
    /// SQLiteContent.xaml 的交互逻辑
    /// </summary>
    public partial class EFCoreContent : BindableUserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// 主表
        /// </summary>
        public const string MasterTable = "sqlite_master";
        /// <summary>
        /// 表序列
        /// </summary>
        public ObservableCollection<String> Tables { get; }
        /// <summary>
        /// 配置项
        /// </summary>
        public DbConnectionConfig Config { get; }
        /// <summary>
        /// 默认构造
        /// </summary>
        public EFCoreContent() : this(MainViewModel.DefaultConfig) { }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="config"></param>
        public EFCoreContent(DbConnectionConfig config)
        {
            Config = config;
            InitializeComponent();
            Tables = new ObservableCollection<string>();
            this.DataContext = this;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (MainViewModel.Instance.Home.Config.Equals(Config))
            {
                try
                {
                    using(var context = new LocalDatabaseContext(Config.ConnString))
                    {
                        //var count = context.LocalTest.Count();
                        context.LocalTest.Add(new LocalTestEntity
                        {
                            ID = DateTime.Now.Ticks,
                            Name = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
                        });
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BtnRefresh_Click(sender, e);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Tables.Clear();
            Tables.Add(MasterTable);
            try
            {
                using (var conn = new SqliteConnection(Config.ConnString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT [name] FROM [{MasterTable}] WHERE [type]='table' ORDER BY [name]";
                        using (var sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                Tables.Add(sdr.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            this.CmbTables.SelectedIndex = 0;
            BtnSearchTable_Click(sender, e);
        }
        private void BtnSearchTable_Click(object sender, RoutedEventArgs e)
        {
            var tableName = this.CmbTables.Text;
            this.DgvTableContent.Columns.Clear();
            this.DgvTableContent.ItemsSource = null;
            if (string.IsNullOrEmpty(tableName)) { return; }
            var connString = Config.ConnString;
            var querySql = $"SELECT * FROM [{tableName}]";
            var grid = this.DgvTableContent;
            TryQuerySql(connString, querySql, grid);
        }

        private static void TryQuerySql(string connString, string querySql, DataGrid grid)
        {
            grid.Columns.Clear();
            grid.ItemsSource = null;
            MainWindow.ShowLoadingDialog();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var conn = new SqliteConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = querySql;
                            using (var sdr = cmd.ExecuteReader())
                            {
                                var table = new DataTable("Result");
                                for (int i = 0; i < sdr.FieldCount; i++)
                                {
                                    var name = sdr.GetName(i);
                                    table.Columns.Add(name);
                                }
                                if (sdr.HasRows)
                                {
                                    while (sdr.Read())
                                    {
                                        var row = table.NewRow();
                                        for (int i = 0; i < sdr.FieldCount; i++)
                                        {
                                            row[i] = sdr.GetValue(i);
                                        }
                                        table.Rows.Add(row);
                                    }
                                }
                                grid.Dispatcher.Invoke(() => grid.ItemsSource = table.DefaultView);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                finally
                {
                    MainWindow.HideLoadingDialog();
                }
            });
        }

        private static void TryExecuteSql(string connString, string querySql, Action<int> callback)
        {
            MainWindow.ShowLoadingDialog();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var conn = new SqliteConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = querySql;
                            var effLine = cmd.ExecuteNonQuery();
                            MessageBox.Show($"{querySql}\r\n执行SQL成功,影响行数{effLine}", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            callback?.Invoke(effLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                finally
                {
                    MainWindow.HideLoadingDialog();
                }
            });
        }

        private static void ShowException(Exception ex)
        {
            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "运行出错", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void ShowMessage(string message)
        {
            MessageBox.Show(message, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DgvContentList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var rowIndex = e.Row.GetIndex() + 1;
            var width = 25 + (int)Math.Log10(rowIndex) * 8;
            e.Row.Header = new TextBlock()
            {
                Text = rowIndex.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Thickness(10, 0, 0, 0),
                Width = width,
            };
        }

        private void BtnEditConfig_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Selected = new MenuViewModel("SQLite数据库连接", "编辑配置参数", () => new ConfigContent(Config, false));
        }

        private void BtnSearchExec_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.TxtQuerySql.Text))
            {
                ShowMessage("执行SQL不能为空!");
                return;
            }
            TryExecuteSql(Config.ConnString, this.TxtQuerySql.Text, null);
        }

        private void BtnSearchSql_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.TxtQuerySql.Text))
            {
                ShowMessage("查询SQL不能为空!");
                return;
            }
            TryQuerySql(Config.ConnString, this.TxtQuerySql.Text, this.DgvSelectContent);
        }

        private void BtnEmptyTable_Click(object sender, RoutedEventArgs e)
        {
            var tableName = CmbTables.Text;
            if (tableName == MasterTable)
            {
                ShowMessage($"{MasterTable}是系统信息表,无法进行清空操作!");
                return;
            }
            var result = MessageBox.Show($"您将清空[{tableName}]表中所有记录,确认这样操作码?", "温馨提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TryQuerySql(Config.ConnString, $"DELETE FROM [{tableName}]", this.DgvTableContent);
            }
        }

        private void BtnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            var tableName = CmbTables.Text;
            if (tableName == MasterTable)
            {
                ShowMessage($"{MasterTable}是系统信息表,无法进行删除操作!");
                return;
            }
            var result = MessageBox.Show($"您将删除[{tableName}]表,确认这样操作码?", "温馨提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TryExecuteSql(Config.ConnString, $"DROP TABLE [{tableName}]", (l) => this.Dispatcher.Invoke(() => BtnRefresh_Click(sender, e)));
            }
        }

        private void BtnClassToTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnTableToClass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExportTable_Click(object sender, RoutedEventArgs e)
        {
            var cmp = (sender as ButtonBase)?.CommandParameter?.ToString();
            var tableName = this.CmbTables.Text;
            if (Int32.TryParse(cmp, out int cmpVal) && cmpVal == 1)
            {
                tableName = System.IO.Path.GetFileNameWithoutExtension(Config.File);
            }
            this.DgvTableContent.Columns.Clear();
            this.DgvTableContent.ItemsSource = null;
            if (string.IsNullOrEmpty(tableName)) { return; }

            var dialog = new System.Windows.Forms.SaveFileDialog();
            // 设置保存的文件的类型，注意过滤器的语法
            dialog.Title = $"保存{tableName}";
            dialog.Filter = "Excel表格文件|*.xls;*.xlsx";
            dialog.OverwritePrompt = true;
            dialog.FileName = $"{DateTime.Now:yyyy-MM-dd}.{tableName}.xlsx";
            // 调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var dataSet = new DataSet();
                var tableList = Tables;
                if (cmpVal != 1)
                {
                    tableList = new ObservableCollection<string> { tableName };
                }
                foreach (var item in tableList)
                {
                    dataSet.Tables.Add(GetDataTable(item));
                }
                IAlertMsg result = new NpoiExportExcel(dialog.FileName).WriteTable(dataSet);
                if (result.IsSuccess)
                {
                    MessageBox.Show($"导出{tableName}到文件成功!", "导出成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"导出{tableName}到文件失败!", "导出失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private DataTable GetDataTable(string tableName)
        {
            var table = new DataTable(tableName);
            try
            {
                using (var conn = new SqliteConnection(Config.ConnString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT * FROM [{tableName}]";
                        using (var sdr = cmd.ExecuteReader())
                        {
                            for (int i = 0; i < sdr.FieldCount; i++)
                            {
                                var name = sdr.GetName(i);
                                table.Columns.Add(name);
                            }
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    var row = table.NewRow();
                                    for (int i = 0; i < sdr.FieldCount; i++)
                                    {
                                        row[i] = sdr.GetValue(i);
                                    }
                                    table.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            return table;
        }

        private void BtnImportTable_Click(object sender, RoutedEventArgs e)
        {
            var cmp = (sender as ButtonBase)?.CommandParameter?.ToString();
            var tableName = this.CmbTables.Text;
            if (Int32.TryParse(cmp, out int cmpVal) && cmpVal == 1)
            {
                tableName = System.IO.Path.GetFileNameWithoutExtension(Config.File);
            }
            this.DgvTableContent.Columns.Clear();
            this.DgvTableContent.ItemsSource = null;
            if (string.IsNullOrEmpty(tableName)) { return; }

            var dialog = new System.Windows.Forms.OpenFileDialog();
            // 设置保存的文件的类型，注意过滤器的语法
            dialog.Title = $"导入{tableName}";
            dialog.Filter = "Excel表格文件|*.xls;*.xlsx";
            // 调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var result = new NpoiImportExcel(dialog.FileName).GetTable(tableName);
                if (result.Rows.Count == 0 || result.Columns.Count == 0)
                {
                    MessageBox.Show("没有数据不导入");
                    return;
                }
                IDbTransaction trans = null;
                try
                {
                    var cols = new List<string>();
                    foreach (DataColumn col in result.Columns)
                    {
                        cols.Add(col.ColumnName);
                    }
                    var querySql = $"REPLACE INTO [{tableName}]([{string.Join("],[", cols)}]) VALUES(@{string.Join(",@", cols)})";
                    var args = new List<Dictionary<string, object>>();
                    var colCount = result.Columns.Count;
                    foreach (DataRow item in result.Rows)
                    {
                        var dic = new Dictionary<string, object>();
                        for (int j = 0; j < colCount; j++)
                        {
                            dic.Add(cols[j], item[j]);
                        }
                        args.Add(dic);
                    }
                    using (var conn = new SqliteConnection(Config.ConnString))
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();
                        foreach (var item in args)
                        {
                            conn.Execute(querySql, item, trans);
                        }
                        trans.Commit();
                    }
                    MessageBox.Show("导入成功!");
                    BtnSearchTable_Click(sender, e);
                }
                catch (Exception ex)
                {
                    if (trans != null) { trans.Rollback(); }
                    MessageBox.Show($"导入失败:{ex.Message}\r\n{ex.StackTrace}");
                }
            }
        }

        private void VacuumDatabase_Click(object sender, RoutedEventArgs e)
        {
            TryExecuteSql(Config.ConnString, "vacuum", null);
        }
    }
}
