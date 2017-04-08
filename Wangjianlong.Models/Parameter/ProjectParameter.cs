using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int? CityID { get; set; }
        public ProjectOrder Order { get; set; }
    }

    public enum ProjectOrder
    {
        [Description("缩写")]
        Title,
        [Description("项目名称")]
        Name,
        [Description("单价")]
        Price,
        [Description("城市")]
        City
    }
}
