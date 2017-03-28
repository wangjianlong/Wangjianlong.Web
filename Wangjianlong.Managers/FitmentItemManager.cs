using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class FitmentItemManager:ManagerBase
    {
        public void AddRange(List<FitmentItem> list)
        {
            try
            {
                Db.FitmentItems.AddRange(list);
                Db.SaveChanges();
            }
            catch
            {

            }
        }

        public bool Edit(FitmentItem item)
        {
            var model = Db.FitmentItems.Find(item.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(item);
            Db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var model = Db.FitmentItems.Find(id);
            if (model == null)
            {
                return false;
            }
            Db.FitmentItems.Remove(model);
            Db.SaveChanges();
            return true;
        }

        public FitmentItem Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var model = Db.FitmentItems.Find(id);
            return model;
        }
        public bool SetNewOld(int id,double newold)
        {
            var model = Db.FitmentItems.Find(id);
            if (model == null)
            {
                return false;
            }
            model.NewOld = newold;
            Db.SaveChanges();
            return true;
        }

        public void SetNewOld(List<ItemProjectPosition> list,double newold)
        {
            foreach(var item in list)
            {
                SetNewOld(item.ID, newold);
            }
        }
    }
}
