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

        public bool Exist(string name,string number)
        {
            return Db.Fitments.Any(e => e.Name.ToLower() == name.ToLower()&&e.Number.ToLower()==number.ToLower());
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
            if (parameter.UserID.HasValue)
            {
                query = query.Where(e => e.UserID == parameter.UserID.Value);
            }
            if (parameter.StartTime.HasValue)
            {
                query = query.Where(e => e.CreateTime >= parameter.StartTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.CreateTime <= parameter.EndTime.Value);
            }
            if (!string.IsNullOrEmpty(parameter.Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(parameter.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(parameter.Number))
            {
                query = query.Where(e => e.Number.ToLower().Contains(parameter.Number.ToLower()));
            }
            if (!string.IsNullOrEmpty(parameter.Address))
            {
                query = query.Where(e => e.Address.ToLower().Contains(parameter.Address.ToLower()));
            }
            query = query.OrderByDescending(e => e.CreateTime).SetPage(parameter.Page);
            return query.ToList();
        }

        public long Count(FitmentParameter parameter)
        {
            return Search(parameter).LongCount();
        }

        /// <summary>
        /// 作用：验证是否含有位置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Used(int id)
        {
            return Db.Positions.Any(e => e.FitmentID == id);
        }

        public bool Delete(int id)
        {
            var model = Db.Fitments.Find(id);
            if (model == null)
            {
                return false;
            }
            Db.Fitments.Remove(model);
            Db.SaveChanges();
            return true;
        }
    }
}
