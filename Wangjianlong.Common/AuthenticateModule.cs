using System;
using System.Web;

namespace Wangjianlong.Common
{
    public class AuthenticateModule:IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler((obj, ea) =>
            {
                var currentUser = new HttpContextWrapper(context.Context).GetCurrentUser();
                context.Context.User = new UserPrincipal(currentUser);
            });
        }
    }
}
