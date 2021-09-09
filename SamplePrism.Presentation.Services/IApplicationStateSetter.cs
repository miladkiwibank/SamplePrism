using System.Collections.Generic;
using SamplePrism.Domain.Models.Entities;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Presentation.Services.Common;

namespace SamplePrism.Presentation.Services
{
    public interface IApplicationStateSetter
    {
        void SetCurrentLoggedInUser(User user);
        void SetCurrentDepartment(int departmentId);
        void SetCurrentApplicationScreen(AppScreens appScreen);
        EntityScreen SetSelectedEntityScreen(EntityScreen entityScreen);
        void SetApplicationLocked(bool isLocked);
        void SetNumberpadValue(string value);
        void SetCurrentTicketType(TicketType ticketType);
        void SetCurrentTerminal(string terminalName);
        void ResetWorkPeriods();
    }
}