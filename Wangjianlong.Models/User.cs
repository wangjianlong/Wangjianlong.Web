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
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 系统名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 显示名称（真实名称）
        /// </summary>
        public string DisplayName { get; set; }
        [NotMapped]
        public string AccessToken { get; set; }
        public UserRole Role { get; set; }
        /// <summary>
        /// 是否授权登录
        /// </summary>
        public bool Approve { get; set; }

    }

    public enum UserRole
    {
        Guest,
        [Description("一般用户")]
        Commone,
        [Description("高级用户")]
        Manager,
        [Description("管理员")]
        Administrator
    }
}
