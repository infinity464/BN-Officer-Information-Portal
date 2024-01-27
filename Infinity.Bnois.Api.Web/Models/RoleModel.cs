using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleModel'
    public class RoleModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.Id'
        public string Id { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.Id'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.Name'
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.Name'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.CompanyId'
        public string CompanyId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.CompanyId'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.TotalUser'
        public int TotalUser { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleModel.TotalUser'
    }
}