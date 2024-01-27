using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.BulkUser)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.BULKUSER)]

    public class BulkUserController : BaseController
    {
        private readonly IBulkUserService bulkUserService;

        public BulkUserController(IBulkUserService bulkUserService)
        {
            this.bulkUserService = bulkUserService;
        }
        [HttpGet]
        [Route("user-list-for-user-create")]
        public async Task<IHttpActionResult> UserList(int ps, int pn, string qs)
        {
            int total = 0;
            List<object> users =  bulkUserService.UserList(ps, pn, qs, out total);
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = users,
                Total = total
            });
        }
        [HttpGet]
        [Route("get-admin-dashboard")]
        public async Task<IHttpActionResult> AdminDashboard()
        {
            int total = 0;
            object dashboard = bulkUserService.GetAdminDashboard();
            return Ok(new ResponseMessage<object>()
            {
                Result = dashboard,
            });
        }
    }
}
