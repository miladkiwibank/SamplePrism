using System.Collections.Generic;
using SamplePrism.Domain.Models.Users;

namespace SamplePrism.Persistance
{
    public interface IUserDao
    {
        bool GetIsUserExists(string pinCode);
        User GetUserByPinCode(string pinCode);
        IEnumerable<UserRole> GetUserRoles();
    }
}
