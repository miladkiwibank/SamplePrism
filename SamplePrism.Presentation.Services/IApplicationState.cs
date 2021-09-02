using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SamplePrism.Presentation.Services
{
    public interface IApplicationState
    {
        AppScreens ActiveScreen { get; }

        //int MaxChannelCount { get; }

        //User CurrentSignedUser { get; }

        IEnumerable<string> Roles { get; }

        IEnumerable<string> PermissionCodes { get; }

        Dispatcher MainDispatcher { get; set; }

        void NotifyEvent(string eventName, object dataObject);
    }
}
