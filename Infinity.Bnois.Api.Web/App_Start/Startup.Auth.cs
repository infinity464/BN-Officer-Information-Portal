using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Infinity.Bnois.Api.Web.Providers;
using Infinity.Bnois.Api.Web.Models;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup'
    public partial class Startup
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.OAuthOptions'
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.OAuthOptions'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.PublicClientId'
        public static string PublicClientId { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.PublicClientId'

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.ConfigureAuth(IAppBuilder)'
        public void ConfigureAuth(IAppBuilder app)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.ConfigureAuth(IAppBuilder)'
        {
            //app.UseCors(CorsOp)

            //var policy = new CorsPolicy()
            //{
            //    AllowAnyHeader = true,
            //    AllowAnyMethod = true,
            //    AllowAnyOrigin = true,
            //    SupportsCredentials = true
            //};
            //policy.ExposedHeaders.Add("Content-Disposition");

            //app.UseCors(new Microsoft.Owin.Cors.CorsOptions() { PolicyProvider = new CorsPolicyProvider() { PolicyResolver = p => Task.FromResult(policy) } });
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //// Configure the db context and user manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
