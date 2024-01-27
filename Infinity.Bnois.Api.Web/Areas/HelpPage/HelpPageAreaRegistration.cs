using System.Web.Http;
using System.Web.Mvc;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration'
    public class HelpPageAreaRegistration : AreaRegistration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration.AreaName'
        public override string AreaName
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration.AreaName'
        {
            get
            {
                return "HelpPage";
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration.RegisterArea(AreaRegistrationContext)'
        public override void RegisterArea(AreaRegistrationContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HelpPageAreaRegistration.RegisterArea(AreaRegistrationContext)'
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}