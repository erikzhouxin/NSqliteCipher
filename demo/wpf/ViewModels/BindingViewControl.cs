using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace TestWPFUI.SQLiteCipher.ViewModels
{
    /// <summary>
    /// 绑定的视图控件
    /// </summary>
    public abstract class BindingViewControl : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变化
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性变化时
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
