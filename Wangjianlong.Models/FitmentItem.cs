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
    /// 装修列表
    /// </summary>
    [Table("fitmentitem")]
    public class FitmentItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int Number { get; set; }
        public int PositionID { get; set; }
    }
}