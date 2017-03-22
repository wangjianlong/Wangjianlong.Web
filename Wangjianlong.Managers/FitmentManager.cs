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
    public class FitmentManager:ManagerBase
    {
        /// <summary>
        /// 作用：
        /// </summary>
        /// <param name="fitment"></param>
        /// <returns></returns>
        public int Save(Fitment fitment)
        {
            Db.Fitments.Add(fitment);
            Db.SaveChanges();
            return fitment.ID;
        }

        public bool Edit(Fitment fitment)
        {
            var model = Db.Fitments.Find(fitment.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(fitment);
            Db.SaveChanges();
            return true;
        }

        public bool Exist(string name)
        {
            return Db.Fitments.Any(e => e.Name.ToLower() == name.ToLower());
        }


        public Fitment Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return Db.Fitments.Find(id);
        }

        public List<Fitment> Search(FitmentParameter parameter)
        {
            var query = Db.Fitments.AsQueryable();
            query = query.OrderByDescending(e => e.CreateTime).SetPage(parameter.Page);
            return query.ToList();
        }
    }
}
