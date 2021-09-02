using SamplePrism.Presentation.Services;
using SamplePrism.Presentation.Services.Common;
using Stateless;
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
        private readonly StateMachine<AppScreens, AppScreens> m_screenState;

        public ApplicationState()
        {
            m_screenState = new StateMachine<AppScreens, AppScreens>(() => ActiveScreen, state => ActiveScreen = state);
            m_screenState.OnUnhandledTrigger(HandlerTrigger);
        }

        private void HandlerTrigger(AppScreens arg1, AppScreens arg2)
        {
            ActiveScreen = arg2;
            if (arg1 != arg2)
                new AppScreenChangeData(arg1, arg2).PublishEvent(EventTopicNames.ScreenChanged);
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

        public AppScreens ActiveScreen { get; private set; }

        //public User CurrentSignedInUser { get; private set; }

        //public int MaxChannelCount { get; }

        public IEnumerable<string> Roles => m_roles;

        public IEnumerable<string> PermissionCodes => m_permissionCodes;

        public Dispatcher MainDispatcher { get; set; }

        //public void SetCurrentSignedUser(User user)
        //{

        //}

        public void SetCurrentApplicationScreen(AppScreens appScreen)
        {
            //InteractionService.ClearMouseClickQueue();
            m_screenState.Fire(appScreen);
        }
    }
}
