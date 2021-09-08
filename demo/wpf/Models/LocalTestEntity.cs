using System;
using System.Collections.Generic;
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
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
