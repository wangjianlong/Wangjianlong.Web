using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class DailyManager:ManagerBase
    {
        public void AddRange(List<Daily> list)
        {
            try
            {
                Db.Dailys.AddRange(list);
                Db.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
