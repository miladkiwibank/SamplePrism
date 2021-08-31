using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Services
{
    public interface IApplicationStateSetter
    {
        //void SetCurentSignedUser(User user);

        //void SetCurrentApplicationScreen(Screens screen);

        void InitializeSettings();
    }
}
