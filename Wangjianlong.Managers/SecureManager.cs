using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class SecureManager:ManagerBase
    {
        public int Save(Secure secure)
        {
            Db.Secures.Add(secure);
            Db.SaveChanges();
            return secure.ID;
        }

        
    }
}
