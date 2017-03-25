using System;
using System.Security.Principal;
using Wangjianlong.Models;

namespace Wangjianlong.Common
{
    public class UserPrincipal:IPrincipal
    {
        public IIdentity Identity { get; set; }
        public UserPrincipal(IIdentity identity)
        {
            Identity = identity;
        }
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
    public class UserIdentity : IIdentity
    {
        public static readonly UserIdentity Guest = new UserIdentity();
        public int UserID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int SecureID { get; set; }
        public UserRole Role { get; set; }
        public string AuthenticationType { get { return "Web.Session"; } }
        public bool IsAuthenticated { get { return UserID > 0; } }
    }
}
