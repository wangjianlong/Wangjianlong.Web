using System;
using System.Threading;
using System.Web.Mvc;

namespace Wangjianlong.Common
{
    public class BaseView<TModule>:WebViewPage<TModule>
    {
        public UserIdentity Identity { get; set; }
        public BaseView()
        {
            Identity = Thread.CurrentPrincipal.Identity as UserIdentity;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
