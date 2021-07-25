using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFUI.SQLiteCipher.ViewModels
{
    /// <summary>
    /// 绑定的视图控件
    /// </summary>
    public abstract class BindableUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// 属性更改
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性变化时
        /// </summary>
        public void OnPropertyChanged<T>(string propertyName, ref T model, T value)
        {
            model = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 属性变化时
        /// </summary>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
