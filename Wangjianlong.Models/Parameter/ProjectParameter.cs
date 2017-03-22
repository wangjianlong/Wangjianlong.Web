using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Models.Parameter
{
    public class ProjectParameter:ParameterBase
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
