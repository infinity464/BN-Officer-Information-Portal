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
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.UsedStoreProcedures)]
    [EnableCors("*", "*", "*")]

    public class UsedStoreProceduresController : PermissionController
    {
        private readonly IUsedStoreProcedureService UsedStoreProcedureService;
        public UsedStoreProceduresController(IUsedStoreProcedureService UsedStoreProcedureService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.UsedStoreProcedureService = UsedStoreProcedureService;
        }

        [HttpGet]
        [Route("get-used-store-procedures")]
        public IHttpActionResult GetUsedStoreProcedures(int id)
        {
            int total = 0;
            List<UsedStoreProcedureModel> models = UsedStoreProcedureService.GetUsedStoreProcedures(id);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<UsedStoreProcedureModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-used-store-procedure")]
        public async Task<IHttpActionResult> SaveUsedStoreProcedure([FromBody] UsedStoreProcedureModel model)
        {
            return Ok(new ResponseMessage<UsedStoreProcedureModel>
            {
                Result = await UsedStoreProcedureService.SaveUsedStoreProcedure(0, model)
            });
        }

        [HttpPut]
        [Route("update-used-store-procedure/{id}")]
        public async Task<IHttpActionResult> UpdateUsedStoreProcedure(int id, [FromBody] UsedStoreProcedureModel model)
        {
            return Ok(new ResponseMessage<UsedStoreProcedureModel>
            {
                Result = await UsedStoreProcedureService.SaveUsedStoreProcedure(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-used-store-procedure/{id}")]
        public async Task<IHttpActionResult> DeleteUsedStoreProcedure(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await UsedStoreProcedureService.DeleteUsedStoreProcedure(id)
            });
        }
    }
}
