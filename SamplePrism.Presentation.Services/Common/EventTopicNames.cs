using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Services.Common
{
    public static class EventTopicNames
    {
        public const string AddedModelSaved = "Model Saved";
        public const string ActivateNavigation = "Activate Navigation";
        public const string DashboardCommandAdded = "Dashboard Command Added";
        public const string ModelAddedOrDeleted = "Model Added or Deleted";
        public const string NavigationCommandAdded = "Navigation Command Added";
        public const string RegionActivated = "Region Activated";
        public const string ScreenChanged = "Screen Changed";
        public const string ShellInitlized = "Shell Initialized";
        public const string ViewAdded = "View Added";
        public const string ViewClosed = "View Closed";
        public const string UserSignedOut = "User SginedOut";
        public const string UserSignedIn = "User SignedIn";
    }
}
