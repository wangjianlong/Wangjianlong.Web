using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>         
        /// 通过IP得到IP所在地省市（Porschev）         
        /// </summary>         
        /// <param name="ip"></param>         
        /// <returns></returns>         
        public static string GetAdrByIp(string ip)
        {
            string url = "http://www.cz88.net/ip/?ip=" + ip;
            string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";
            //得到网页源码
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html);
            html = ma.Value;
            string[] arr = html.Split(' ');
            return arr[0];
        }


        /// <summary>         
        /// 获取HTML源码信息(Porschev)         
        /// </summary>         
        /// <param name="url">获取地址</param>         
        /// <returns>HTML源码</returns>         
        public static string GetHtml(string url)
        {
            string str = "";
            try
            {
                Uri uri = new Uri(url);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e)
            {
            }
            return str;
        }
    }
}
