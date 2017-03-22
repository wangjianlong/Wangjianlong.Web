using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    /// <summary>
    /// 位置
    /// </summary>
    [Table("position")]
    public class Position
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int FitmentID { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        [Description("装修")]
        Fitment,
        [Description("附属品")]
        Appendix
    }
}
