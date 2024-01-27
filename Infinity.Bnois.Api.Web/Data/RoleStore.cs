
using Infinity.Bnois.Api.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleStore'
    public class RoleStore : RoleStore<Role>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleStore'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleStore.RoleStore(InfinityIdentityDbContext)'
        public RoleStore(InfinityIdentityDbContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleStore.RoleStore(InfinityIdentityDbContext)'
            : base(context)
        {
        }
    }
}