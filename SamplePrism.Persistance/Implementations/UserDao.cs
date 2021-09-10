﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using SamplePrism.Domain.Models.Tickets;
using SamplePrism.Domain.Models.Users;
using SamplePrism.Infrastructure.Data;
using SamplePrism.Infrastructure.Data.Validation;
using SamplePrism.Localization.Properties;
using SamplePrism.Persistance.Data;

namespace SamplePrism.Persistance.Implementations
{
    public class UserDao : IUserDao
    {
        public UserDao()
        {
            ValidatorRegistry.RegisterDeleteValidator(new UserDeleteValidator());
            ValidatorRegistry.RegisterDeleteValidator(new UserRoleDeleteValidator());
            ValidatorRegistry.RegisterSaveValidator(new UserSaveValidator());
        }

        public bool GetIsUserExists(string pinCode)
        {
            return Dao.Exists<User>(x => x.PinCode == pinCode);
        }

        public User GetUserByPinCode(string pinCode)
        {
            return Dao.Single<User>(x => x.PinCode == pinCode, x => x.UserRole.Permissions);
        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return Dao.Query<UserRole>();
        }
    }

    public class UserSaveValidator : SpecificationValidator<User>
    {
        public override string GetErrorMessage(User model)
        {
            if (Dao.Exists<User>(x => x.PinCode == model.PinCode && x.Id != model.Id))
                return Resources.SaveErrorThisPinCodeInUse;
            return "";
        }
    }

    public class UserDeleteValidator : SpecificationValidator<User>
    {
        public override string GetErrorMessage(User model)
        {
            if (model.UserRole.IsAdmin) return Resources.DeleteErrorAdminUser;
            if (Dao.Count<User>() == 1) return Resources.DeleteErrorLastUser;
            if (Dao.Exists<Order>(x => x.CreatingUserName == model.Name))
                return string.Format(Resources.DeleteErrorUsedBy_f, Resources.User, Resources.Order);
            return "";
        }
    }

    public class UserRoleDeleteValidator : SpecificationValidator<UserRole>
    {
        public override string GetErrorMessage(UserRole model)
        {
            if (model.Id == 1 || model.IsAdmin) return string.Format(Resources.CantDelete_f, Resources.AdminRole);
            if (Dao.Exists<User>(y => y.UserRole.Id == model.Id))
                return string.Format(Resources.DeleteErrorUsedBy_f, Resources.UserRole, Resources.User);
            return "";
        }
    }
}
