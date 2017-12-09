using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    [Table("item_project_position")]
    public class ItemProjectPosition
    {
        [Key]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public string Formula { get; set; }
        public double Number { get; set; }
        public double NewOld { get; set; }
        [NotMapped]
        public double Sum
        {
            get
            {
                return Math.Floor(Price * Number*NewOld/100+0.5); 
            }
        }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public int FitmentID { get; set; }
        public Category Category { get; set; }
    }
}
