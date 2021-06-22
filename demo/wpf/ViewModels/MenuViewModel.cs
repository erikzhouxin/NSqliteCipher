using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPFUI.SQLiteCipher.Models;

namespace TestWPFUI.SQLiteCipher.ViewModels
{
    /// <summary>
    /// 菜单视图模型
    /// </summary>
    public class MenuViewModel
    {
        private Func<object> _func;
        private Lazy<object> _content;
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; }
        /// <summary>
        /// 子标题
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="func"></param>
        public MenuViewModel(string title, string subtitle, Func<object> func)
        {
            Title = title;
            Subtitle = subtitle;
            _func = func;
            _content = new Lazy<object>(func, true);
        }
        /// <summary>
        /// 内容
        /// </summary>
        public Object Content { get { return _content.Value; } }
        /// <summary>
        /// 配置项
        /// </summary>
        public DbConnectionConfig Config { get; set; }
        /// <summary>
        /// 重置内容
        /// </summary>
        /// <returns></returns>
        public bool ReleaseContent()
        {
            _content = new Lazy<object>(_func, true);
            return true;
        }
        private bool _isSelected;
        /// <summary>
        /// 选中
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => OnPropertyChanged(nameof(IsSelected), ref _isSelected, value);
        }
        #region // 实现MVVM
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
        #endregion
    }
}
