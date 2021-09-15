using System;
using System.Collections.Generic;
using System.Data.Cobber;
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
using TestWPFUI.SQLiteCipher.UControls;
using TestWPFUI.SQLiteCipher.ViewModels;

namespace TestWPFUI.SQLiteCipher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static MainWindow Instance { get; private set; }
        /// <summary>
        /// 主视图
        /// </summary>
        public static MainViewModel ViewModel { get { return MainViewModel.Instance; } }
        /// <summary>
        /// 构造
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            this.DataContext = ViewModel;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var home = new MenuViewModel("内置数据库", MainViewModel.DefaultFile, () => new SQLiteContent(MainViewModel.DefaultConfig))
            {
                Config = MainViewModel.DefaultConfig,
            };
            ViewModel.SetHome(home);
            ViewModel.Menus.Add(home);
            foreach (var item in EConfiguration.Menus)
            {
                ViewModel.Menus.Add(new MenuViewModel(item.Title, item.SubTitle, () => new SQLiteContent(item.Config))
                {
                    Config = item.Config,
                });
            }
            ViewModel.Selected = home;
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "打开或新建SQLite数据库";
            dialog.Filter = "SQLite数据库(.sqlite3,.db3)|*.sqlite3;*.db3;*.db;*.sqlite;";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            //dialog.InitialDirectory = Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var config = new DbConnectionConfig
                {
                    Name = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName),
                    File = dialog.FileName,
                    ConnString = $"DataSource={dialog.FileName};",
                    IsEditable = true,
                };
                ViewModel.Selected = new MenuViewModel("SQLite数据库连接", "编辑配置参数", () => new ConfigContent(config, true))
                {
                    Config = config,
                };
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var menu = (sender as ListView)?.SelectedItem as MenuViewModel;
            if (menu != null)
            {
                ViewModel.Selected = menu;
            }
        }

        private void OnRunCmd(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var param = e.Parameter?.ToString();
                switch (param)
                {
                    case "Refresh":
                        ViewModel.RefreshContent();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("快捷操作执行错误!\r\n" + ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = ((MenuItem)sender)?.CommandParameter as MenuViewModel;
            if (menuItem != null && menuItem.Content is SQLiteContent menuContent)
            {
                ViewModel.Selected = new MenuViewModel("SQLite数据库连接", "编辑配置参数", () => new ConfigContent(menuContent.Config, false))
                {
                    Config = menuContent.Config,
                };
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = ((MenuItem)sender)?.CommandParameter as MenuViewModel;
            if (menuItem != null)
            {
                ViewModel.Menus.Remove(menuItem);
                EConfiguration.Menus.Clear();
                foreach (var item in MainViewModel.Instance.Menus.Skip(1))
                {
                    EConfiguration.Menus.Add(new DbMenuModel
                    {
                        Title = item.Title,
                        SubTitle = item.Subtitle,
                        Config = item.Config,
                    });
                }
                EConfiguration.TrySave();
            }
        }
        /// <summary>
        /// 显示内容面罩
        /// </summary>
        public void ShowContentMask()
        {
            this.ContentLoading.ShowMask();
        }
        /// <summary>
        /// 隐藏内容面罩
        /// </summary>
        public void HideContentMask()
        {
            this.ContentLoading.HideMask();
        }
        /// <summary>
        /// 显示对话框面罩
        /// </summary>
        public void ShowDialogMask(Object content)
        {
            this.ContentDialog.ShowMask(content);
        }
        /// <summary>
        /// 隐藏对话框面罩
        /// </summary>
        public void HideDialogMask()
        {
            this.ContentDialog.ReleaseMask();
        }
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="content"></param>
        public static void ShowContentDialog(object content)
        {
            MainWindow.Instance.ContentDialog.ShowMask(content);
        }
        /// <summary>
        /// 显示等待对话框
        /// </summary>
        public static void ShowLoadingDialog()
        {
            MainWindow.Instance.ContentLoading.ShowMask();
        }
        /// <summary>
        /// 显示对话框
        /// </summary>
        public static void HideContentDialog()
        {
            MainWindow.Instance.ContentDialog.HideMask();
        }
        /// <summary>
        /// 显示等待对话框
        /// </summary>
        public static void HideLoadingDialog()
        {
            MainWindow.Instance.ContentLoading.HideMask();
        }
        private LazyBone<MenuViewModel> _contextModel = new LazyBone<MenuViewModel> (()=>
        {
            return new MenuViewModel("内置数据库", MainViewModel.DefaultFile, () => new EFCoreContent(MainViewModel.DefaultConfig))
            {
                Config = MainViewModel.DefaultConfig,
            };
        });
        private void BtnOpenEFCore_Click(object sender, RoutedEventArgs e)
        {
            var cache = new CacheConcurrentModel();
            var distTicks = 100000000;
            var key = (DateTime.Now.Ticks / distTicks).ToString();
            if (!cache.Get<bool>(key))
            {
                cache.Set(key, true, new DateTimeOffset(DateTime.Now.AddTicks(distTicks)));
                _contextModel.Reload();
            }
            ViewModel.Selected = _contextModel.Value;
        }
    }
}
