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
        public bool Lock { get; set; }
        public virtual List<FitmentItem> Items { get; set; }
        public double Sum
        {
            get
            {
                if (Items == null)
                {
                    return .0;
                }
                return Items.Sum(e => e.Sum);
            }
        }
    }

    public enum Category
    {
        [Description("装潢")]
        Fitment,
        [Description("附属物")]
        Appendix
    }
}
