using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wangjianlong.Managers
{
    public class ManagerCore
    {
        public static readonly ManagerCore Instance = new ManagerCore();
        private UserManager _userManager { get; set; }
        public UserManager UserManager { get { return _userManager == null ? _userManager = new UserManager() : _userManager; } }
        private ProjectManager _projectManager { get; set; }
        public ProjectManager ProjectManager { get { return _projectManager == null ? _projectManager = new ProjectManager() : _projectManager; } }
    }
}
