
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ObservationIntelligentReports)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.OBSERVATION_INTELLIGENT_REPORTS)]

    public class ObservationIntelligentReportsController : PermissionController
    {
        private readonly IObservationIntelligentService observationIntelligentService;

        public ObservationIntelligentReportsController(IObservationIntelligentService observationIntelligentService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.observationIntelligentService = observationIntelligentService;

    
            
        }

        [HttpGet]
        [Route("get-observation-intelligent-reports")]
        public IHttpActionResult GetObservationIntelligents(int ps, int pn, string qs)
        {
            int total = 0;
            List<ObservationIntelligentModel> models = observationIntelligentService.GetObservationIntelligents(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OBSERVATION_INTELLIGENT_REPORTS);
            return Ok(new ResponseMessage<List<ObservationIntelligentModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-observation-intelligent-report")]
        public async Task<IHttpActionResult> GetObservationIntelligent(int id)
        {
            ObservationIntelligentViewModel vm = new ObservationIntelligentViewModel();
            vm.ObservationIntelligent = await observationIntelligentService.GetObservationIntelligent(id);
            vm.ObservationIntelligentTypes = observationIntelligentService.GetObservationIntelligentTypeSelectModels();

   

            return Ok(new ResponseMessage<ObservationIntelligentViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-observation-intelligent-report")]
        public async Task<IHttpActionResult> SaveObservationIntelligent([FromBody] ObservationIntelligentModel model)
        {
            return Ok(new ResponseMessage<ObservationIntelligentModel>
            {
                Result = await observationIntelligentService.SaveObservationIntelligent(0, model)
            });
        }

        [HttpPut]
        [Route("update-observation-intelligent-report/{id}")]
        public async Task<IHttpActionResult> UpdateObservationIntelligent(int id, [FromBody] ObservationIntelligentModel model)
        {
            return Ok(new ResponseMessage<ObservationIntelligentModel>
            {
                Result = await observationIntelligentService.SaveObservationIntelligent(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-observation-intelligent-report/{id}")]
        public async Task<IHttpActionResult> DeleteObservationIntelligent(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await observationIntelligentService.DeleteObservationIntelligent(id)
            });
        }
    }
}