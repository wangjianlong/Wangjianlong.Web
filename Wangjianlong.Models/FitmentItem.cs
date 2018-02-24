using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    /// <summary>
    /// 装修列表
    /// </summary>
    [Table("fitmentitem")]
    public class FitmentItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }
        /// <summary>
        /// 公式
        /// </summary>
        public string Formula { get; set; }
        public double Number { get; set; }
        public int PositionID { get; set; }
        public virtual Position Position { get; set; }
        /// <summary>
        /// 成新
        /// </summary>
        public double NewOld { get; set; }
        /// <summary>
        /// 指定单价
        /// </summary>
        public int? Price { get; set; }

        public double Sum
        {
            get
            {
                if (Price.HasValue)
                {
                    return Math.Floor(Price.Value * Number * NewOld / 100 + 0.5);
                }
                if (Project == null)
                {
                    return .0;
                }
                return  Math.Floor(Project.Price * Number * NewOld / 100 + 0.5);
            }
        }
      
    }
}
