using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Infinity.Bnois.Api.Web.IdentityServer;
using Infinity.Bnois.Api.Web.Config;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Services;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Infinity.Bnois.Api.Web.Startup))]

namespace Infinity.Bnois.Api.Web
{
    public partial class Startup
    {
        private EntityFrameworkServiceOptions efConfig = new EntityFrameworkServiceOptions
        {
            ConnectionString = "InfinityIdentityEntities",
        };

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configuration(IAppBuilder)'
        public void Configuration(IAppBuilder app)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configuration(IAppBuilder)'
        {
            DefaultCorsPolicyService corsPolicyService = new DefaultCorsPolicyService()
            {
                AllowAll = true
            };

            // these two calls just pre-populate the test DB from the in-memory config
            InitialValues.ConfigureInitValues(efConfig);

            DefaultViewServiceOptions defaultViewServiceOptions = new DefaultViewServiceOptions();
            defaultViewServiceOptions.CacheViews = false;

            IdentityServerServiceFactory idsServiceFactory = new IdentityServerServiceFactory();

            if (true)
            {
                idsServiceFactory.UseInMemoryScopes(Scopes.Get());
                idsServiceFactory.UseInMemoryClients(Clients.Get());
            }
            else
            {
                //idsServiceFactory.RegisterConfigurationServices(efConfig);
            }

            idsServiceFactory.CorsPolicyService = new Registration<ICorsPolicyService>(corsPolicyService);
            idsServiceFactory.ConfigureDefaultViewService(defaultViewServiceOptions);

            idsServiceFactory.UserService = new Registration<IdentityServer3.Core.Services.IUserService, IdentityUserService>();
            idsServiceFactory.ViewService = new Registration<IViewService, CustomViewService>();
            idsServiceFactory.Register(new Registration<UserStore>());
            idsServiceFactory.Register(new Registration<UserManager>());

            idsServiceFactory.Register(new Registration<InfinityIdentityDbContext>(resolver => new InfinityIdentityDbContext()));

            IdentityServerOptions options = new IdentityServerOptions
            {
                Factory = idsServiceFactory,
                SiteName = "Bangladesh Naval Officers Information System client security Token Service",
                IssuerUri = IdentityConfig.IssuerUri,
                PublicOrigin = IdentityConfig.PublicOrigin,
                SigningCertificate = LoadCertificate(),
                EnableWelcomePage = true,
                RequireSsl = false,

                AuthenticationOptions = new AuthenticationOptions()
                {

                    EnablePostSignOutAutoRedirect = true,
                    EnableSignOutPrompt = false,
                    PostSignOutAutoRedirectDelay = 2,
                    LoginPageLinks = new LoginPageLink[] {

                            new LoginPageLink
                            {
                                Text = "Reset password by email",
                                Href = "authentication/forget-password"
                            },
                            new LoginPageLink
                            {
                                Text = "Reset password by phone",
                                Href = "authentication/reset-password-sms"
                            }
                        }

                },
                CspOptions = new CspOptions()
                {
                    Enabled = false
                }
            };

            app.Map("/core", idsrvApp =>
                {
                    idsrvApp.UseIdentityServer(options);
                });


            app.Map("", adminService =>
            {
                adminService.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "Cookies"
                });

                adminService.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = IdentityConfig.Authority,
                    RequiredScopes = new[] { "infinity-bnois-identity-api-scope" },
                    DelayLoadMetadata = true,
                    ValidationMode = ValidationMode.ValidationEndpoint
                });

                ConfigureAuth(adminService);
            });

        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                $@"{AppDomain.CurrentDomain.BaseDirectory}\certificates\idsrv3test.pfx", "idsrv3test");
        }
    }
}
