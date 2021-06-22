using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFUI.SQLiteCipher.Models
{
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    public class DbConnectionConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public String ConnString { get; set; }
        /// <summary>
        /// 可否编辑
        /// </summary>
        public bool IsEditable { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public DbConnectionConfig()
        {

        }
    }
}
