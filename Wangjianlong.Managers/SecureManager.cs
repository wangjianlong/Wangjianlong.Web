using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Common;
using Wangjianlong.Models;
using Wangjianlong.Models.Parameter;

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


        public List<Secure> Search(SecureParameter parameter)
        {
            var query = Db.Secures.AsQueryable();

            if (parameter.UserID.HasValue)
            {
                query = query.Where(e => e.UserID == parameter.UserID.Value);
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.LoginTime >= parameter.StartTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.LoginTime <= parameter.EndTime.Value);
            }
            query = query.OrderByDescending(e => e.LoginTime).SetPage(parameter.Page);
            return query.ToList();
        }

        public long Count(SecureParameter parameter)
        {
            return Search(parameter).LongCount();
        }
        public void Logout(int id)
        {
            var model = Db.Secures.Find(id);
            if (model != null)
            {
                model.LogoutTime = DateTime.Now;
                Db.SaveChanges();
            }
        }
        
    }
}
