using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Infinity.Bnois.Api.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUser'
    public class ApplicationUser : IdentityUser
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUser'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUser.GenerateUserIdentityAsync(UserManager<ApplicationUser>, string)'
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUser.GenerateUserIdentityAsync(UserManager<ApplicationUser>, string)'
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext'
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext.ApplicationDbContext()'
        public ApplicationDbContext()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext.ApplicationDbContext()'
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext.Create()'
        public static ApplicationDbContext Create()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationDbContext.Create()'
        {
            return new ApplicationDbContext();
        }
    }
}