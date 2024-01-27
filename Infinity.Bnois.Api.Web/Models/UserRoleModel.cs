using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel'
    public class UserRoleModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.RoleId'
        public string RoleId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.RoleId'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.RoleName'
        public string RoleName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.RoleName'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.IsAssigned'
        public bool IsAssigned { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserRoleModel.IsAssigned'
    }
}