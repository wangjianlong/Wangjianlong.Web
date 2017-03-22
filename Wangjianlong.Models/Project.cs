using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    [Table("project")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 缩写
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
