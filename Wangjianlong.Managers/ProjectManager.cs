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
        
        
    }
}
