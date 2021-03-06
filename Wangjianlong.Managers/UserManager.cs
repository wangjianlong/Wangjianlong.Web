﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Common;
using Wangjianlong.Models;
using Wangjianlong.Models.Parameter;

namespace Wangjianlong.Managers
{
    public class UserManager:ManagerBase
    {

        /// <summary>
        /// 作用：保存用户  用户密码无需加密
        /// 作者：汪建龙
        /// 编写时间：2017年3月21日21:05:36
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Save(User user)
        {
            user.Password = user.Password.MD5();
            Db.Users.Add(user);
            Db.SaveChanges();
            return user.ID;
        }

        /// <summary>
        /// 作用：编辑更新用户信息
        /// 作者：汪建龙
        /// 编写时间：2017年3月21日21:07:00
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Edit(User user)
        {
            var model = Db.Users.Find(user.ID);
            if (model == null)
            {
                return false;
            }
            Db.Entry(model).CurrentValues.SetValues(user);
            Db.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：用户登陆，密码为明文
        /// 作者：汪建龙
        /// 编写时间：2017年3月21日21:08:39
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string name,string password)
        {
            password = password.MD5();
            var model = Db.Users.FirstOrDefault(e => e.UserName.ToLower() == name.ToLower() && e.Password == password);
            return model;
        }

        /// <summary>
        /// 作用：获取所有用户列表
        /// 作者：汪建龙
        /// 编写时间：2017年3月24日14:57:33
        /// </summary>
        /// <returns></returns>
        public List<User> Search(UserParameter parameter)
        {
            var query = Db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(parameter.Name))
            {
                query = query.Where(e => e.UserName.ToLower().Contains(parameter.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(parameter.DisplayName))
            {
                query = query.Where(e => e.DisplayName.ToLower().Contains(parameter.DisplayName.ToLower()));
            }
            query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
            return query.ToList();
        }

        /// <summary>
        /// 作用：验证用户密码是否正确
        /// 作者：汪建龙
        /// 编写时间：2017年3月24日15:14:25
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Validate(int id,string password)
        {
            var model = Db.Users.Find(id);
            if (model == null)
            {
                return false;
            }
            return model.Password == password.MD5();
        }

        public User Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var model = Db.Users.Find(id);
            return model;
        }

        public bool Approve(int id,bool approve)
        {
            var model = Db.Users.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Approve = approve;
            Db.SaveChanges();
            return true;
        }

    }
}
