using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Services
{
    public interface IApplicationState
    {
        //Screens ActiveScreen { get; }

        //int MaxChannelCount { get; }

        //User CurrentSignedUser { get; }

        IEnumerable<string> Roles { get; }

        IEnumerable<string> PermissionCodes { get; }
    }
}
