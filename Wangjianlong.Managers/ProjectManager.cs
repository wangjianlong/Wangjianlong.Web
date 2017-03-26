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
    public class ProjectManager:ManagerBase
    {
        public int Save(Project project)
        {
            Db.Projects.Add(project);
            Db.SaveChanges();
            return project.ID;
        }

        public bool Edit(Project project)
        {
            var model = Db.Projects.Find(project.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(project);
            Db.SaveChanges();
            return true;
        }
        /// <summary>
        /// 作用：验证是否存在
        /// 作者：汪建龙
        /// 编写时间：2017年3月22日18:03:29
        /// </summary>
        /// <param name="title"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Exist(string title,string name,int price,string unit)
        {
            return Db.Projects.Any(e => e.Title.ToLower() == title.ToLower() && e.Name.ToLower() == name.ToLower() && e.Price == price && e.Unit.ToLower() == unit.ToLower());
        }

        /// <summary>
        /// 作用：查询
        /// 作者：汪建龙
        /// 编写时间：2017年3月21日21:56:25
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Project> Search(ProjectParameter parameter)
        {
            var query = Db.Projects.AsQueryable();
            if (!string.IsNullOrEmpty(parameter.Title))
            {
                query = query.Where(e => e.Title.ToLower().Contains(parameter.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(parameter.Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(parameter.Name.ToLower()));
            }
            if (parameter.CityID.HasValue)
            {
                query = query.Where(e => e.CityID == parameter.CityID.Value);
            }

            if (parameter.MinPrice.HasValue)
            {
                query.Where(e => e.Price >= parameter.MinPrice.Value);
            }

            if (parameter.MaxPrice.HasValue)
            {
                query = query.Where(e => e.Price <= parameter.MaxPrice.Value);
            }
            query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
            return query.ToList();
        }

        public long Count(ProjectParameter parameter)
        {
            return Search(parameter).LongCount();
        }
        public List<Project> Search(string key,int cityID)
        {
            var query = Db.Projects.Where(e=>e.CityID==cityID).Where(e => e.Name.ToLower().Contains(key.ToLower()) || e.Title.ToLower().Contains(key.ToLower())).OrderBy(e=>e.ID).SetPage(new PageParameter(1, 10)).ToList();
            return query;
        }
        
        public void AddRange(List<Project> list)
        {
            var inputs = new List<Project>();
            var dailys = new List<Daily>();
            foreach(var item in list)
            {
                if (Exist(item.Title, item.Name, item.Price, item.Unit))
                {
                    dailys.Add(new Daily
                    {
                        Name="文件导入装修项目",
                        Description=string.Format("系统中已经存在缩写：{0}；名称：{1}；价格：{2}；单位：{3}",item.Title,item.Name,item.Price,item.Unit)
                    });
                }
                else
                {
                    inputs.Add(item);
                }
            }

            if (inputs.Count > 0)
            {
                Db.Projects.AddRange(inputs);
                Db.SaveChanges();
            }
            if (dailys.Count > 0)
            {
                Core.DailyManager.AddRange(dailys);
            }
        }
        
    }
}
