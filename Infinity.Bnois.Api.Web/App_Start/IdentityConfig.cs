using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Infinity.Bnois.Api.Web.Models;

namespace Infinity.Bnois.Api.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager'
    public class ApplicationUserManager : UserManager<ApplicationUser>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager.ApplicationUserManager(IUserStore<ApplicationUser>)'
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager.ApplicationUserManager(IUserStore<ApplicationUser>)'
            : base(store)
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager.Create(IdentityFactoryOptions<ApplicationUserManager>, IOwinContext)'
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationUserManager.Create(IdentityFactoryOptions<ApplicationUserManager>, IOwinContext)'
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
           
                
            };
          
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
         
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
