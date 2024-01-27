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
    [RoutePrefix(BnoisRoutePrefix.TerminationTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TERMINATION_TYPES)]

    public class TerminationTypesController : PermissionController
    {
        private readonly ITerminationTypeService terminationTypeService;

        public TerminationTypesController(ITerminationTypeService terminationTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.terminationTypeService = terminationTypeService;
        }

        [HttpGet]
        [Route("get-termination-types")]
        public IHttpActionResult GetTerminationTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<TerminationTypeModel> models = terminationTypeService.GetTerminationTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TERMINATION_TYPES);
            return Ok(new ResponseMessage<List<TerminationTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-termination-type")]
        public async Task<IHttpActionResult> GetTerminationType(int id)
        {
            TerminationTypeModel model = await terminationTypeService.GetTerminationType(id);
            return Ok(new ResponseMessage<TerminationTypeModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-termination-type")]
        public async Task<IHttpActionResult> SaveTerminationType([FromBody] TerminationTypeModel model)
        {
            return Ok(new ResponseMessage<TerminationTypeModel>
            {
                Result = await terminationTypeService.SaveTerminationType(0, model)
            });
        }



        [HttpPut]
        [Route("update-termination-type/{id}")]
        public async Task<IHttpActionResult> UpdateTerminationType(int id, [FromBody] TerminationTypeModel model)
        {
            return Ok(new ResponseMessage<TerminationTypeModel>
            {
                Result = await terminationTypeService.SaveTerminationType(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-termination-type/{id}")]
        public async Task<IHttpActionResult> DeleteTerminationType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await terminationTypeService.DeleteTerminationType(id)
            });
        }
    }
}