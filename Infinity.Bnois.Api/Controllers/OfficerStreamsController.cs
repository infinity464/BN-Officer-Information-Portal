using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.OfficerStreams)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.STREAMS)]

    public class OfficerStreamsController: PermissionController
    {
        private readonly IOfficerStreamService officerStreamService;
        public OfficerStreamsController(IOfficerStreamService officerStreamService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.officerStreamService = officerStreamService;
        }

        [HttpGet]
        [Route("get-officer-streams")]
        public IHttpActionResult GetOfficerStreams(int ps, int pn, string qs)
        {
            int total = 0;
            List<OfficerStreamModel> models = officerStreamService.GetOfficerStreams(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.STREAMS);
            return Ok(new ResponseMessage<List<OfficerStreamModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-officer-stream")]
        public async Task<IHttpActionResult> GetOfficerStream(int id)
        {
            OfficerStreamModel model = await officerStreamService.GetOfficerStream(id);
            return Ok(new ResponseMessage<OfficerStreamModel>
            {
                Result = model
            });
        }

        [HttpGet]
        [Route("get-officer-stream-select-models")]
        public async Task<IHttpActionResult> GetOfficerStreamSelectModels()
        {
           
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await officerStreamService.GetOfficerStreamSelectModels()

            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-officer-stream")]
        public async Task<IHttpActionResult> SaveOfficerStream([FromBody] OfficerStreamModel model)
        {
            return Ok(new ResponseMessage<OfficerStreamModel>
            {
                Result = await officerStreamService.SaveOfficerStream(0, model)
            });
        }

        [HttpPut]
        [Route("update-officer-stream/{id}")]
        public async Task<IHttpActionResult> UpdateOfficerStream(int id, [FromBody] OfficerStreamModel model)
        {
            return Ok(new ResponseMessage<OfficerStreamModel>
            {
                Result = await officerStreamService.SaveOfficerStream(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-officer-stream/{id}")]
        public async Task<IHttpActionResult> DeleteOfficerStream(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await officerStreamService.DeleteOfficerStream(id)
            });
        }
    }
}
