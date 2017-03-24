using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wangjianlong.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Wangjianlong.Common
{
    public static class ApplicationHelper
    {
        private static string _sessionName = "USER_LOGIN_LIST";
        private static Hashtable GetHashtable(HttpApplicationState application)
        {
            Hashtable SingleOnline = application[_sessionName] as Hashtable;
            if (SingleOnline == null)
            {
                SingleOnline = new Hashtable();
            }
            return SingleOnline;
        }

        public static void Login(this HttpApplicationState application, User user,SecureBase secure)
        {
            var SingleOnline = GetHashtable(application);
            if (SingleOnline.ContainsKey(user.ID))
            {
                SingleOnline[user.ID] = secure;
            }
            else
            {
                SingleOnline.Add(user.ID, secure);
            }
            application.Lock();
            application[_sessionName] = SingleOnline;
            application.UnLock();
        } 

        public static void LogOut(this HttpApplicationState application,int UserID)
        {
            var singleOnline = GetHashtable(application);
            if (singleOnline.ContainsKey(UserID))
            {
                singleOnline.Remove(UserID);
                application.Lock();
                application[_sessionName] = singleOnline;
                application.UnLock();
            }
        }

        public static bool IsCurrentDevice(this HttpApplicationState application,int userId, SecureBase secure)
        {
            var singleOnline = GetHashtable(application);
            if (singleOnline.ContainsKey(userId))
            {
                var currentSecure = singleOnline[userId] as SecureBase;
                return secure.IsEqual(currentSecure);
            }
            return false;
        }

    }
}
