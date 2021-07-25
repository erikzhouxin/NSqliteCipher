using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// 连接字符串关键词
    /// </summary>
    public enum SqliteConnKeywords
    {
        /// <summary>
        /// 数据源
        /// </summary>
        DataSource = 0,
        /// <summary>
        /// 模式
        /// </summary>
        Mode = 1,
        /// <summary>
        /// 缓存
        /// </summary>
        Cache = 2,
        /// <summary>
        /// 密码
        /// </summary>
        Password = 3,
        /// <summary>
        /// 外键
        /// </summary>
        ForeignKeys = 4,
        /// <summary>
        /// 递归触发器
        /// </summary>
        RecursiveTriggers = 5,
        /// <summary>
        /// 版本号
        /// </summary>
        Version = 6,
        /// <summary>
        /// 提供者
        /// </summary>
        Provider = 7,
        /// <summary>
        /// 驱动
        /// </summary>
        Driver = 8,
    }
}
