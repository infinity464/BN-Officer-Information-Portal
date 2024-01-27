
using Infinity.Bnois.Api.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserStore'
    public class UserStore : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserStore'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserStore.UserStore(InfinityIdentityDbContext)'
        public UserStore(InfinityIdentityDbContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserStore.UserStore(InfinityIdentityDbContext)'
            : base(context)
        {
        }
    }
}