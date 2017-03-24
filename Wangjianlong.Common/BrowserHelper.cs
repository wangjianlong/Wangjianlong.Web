using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Wangjianlong.Common
{
    public static class BrowserHelper
    {
        public static string GetIP(this HttpRequest request)
        {
            string IP=string.Empty;
            if (!string.IsNullOrEmpty(request.ServerVariables["HTTP_VIA"]))
            {
                IP = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                IP = request.UserHostAddress;
            }
            

            return IP;
        }

        public static string GetBrowserIP(this HttpRequest request)
        {
            return request.UserHostAddress;
        }

        //public static string GetHostName(this HttpRequest request)
        //{

        //}
    }
}
