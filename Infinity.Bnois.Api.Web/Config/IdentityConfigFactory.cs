using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Data;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.ExceptionHelper;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Infinity.Bnois.Api.Web.Config
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfigFactory'
    public class IdentityConfigFactory : IConfigurationFactory
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfigFactory'
    {
       
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfigFactory.Get()'
        public CompanyConfiguration Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfigFactory.Get()'
        {
            CompanyConfiguration config = null;
            if (HttpContext.Current != null)
            {
                config = HttpContext.Current.Items["Configuration"] as CompanyConfiguration;
                if (config == null)
                {
                    config = new CompanyConfiguration();
                }
                ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                  
                    Claim userIdClaim = identity.Claims.FirstOrDefault(x => x.Type == "sub");
                    string userId = userIdClaim != null ? userIdClaim.Value : null;

                    Claim[] roleClaims = identity.Claims.Where(x => x.Type == "role").ToArray();
                    Claim[] roleIdClaims = identity.Claims.Where(x => x.Type == "role_id").ToArray();

                    string[] userRoles = null;
                    if (!string.IsNullOrWhiteSpace(userId) && roleClaims != null && roleClaims.Any())
                    {
                        userRoles = roleClaims.Select(x => x.Value.Trim()).ToArray();
                    }
                    string[] roleIds = null;
                    if (!string.IsNullOrWhiteSpace(userId) && roleIdClaims != null && roleIdClaims.Any())
                    {
                        roleIds = roleIdClaims.Select(x => x.Value.Trim()).ToArray();
                    }
                    int[] userFeatureCodes = null;
                    if (!string.IsNullOrWhiteSpace(userId) && roleIdClaims != null && roleIdClaims.Any())
                    {
          
                        userFeatureCodes = new RoleFeatureService().GetUserFeature(roleIds);
                    }
                    config.LoggedInUser = new LoggedInUser(userId, roleIds, userRoles,userFeatureCodes);
                }
            }
            else
            {
                config = System.Threading.Thread.GetData(System.Threading.Thread.GetNamedDataSlot("Configuration")) as CompanyConfiguration;
            }

            if (config == null)
            {
                throw new InfinityInternalServerException("Unabled to retrieve configuration for current operation");
            }

            return config;
        }
    }
}