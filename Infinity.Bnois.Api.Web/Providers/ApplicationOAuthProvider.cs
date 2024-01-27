using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Infinity.Bnois.Api.Web.Models;

namespace Infinity.Bnois.Api.Web.Providers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider'
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider'
    {
        private readonly string _publicClientId;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ApplicationOAuthProvider(string)'
        public ApplicationOAuthProvider(string publicClientId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ApplicationOAuthProvider(string)'
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext)'
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext)'
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.TokenEndpoint(OAuthTokenEndpointContext)'
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.TokenEndpoint(OAuthTokenEndpointContext)'
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ValidateClientAuthentication(OAuthValidateClientAuthenticationContext)'
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ValidateClientAuthentication(OAuthValidateClientAuthenticationContext)'
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext)'
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext)'
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.CreateProperties(string)'
        public static AuthenticationProperties CreateProperties(string userName)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApplicationOAuthProvider.CreateProperties(string)'
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}