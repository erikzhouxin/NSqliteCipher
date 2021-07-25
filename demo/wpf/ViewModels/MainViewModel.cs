using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestWPFUI.SQLiteCipher.Models;
using TestWPFUI.SQLiteCipher.UControls;

namespace TestWPFUI.SQLiteCipher.ViewModels
{
    /// <summary>
    /// 主窗体视图
    /// </summary>
    public class MainViewModel : BindableViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// 默认文件
        /// </summary>
        public const string DefaultFile = nameof(SQLiteCipher) + ".sqlite3";
        /// <summary>
        /// 文件名称
        /// </summary>
        private static readonly string DefaultFileName = EConfiguration.ResDirectory + DefaultFile;
        /// <summary>
        /// 默认配置
        /// </summary>
        public static DbConnectionConfig DefaultConfig { get; } = new DbConnectionConfig
        {
            Name = "程序内部数据库",
            File = DefaultFileName,
            ConnString = $"Data Source={DefaultFileName};Password={nameof(SQLiteCipher)};Version=3",
            IsEditable = false,
            Version = 3,
        };
        /// <summary>
        /// 单例实例
        /// </summary>
        public static MainViewModel Instance { get; } = new MainViewModel();
        private MenuViewModel _selected;
        /// <summary>
        /// 菜单项
        /// </summary>
        public ObservableCollection<MenuViewModel> Menus { get; }
        /// <summary>
        /// 首页
        /// </summary>
        public MenuViewModel Home { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        private MainViewModel()
        {
            Menus = new ObservableCollection<MenuViewModel>();
        }
        /// <summary>
        /// 选中项
        /// </summary>
        public MenuViewModel Selected { get { return _selected; } set { SelectMenu(value); } }
        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public MenuViewModel SelectMenu(MenuViewModel menu)
        {
            if (menu == null || menu.Equals(_selected)) { return _selected; }
            _selected = menu;
            _selected.IsSelected = true;
            OnPropertyChanged(nameof(Selected));
            return _selected;
        }
        /// <summary>
        /// 刷新选中内容
        /// </summary>
        public void RefreshContent()
        {
            Selected.ReleaseContent();
            OnPropertyChanged(nameof(Selected));
        }
        /// <summary>
        /// 跳转到首页
        /// </summary>
        public void GoHome()
        {
            Selected = Home;
        }
        /// <summary>
        /// 设置首页
        /// </summary>
        /// <param name="home"></param>
        public void SetHome(MenuViewModel home)
        {
            Home = home;
        }

    }
}
