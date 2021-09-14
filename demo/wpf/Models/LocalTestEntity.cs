using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPFUI.SQLiteCipher.Models
{
    /// <summary>
    /// 本地测试实体
    /// </summary>
    [Table("local_test")]
    public class LocalTestEntity
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public String Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public String Value { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remarks { get; set; }
    }
}
