using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix'
    public class IdentityRoutePrefix
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix'
    {
        private const string IdentityRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "Configuration/";
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.ActionList'
        public const string ActionList = IdentityRoutePrefixBase + "action-lists";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.ActionList'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Module'
        public const string Module = IdentityRoutePrefixBase + "modules";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Module'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Feature'
        public const string Feature = IdentityRoutePrefixBase + "features";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Feature'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Roles'
        public const string Roles = IdentityRoutePrefixBase + "roles";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Roles'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Users'
        public const string Users = IdentityRoutePrefixBase + "users";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Users'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Accounts'
        public const string Accounts = IdentityRoutePrefixBase + "accounts";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityRoutePrefix.Accounts'

    
    }
}