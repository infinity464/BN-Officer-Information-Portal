using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Infinity.Bnois.Api.Core
{
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        public int Feature { get; set; }
        public ActionAuthorizeAttribute(int Feature = 0)
        {
            this.Feature = Feature;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
           ILoggedInUser loggedInUser = ConfigurationResolver.Get().LoggedInUser;
            if(string.IsNullOrWhiteSpace(loggedInUser.UserId))
            {
               return false;
            }

            if(Feature>0)
            {
              return loggedInUser.UserFeatureCodes.Contains(Feature);
            }
            else
            {
                return false;
            }
        }
    }
}
