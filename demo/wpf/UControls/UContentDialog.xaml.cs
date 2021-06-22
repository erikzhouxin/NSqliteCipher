using System;
using System.Collections.Generic;
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
using TestWPFUI.SQLiteCipher.ViewModels;

namespace TestWPFUI.SQLiteCipher.UControls
{
    /// <summary>
    /// UContentDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UContentDialog : BindingViewControl
    {
        private Object _body;
        /// <summary>
        /// 构造
        /// </summary>
        public UContentDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        /// <summary>
        /// 主体
        /// </summary>
        public object Body { get { return _body; } set { _body = value; OnPropertyChanged(nameof(Body)); } }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return this.Visibility == Visibility.Visible; }
            set
            {
                if (value)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// 是否释放
        /// </summary>
        public bool IsRelease
        {
            get { return this.Body == null; }
            set { if (value) { Body = null; IsShow = false; } else { IsShow = true; } }
        }
        /// <summary>
        /// 显示弹框
        /// </summary>
        public void ShowMask()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Visibility = Visibility.Visible;
            });
        }
        /// <summary>
        /// 显示弹框
        /// </summary>
        public void ShowMask(object body)
        {
            this.Dispatcher.Invoke(() =>
            {
                Body = body;
                this.Visibility = Visibility.Visible;
            });
        }
        /// <summary>
        /// 隐藏弹框
        /// </summary>
        public void HideMask()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Visibility = Visibility.Collapsed;
            });
        }
        /// <summary>
        /// 释放弹框
        /// </summary>
        public void ReleaseMask()
        {
            this.Dispatcher.Invoke(() =>
            {
                Body = null;
                this.Visibility = Visibility.Collapsed;
            });
        }
    }
}
