using System.Web;
using System.Web.Mvc;

namespace Wangjianlong.Common
{
    public class UserAuthorizeAttribute:AuthorizeAttribute
    {
        private bool _enable { get; set; }
        public UserAuthorizeAttribute(bool enable = true)
        {
            _enable = enable;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_enable)
            {
                var request = filterContext.HttpContext.Request;
                var retureUrl = request.Url.AbsoluteUri;
                filterContext.HttpContext.Response.Redirect("/user/login?returnUrl=" + HttpUtility.UrlEncode(retureUrl));
            }
        }

        

    }
}
