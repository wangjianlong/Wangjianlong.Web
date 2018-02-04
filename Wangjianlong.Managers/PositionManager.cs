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
        public bool Lock(int id,bool flag)
        {
            if (id <= 0)
            {
                return false;
            }
            var model = Db.Positions.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Lock = flag;
            Db.SaveChanges();
            return true;
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

        /// <summary>
        /// 作用：验证是否已使用
        /// 作者：汪建龙
        /// 编写时间：2017年3月23日15:51:49
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Used(int id)
        {
            return Db.FitmentItems.Any(e => e.PositionID == id);
        }

        public bool Delete(int id)
        {
            var model = Db.Positions.Find(id);
            if (model == null)
            {
                return false;
            }
            Db.Positions.Remove(model);
            Db.SaveChanges();
            return true;
        }
        public bool Copy(int sourceId,string name,Category category)
        {
            var position = Db.Positions.Find(sourceId);
            if (position == null)
            {
                return false;
            }

            var newPosition = new Position
            {
                Name = name,
                FitmentID = position.FitmentID,
                Category = category,
                Items = position.Items.Select(e => new FitmentItem
                {
                    ProjectID = e.ProjectID,
                    Formula = e.Formula,
                    Number = e.Number,
                    NewOld = e.NewOld,
                    Price = e.Price
                }).ToList()
            };
            Db.Positions.Add(newPosition);
            Db.SaveChanges();
            return true;
        }
    }
}
