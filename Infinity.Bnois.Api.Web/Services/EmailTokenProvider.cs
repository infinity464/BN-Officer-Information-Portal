using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EmailTokenProvider'
    public class EmailTokenProvider : EmailTokenProvider<User, string>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EmailTokenProvider'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'EmailTokenProvider.ValidateAsync(string, string, UserManager<User, string>, User)'
        public override Task<bool> ValidateAsync(string purpose, string token, UserManager<User, string> manager, User user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'EmailTokenProvider.ValidateAsync(string, string, UserManager<User, string>, User)'
        {
            return base.ValidateAsync(purpose, token, manager, user);
        }
    }
}