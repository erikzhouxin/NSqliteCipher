using System;
using System.Collections.Generic;
using System.Text;

namespace TestWPFUI.SQLiteCipher.Models
{
    /// <summary>
    /// 数据菜单模型
    /// </summary>
    public class DbMenuModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        public String SubTitle { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public DbConnectionConfig Config { get; set; }
    }
}
