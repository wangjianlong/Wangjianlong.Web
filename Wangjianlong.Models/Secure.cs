using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models
{
    [Table("secure")]
    public class Secure:SecureBase
    {
        public Secure()
        {
            LoginTime = DateTime.Now;
        }
        public Secure(SecureBase child):this()
        {
            Address = child.Address;
            HostName = child.HostName;
            Browser = child.Browser;
            Version = child.Version;
            Platform = child.Platform;
            Type = child.Type;
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
    
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
    public class SecureBase
    {
        public string Address { get; set; }
        public string HostName { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string Type { get; set; }

        public bool IsEqual(SecureBase obj)
        {
            if (obj == null)
                return this == null;
            return Address == obj.Address
                    && HostName == obj.HostName
                    && Browser == obj.Browser
                    && Version == obj.Version
                    && Platform == obj.Platform
                    && Type == obj.Type;
        }

    }
}
