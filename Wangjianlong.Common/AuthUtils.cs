using System;
using System.Web;
using System.Web.Security;
using Wangjianlong.Models;

namespace Wangjianlong.Common
{
    public static class AuthUtils
    {
        private const string _cookieName = "Wangjianlong";
        public static string GenerateToken(this HttpContextBase context,User user)
        {
            var ticket = new FormsAuthenticationTicket(1, user.ID + "|" + user.UserName + "|" + user.DisplayName+"|"+user.Role+"|"+user.SecureID, DateTime.Now, DateTime.Now.AddHours(8), true, "user_token");
            var token = FormsAuthentication.Encrypt(ticket);
            return token;
        }
        private static string GetToken(HttpContextBase context)
        {
            var token = context.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                var cookie = context.Request.Cookies.Get(_cookieName);
                if (cookie != null)
                {
                    token = cookie.Value;
                }
            }
            return token;
        }

        public static void SaveAuth(this HttpContextBase context, User user)
        {
            user.AccessToken = GenerateToken(context, user);
            var cookie = new HttpCookie(_cookieName, user.AccessToken);
            context.Response.Cookies.Remove(_cookieName);
            context.Response.Cookies.Add(cookie);
        }

        public static void ClearAuth(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies.Get(_cookieName);
            if (cookie == null) return;
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddHours(2);
            context.Response.SetCookie(cookie);
        }
        public static UserIdentity GetCurrentUser(this HttpContextBase context)
        {
            var token = GetToken(context);
            if (!string.IsNullOrEmpty(token))
            {
                var ticket = FormsAuthentication.Decrypt(token);
                if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                {
                    var values = ticket.Name.Split('|');
                    if (values.Length == 5)
                    {
                        UserRole role;
                        Enum.TryParse<UserRole>(values[3], out role);
                        return new UserIdentity
                        {
                            UserID = int.Parse(values[0]),
                            Name = values[1],
                            DisplayName = values[2],
                            Role = role,
                            SecureID = int.Parse(values[4])
                        };
                    }
                }
            }
            return UserIdentity.Guest;
        }

    }
}
