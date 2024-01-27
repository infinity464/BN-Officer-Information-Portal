using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ResultTypes)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RESULT_TYPES)]

    public class ResultTypesController : PermissionController
    {
        private readonly IResultTypeService resultTypeService;

        public ResultTypesController(IResultTypeService resultTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.resultTypeService = resultTypeService;
        }

        [HttpGet]
        [Route("get-result-types")]
        public IHttpActionResult GetResultTypes(int ps, int pn, string qs)
        {
            int total = 0;
            List<ResultTypeModel> models = resultTypeService.GetResultTypes(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RESULT_TYPES);
            return Ok(new ResponseMessage<List<ResultTypeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-result-type")]
        public async Task<IHttpActionResult> GetResultType(int id)
        {
            ResultTypeModel model = await resultTypeService.GetResultType(id);
            return Ok(new ResponseMessage<ResultTypeModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-result-type")]
        public async Task<IHttpActionResult> SaveResultType([FromBody] ResultTypeModel model)
        {
            return Ok(new ResponseMessage<ResultTypeModel>
            {
                Result = await resultTypeService.SaveResultType(0, model)
            });
        }



        [HttpPut]
        [Route("update-result-type/{id}")]
        public async Task<IHttpActionResult> UpdateResultType(int id, [FromBody] ResultTypeModel model)
        {
            return Ok(new ResponseMessage<ResultTypeModel>
            {
                Result = await resultTypeService.SaveResultType(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-result-type/{id}")]
        public async Task<IHttpActionResult> DeleteResultType(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await resultTypeService.DeleteResultType(id)
            });
        }


    }
}