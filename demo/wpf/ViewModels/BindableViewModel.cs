using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFUI.SQLiteCipher.ViewModels
{
    /// <summary>
    /// 可绑定的视图模型
    /// </summary>
    public abstract class BindableViewModel : INotifyPropertyChanged
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
        /// <summary>
        /// 设置属性
        /// </summary>
        protected virtual void SetProperty<T>(ref T item, T value, string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
