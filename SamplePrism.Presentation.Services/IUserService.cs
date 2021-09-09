using System.Collections.Generic;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Presentation.Services.Common;

namespace SamplePrism.Presentation.Services
{
    public interface IUserService : IPresentationService
    {
        string GetUserName(int userId);
        IEnumerable<Department> PermittedDepartments { get; }
        bool ContainsUser(int userId);
        bool IsDefaultUserConfigured { get; }
        User LoginUser(string pinValue);
        void LogoutUser(bool resetCache = true);
        bool IsUserPermittedFor(string permissionName);
        IEnumerable<UserRole> GetUserRoles();
    }
}
