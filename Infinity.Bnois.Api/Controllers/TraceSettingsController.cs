using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.TraceSettings)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRACE_SETTINGS)]

    public class TraceSettingsController : PermissionController
    {
        private readonly ITraceSettingService traceSettingservice;

        public TraceSettingsController(ITraceSettingService traceSettingservice, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.traceSettingservice = traceSettingservice;
        }

        [HttpGet]
        [Route("get-trace-settings")]
        public IHttpActionResult GetTraceSettings(int ps, int pn, string qs)
        {
            int total = 0;
            List<TraceSettingModel> models = traceSettingservice.GetTraceSettings(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRACE_SETTINGS);
            return Ok(new ResponseMessage<List<TraceSettingModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-trace-setting")]
        public async Task<IHttpActionResult> GetTraceSetting(int id)
        {
            TraceSettingModel model = await traceSettingservice.GetTraceSetting(id);
            return Ok(new ResponseMessage<TraceSettingModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-trace-setting")]
        public async Task<IHttpActionResult> SaveTraceSetting([FromBody] TraceSettingModel model)
        {
            return Ok(new ResponseMessage<TraceSettingModel>
            {
                Result = await traceSettingservice.SaveTraceSetting(0, model)
            });
        }



        [HttpPut]
        [Route("update-trace-setting/{id}")]
        public async Task<IHttpActionResult> UpdateTraceSetting(int id, [FromBody] TraceSettingModel model)
        {
            return Ok(new ResponseMessage<TraceSettingModel>
            {
                Result = await traceSettingservice.SaveTraceSetting(id, model)
            });
        }




        [HttpDelete]
        [Route("delete-trace-setting/{id}")]
        public async Task<IHttpActionResult> DeleteTraceSetting(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await traceSettingservice.DeleteTraceSetting(id)
            });
        }


    }
}