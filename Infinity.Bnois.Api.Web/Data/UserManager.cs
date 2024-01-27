
using Infinity.Bnois.Api.Web.Services;
using Infinity.Ers.IdentityServer.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserManager'
    public class UserManager : UserManager<User, string>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserManager'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserManager.UserManager(UserStore)'
        public UserManager(UserStore userRepository)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserManager.UserManager(UserStore)'
            : base(userRepository)
        {

            this.PasswordValidator = new Providers.CustomPasswordValidator(6);

            this.RegisterTwoFactorProvider("sms", new Sms()
            {
                MessageFormat = "Your One Time Password (OTP) for BNOIP Login is: {0}"
            });
            this.RegisterTwoFactorProvider("email", new EmailTokenProvider()
            {
                Subject = "SecurityCode",
                BodyFormat = "Your One Time Password (OTP) for BNOIP Login is {0}"
            });
            this.SmsService = new SmsService();
            this.EmailService = new EmailService();
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Infinity.Ers");
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>());
            this.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<User>(provider.Create("EmailConfirmation"));

        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserManager.SmsResetTokenProvider'
        public TotpSecurityStampBasedTokenProvider<User, string> SmsResetTokenProvider = new TotpSecurityStampBasedTokenProvider<User, string>();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserManager.SmsResetTokenProvider'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'UserManager.UpdateTokenPassword(IUserPasswordStore<User, string>, User, string)'
        public async Task<IdentityResult> UpdateTokenPassword(IUserPasswordStore<User, string> passwordStore, User user, string newPassword)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'UserManager.UpdateTokenPassword(IUserPasswordStore<User, string>, User, string)'
        {
            return await base.UpdatePassword(passwordStore, user, newPassword);
        }
    }
}