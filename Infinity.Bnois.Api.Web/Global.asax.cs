
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Web.Models;
using Infinity.Bnois.ApplicationService;
using Infinity.Bnois.Configuration.ServiceModel;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Infinity.Bnois.Api.Web
{/// <summary>
/// 
/// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {/// <summary>
    /// 
    /// </summary>
        protected void Application_Start()
        {
          //  AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            ModelMapper.SetUp();
        }
    }
}
