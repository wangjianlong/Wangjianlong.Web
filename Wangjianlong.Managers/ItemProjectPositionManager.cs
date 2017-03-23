using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Models;

namespace Wangjianlong.Managers
{
    public class ItemProjectPositionManager:ManagerBase
    {
        public List<ItemProjectPosition> Search()
        {
            var query = Db.ItemProjectPositions.AsQueryable();

            return query.ToList();
        }

        public List<ItemProjectPosition> GetPositionID(int positionID)
        {
            return Db.ItemProjectPositions.Where(e => e.PositionID == positionID).ToList();
        }

        public List<ItemProjectPosition> GetFitmentID(int fitmentID)
        {
            return Db.ItemProjectPositions.Where(e => e.FitmentID == fitmentID).ToList();
        }
    }
}
