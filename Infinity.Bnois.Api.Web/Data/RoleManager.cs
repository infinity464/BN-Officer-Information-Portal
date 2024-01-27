

using Infinity.Bnois.Api.Web.Models;

namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleManager'
    public class RoleManager : Microsoft.AspNet.Identity.RoleManager<Role>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleManager'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleManager.RoleManager(RoleStore)'
        public RoleManager(RoleStore roleRepository)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleManager.RoleManager(RoleStore)'
            : base(roleRepository)
        {
        }
    }
}