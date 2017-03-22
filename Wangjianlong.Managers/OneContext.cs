using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Wangjianlong.Managers
{
    public static class OneContext
    {
        private static string _contextName { get; set; }
        public static DataBaseDbContext Current
        {
            get { return HttpContext.Current.Items[_contextName] as DataBaseDbContext; }
        }

        static OneContext()
        {
            _contextName = "_entityContext";
        }

        public static void Begin()
        {
            HttpContext.Current.Items[_contextName] = new DataBaseDbContext();
        }

        public static void End()
        {
            var entity = HttpContext.Current.Items[_contextName] as DataBaseDbContext;
            if (entity != null)
            {
                entity.Dispose();
            }
        }
    }
}
