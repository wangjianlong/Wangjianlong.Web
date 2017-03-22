using System;
using System.Collections.Generic;
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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        [NotMapped]
        public string AccessToken { get; set; }
        public UserRole Role { get; set; }

    }

    public enum UserRole
    {
        Guest,
        Commone,
        Manager,
        Administrator
    }
}
