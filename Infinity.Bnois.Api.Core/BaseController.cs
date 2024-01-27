using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Core
{
    [Authorize]
    [ValidationFilter]
    public class BaseController : ApiController
    {
        public string UserName
        {

            get
            {
                ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    Claim userNameClaim = identity.Claims.FirstOrDefault(x => x.Type == "preferred_username");
                    if (userNameClaim != null)
                    {
                        return userNameClaim.Value.Trim();
                    }
                }

                return string.Empty;
            }
        }

        public string UserId
        {
            get
            {
                ClaimsIdentity identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    Claim userIdClaim = identity.Claims.FirstOrDefault(x => x.Type == "user_id");
                    if (userIdClaim != null)
                    {
                        return userIdClaim.Value.Trim();
                    }
                }
                return string.Empty;
            }
        }

    }
}
