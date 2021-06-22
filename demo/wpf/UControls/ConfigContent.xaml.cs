using System;
using System.Collections.Generic;
using System.IO;
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

namespace TestWPFUI.SQLiteCipher.UControls
{
    /// <summary>
    /// ConfigContent.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigContent
    {
        /// <summary>
        /// 配置
        /// </summary>
        public DbConnectionConfig Config { get; }
        private string _fileName;
        private string _title;
        private string _password;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        /// <summary>
        /// 是新添加
        /// </summary>
        public bool IsNew { get; }
        /// <summary>
        /// 是可编辑
        /// </summary>
        public bool IsEditable { get; }
        /// <summary>
        /// 构造
        /// </summary>
        public ConfigContent() : this(new DbConnectionConfig(), true)
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config"></param>
        public ConfigContent(DbConnectionConfig config) : this(config, false) { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config"></param>
        /// <param name="isNew"></param>
        public ConfigContent(DbConnectionConfig config, bool isNew)
        {
            InitializeComponent();
            this.DataContext = this;
            Config = config;
            FileName = config.File;
            Title = config.Name;
            Password = config.Password;
            IsNew = isNew;
            IsEditable = config.IsEditable;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ViewModel.GoHome();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsEditable)
            {
                if (string.IsNullOrWhiteSpace(this.Title))
                {
                    MessageBox.Show("请输入[配置标题]内容", "温馨提示");
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.FileName))
                {
                    MessageBox.Show("请输入[文件路径]内容", "温馨提示");
                    return;
                }
                Config.Name = Title;
                Config.File = this.FileName;
                Config.Password = this.Password;
                if (string.IsNullOrWhiteSpace(this.Password))
                {
                    Config.ConnString = $"DataSource={this.FileName};";
                }
                else
                {
                    Config.ConnString = $"DataSource={this.FileName};Password={this.Password};";
                }
                if (this.IsNew)
                {
                    var subTitle = System.IO.Path.GetFileName(this.FileName);
                    MainViewModel.Instance.Menus.Add(new MenuViewModel(this.Title, subTitle, () => new SQLiteContent(Config))
                    {
                        Config = Config,
                    });
                }
                else
                {
                    foreach (var item in MainViewModel.Instance.Menus)
                    {
                        item.ReleaseContent();
                    }
                }
                MainViewModel.Instance.GoHome();
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

        private void BtnOpenFile_Click(object sender, MouseButtonEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "打开或新建SQLite数据库";
            dialog.Filter = "SQLite数据库(.sqlite3,.db3)|*.sqlite3;*.db3;*.db;*.sqlite;";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            //dialog.InitialDirectory = Environment.CurrentDirectory;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FileName = dialog.FileName;
            }
        }
    }
}
