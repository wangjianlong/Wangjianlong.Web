using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models.Parameter
{
    public class SecureParameter:ParameterBase
    {
        public int? UserID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Address { get; set; }
        public string HostName { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
    }
}
