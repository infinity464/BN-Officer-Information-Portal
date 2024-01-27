using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infinity.Bnois.Api.Web.Results
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult'
    public class ChallengeResult : IHttpActionResult
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.ChallengeResult(string, ApiController)'
        public ChallengeResult(string loginProvider, ApiController controller)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.ChallengeResult(string, ApiController)'
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.LoginProvider'
        public string LoginProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.LoginProvider'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.Request'
        public HttpRequestMessage Request { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.Request'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.ExecuteAsync(CancellationToken)'
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ChallengeResult.ExecuteAsync(CancellationToken)'
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}
