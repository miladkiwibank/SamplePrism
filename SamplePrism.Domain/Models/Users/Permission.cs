﻿using SamplePrism.Infrastructure.Data;

namespace SamplePrism.Domain.Models.Users
{
    public enum PermissionValue
    {
        Enabled,
        Disabled,
        Invisible
    }

    public class Permission : EntityClass
    {
        public int Value { get; set; }
        public int UserRoleId { get; set; }
    }
}
