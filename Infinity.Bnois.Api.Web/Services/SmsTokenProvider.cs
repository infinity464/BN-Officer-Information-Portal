using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Sms'
    public class Sms : PhoneNumberTokenProvider<User, string>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Sms'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Sms.ValidateAsync(string, string, UserManager<User, string>, User)'
        public override Task<bool> ValidateAsync(string purpose, string token, UserManager<User, string> manager, User user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Sms.ValidateAsync(string, string, UserManager<User, string>, User)'
        {
            // just hard coding to validate any 2fa token
            //return Task.FromResult(true);
            return base.ValidateAsync(purpose, token, manager, user);
        }
    }
}