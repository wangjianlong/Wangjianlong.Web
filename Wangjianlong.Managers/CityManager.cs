using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class CityManager:ManagerBase
    {
        public int Save(City city)
        {
            Db.Citys.Add(city);
            Db.SaveChanges();
            return city.ID;
        }

        public bool Edit(City city)
        {
            var model = Db.Citys.Find(city.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(city);
            Db.SaveChanges();
            return true;
        }

        public List<City> GetList()
        {
            return Db.Citys.ToList();
        }
        public City Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return Db.Citys.Find(id);
        }

        public bool Exist(string name)
        {
            return Db.Citys.Any(e => e.Name.ToLower() == name.ToLower());
        }

        public bool Used(int id)
        {
            return Db.Projects.Any(e => e.CityID == id);
        }

        public bool Delete(int id)
        {
            var model = Db.Citys.Find(id);
            if (model == null)
            {
                return false;
            }
            Db.Citys.Remove(model);
            Db.SaveChanges();
            return true;
        }
    }
}
