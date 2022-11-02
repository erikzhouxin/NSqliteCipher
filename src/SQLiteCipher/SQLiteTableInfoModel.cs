using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SQLiteCipher
{
    /// <summary>
    /// 通过表信息得到的结果结构
    /// pragma table_info ('表名')
    /// </summary>
    public class SQLiteTableInfoModel
    {
        /// <summary>
        /// 列标识
        /// </summary>
        public virtual String CID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual String Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual String Type { get; set; }
        /// <summary>
        /// 不为空
        /// </summary>
        public virtual int NotNull { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public virtual String Dflt_Value { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Pk { get; set; }
    }
}
