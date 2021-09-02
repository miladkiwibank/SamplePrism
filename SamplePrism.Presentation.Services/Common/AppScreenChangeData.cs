using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Services.Common
{
    public class AppScreenChangeData
    {
        public AppScreenChangeData(AppScreens pre, AppScreens next)
        {
            PreviousScreen = pre;
            CurrentScreen = next;
        }

        public AppScreens PreviousScreen { get; set; }
        public AppScreens CurrentScreen { get; set; }
    }
}
