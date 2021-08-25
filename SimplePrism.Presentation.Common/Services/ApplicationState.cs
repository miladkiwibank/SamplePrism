using SimplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Common.Services
{
    public class ApplicationState : IApplicationState, IApplicationStateSetter
    {
        private IList<string> m_roles = new List<string>();
        private IList<string> m_permissionCodes = new List<string>();

        public ApplicationState()
        {

        }

        public void InitializeSettings()
        {
        }

        //public Screens ActiveScreen { get; private set; }

        //public User CurrentSignedUser { get; private set; }

        //public int MaxChannelCount { get; }

        public IEnumerable<string> Roles => m_roles;

        public IEnumerable<string> PermissionCodes => m_permissionCodes;

        //public void SetCurrentSignedUser(User user)
        //{

        //}

        //public void SetCurrentApplicationScreen
        //{

        //}
    }
}
