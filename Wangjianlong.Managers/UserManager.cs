using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wangjianlong.Common;
using Wangjianlong.Models;

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
    }
}
