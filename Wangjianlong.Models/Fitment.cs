﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    /// <summary>
    /// 装修表单
    /// </summary>
    [Table("fitment")]
    public class Fitment
    {
        public Fitment()
        {
            CreateTime = DateTime.Now;
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }
        public string Address { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int CityID { get; set; }
        public virtual City City { get; set; }
        public virtual List<Position> Positions { get; set; }
        public double Sum
        {
            get
            {
                
                return Positions==null?0: Positions.Sum(e => e.Sum);
            }
        }
    }
}
