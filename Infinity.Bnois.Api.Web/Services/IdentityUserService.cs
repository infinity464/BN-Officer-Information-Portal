using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Models;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;
using Infinity.Bnois.ApplicationService.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService'
    public class IdentityUserService : AspNetIdentityUserService<User, string>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService'
    {
        InfinityIdentityDbContext db = new InfinityIdentityDbContext();
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.IdentityUserService(UserManager)'
        public IdentityUserService(UserManager userManager) : base(userManager)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.IdentityUserService(UserManager)'
        {

        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.GetClaimsFromAccount(User)'
        protected override async Task<IEnumerable<Claim>> GetClaimsFromAccount(User user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.GetClaimsFromAccount(User)'
        {
            var claims = (await base.GetClaimsFromAccount(user)).ToList();
            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                claims.Add(new Claim("given_name", user.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                claims.Add(new Claim("family_name", user.LastName));
            }
            if (!string.IsNullOrWhiteSpace(user.CultureCode))
            {
                claims.Add(new Claim("culture_code", user.CultureCode));
            }
            if (!string.IsNullOrWhiteSpace(user.Id))
            {
                claims.Add(new Claim("user_id", user.Id));
            }
          
            if (user.Roles.Any())
            {
                List<Claim> roleIdClaims = user.Roles.Select(x => new Claim("role_id", x.RoleId)).ToList();
                claims.AddRange(roleIdClaims);
            }

            return claims;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.AuthenticateLocalAsync(LocalAuthenticationContext)'
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext ctx)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.AuthenticateLocalAsync(LocalAuthenticationContext)'
        {
            var username = ctx.UserName;
            var PNo = ctx.UserName;
            var password = ctx.Password;
            var message = ctx.SignInMessage;

            ctx.AuthenticateResult = null;

            if (userManager.SupportsUserPassword)
            {
                bool doesEmployeeActive = true;
                User user = new User();
                user = await FindUserAsync(username);
                if (user == null)
                {
                    user = db.Users.Where(x => x.PNo == PNo).FirstOrDefault();
                    if (PNo != null)
                    {
                        int employeeId = db.Database.SqlQuery<int>("Select EmployeeId from Employe where PNo='" + PNo + "'and EmployeeStatusId=1").SingleOrDefault();
                        doesEmployeeActive = employeeId > 0 ? true : false;
                    }
                    else
                    {
                        doesEmployeeActive = false;
                    }
                }
                
                if (user != null && user.IsActive && doesEmployeeActive == true )
                {
                    if (userManager.SupportsUserLockout &&
                        await userManager.IsLockedOutAsync(user.Id))
                    {
                        return;
                    }

                    if (await userManager.CheckPasswordAsync(user, password))
                    {
                        if (userManager.SupportsUserLockout)
                        {
                            await userManager.ResetAccessFailedCountAsync(user.Id);
                        }

                        var result = await PostAuthenticateLocalAsync(user, message);
                        if (result == null)
                        {
                            var claims = await GetClaimsForAuthenticateResult(user);
                            result = new AuthenticateResult(user.Id.ToString(), await GetDisplayNameForAccountAsync(user.Id), claims);
                        }

                        ctx.AuthenticateResult = result;
                    }
                    else if (userManager.SupportsUserLockout)
                    {
                        await userManager.AccessFailedAsync(user.Id);
                    }
                }
            }
        }



#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.PostAuthenticateLocalAsync(User, SignInMessage)'
        protected override async Task<AuthenticateResult> PostAuthenticateLocalAsync(User user, SignInMessage message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityUserService.PostAuthenticateLocalAsync(User, SignInMessage)'
        {
            if (base.userManager.SupportsUserTwoFactor)
            {
                var id = user.Id;

                if (await userManager.GetTwoFactorEnabledAsync(id))
                {
                    var code = await this.userManager.GenerateTwoFactorTokenAsync(id, "sms");
                    var result = await userManager.NotifyTwoFactorTokenAsync(id, "sms", code);
                    if (!result.Succeeded)
                    {
                        return new IdentityServer3.Core.Models.AuthenticateResult(result.Errors.First());
                    }

                    var name = await GetDisplayNameForAccountAsync(id);
                    return new IdentityServer3.Core.Models.AuthenticateResult("~/2fa", id, name);
                }
            }

            return null;
        }





    }
}