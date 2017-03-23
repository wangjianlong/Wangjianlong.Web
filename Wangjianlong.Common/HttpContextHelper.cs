using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Wangjianlong.Common
{
    public static class HttpContextHelper
    {
        public static int[] Get(this HttpContextBase context,string name)
        {
            var values = context.Request.Form[name].Split(',');
            int[] result = new int[values.Length];
            var a = 0;
            for(var i = 0; i < values.Length; i++)
            {
                result[i] = int.TryParse(values[i], out a) ? a : 0;
            }
            return result;
        }

        public static double[] GetDouble(this HttpContextBase context,string name)
        {
            var values = context.Request.Form[name].Split(',');
            double[] result = new double[values.Length];
            var a = .0;
            for (var i = 0; i < values.Length; i++)
            {
                result[i] = double.TryParse(values[i], out a) ? a : .0;
            }
            return result;
        }


    }
}
