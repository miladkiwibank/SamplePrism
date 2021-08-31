using SamplePrism.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SamplePrism.Presentation.Common.Services
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

        public void NotifyEvent(string eventName, object dataObject)
        {
            //var terminalId = CurrentTerminal.Id;
            //var departmentId = CurrentDepartment.Id;
            //var roleId = CurrentSignedInUser.UserRole.Id;
            //_notificationService.NotifyEvent(eventName, dataObject, terminalId, departmentId, roleId,
            //    x => x.PublishEvent(EventTopicNames.ExecuteEvent, true));
        }

        //public Screens ActiveScreen { get; private set; }

        //public User CurrentSignedInUser { get; private set; }

        //public int MaxChannelCount { get; }

        public IEnumerable<string> Roles => m_roles;

        public IEnumerable<string> PermissionCodes => m_permissionCodes;

        public Dispatcher MainDispatcher { get; set; }

        //public void SetCurrentSignedUser(User user)
        //{

        //}

        //public void SetCurrentApplicationScreen
        //{

        //}
    }
}
