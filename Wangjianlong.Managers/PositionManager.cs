using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class PositionManager:ManagerBase
    {
        public int Save(Position position)
        {
            Db.Positions.Add(position);
            Db.SaveChanges();
            return position.ID;
        }

        public bool Edit(Position position)
        {
            var model = Db.Positions.Find(position.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(position);
            Db.SaveChanges();
            return true;
        }
        public Position Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return Db.Positions.Find(id);
        }

        public List<Position> GetByFitmentID(int fitmentID)
        {
            if (fitmentID <= 0)
            {
                return new List<Position>();
            }
            return Db.Positions.Where(e => e.FitmentID == fitmentID).ToList();

        }

        public bool Exist(int fitmentID,string name,Category category)
        {
            return Db.Positions.Any(e => e.FitmentID == fitmentID && e.Name.ToLower() == name.ToLower() && e.Category == category);
        }
    }
}
