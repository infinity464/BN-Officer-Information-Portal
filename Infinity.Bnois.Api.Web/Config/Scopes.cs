using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace Infinity.Bnois.Api.Web.Config
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Scopes'
    public class Scopes
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Scopes'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Scopes.Get()'
        public static IEnumerable<Scope> Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Scopes.Get()'
        {
            return new Scope[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Roles,
                StandardScopes.OfflineAccess,
                StandardScopes.AllClaims,
                new Scope
                {
                    Name = "infinity-bnois-api-scope",
                    DisplayName = "BNOIS",
                    Description = "Allow the application to manage infinity.",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>()
                    {
                        new ScopeClaim("role", true),
                        new ScopeClaim(Constants.ClaimTypes.Name, true),
                        new ScopeClaim(Constants.ClaimTypes.Email, true),
                        new ScopeClaim("given_name", alwaysInclude: true),
                        new ScopeClaim("family_name", alwaysInclude: true),
                        new ScopeClaim("culture_code", alwaysInclude: true),
                        new ScopeClaim("role_id", alwaysInclude: true),
                        new ScopeClaim("user_id", alwaysInclude: true)
                    },

                    IncludeAllClaimsForUser = true
                },
          
                new Scope
                {
                    Name = "infinity-bnois-identity-api-scope",
                    DisplayName = "BNOIS",
                    Description = "Allow the application to manage Infinity.",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>()
                    {
                         new ScopeClaim("role", true),
                        new ScopeClaim(Constants.ClaimTypes.Name, true),
                        new ScopeClaim(Constants.ClaimTypes.Email, true),

                        new ScopeClaim("given_name", alwaysInclude: true),
                        new ScopeClaim("family_name", alwaysInclude: true),
                        new ScopeClaim("culture_code", alwaysInclude: true),
                        new ScopeClaim("role_id", alwaysInclude: true),
                        new ScopeClaim("user_id", alwaysInclude: true)
                    },

                    IncludeAllClaimsForUser = true
                },
             
            };
        }
    }
}