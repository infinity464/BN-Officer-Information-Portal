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
    [RoutePrefix(BnoisRoutePrefix.Divisions)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.DIVISIONS)]

    public class DivisionsController : PermissionController
    {
        private readonly IDivisionService divisionService;

        public DivisionsController(IDivisionService divisionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.divisionService = divisionService;
        }

        [HttpGet]
        [Route("get-divisions")]
        public IHttpActionResult GetDivisions(int ps, int pn, string qs)
        {
            int total = 0;
            List<DivisionModel> models = divisionService.GetDivisions(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.DIVISIONS);
            return Ok(new ResponseMessage<List<DivisionModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-division")]
        public async Task<IHttpActionResult> GetDivision(int id)
        {
            DivisionModel model = await divisionService.GetDivision(id);
            return Ok(new ResponseMessage<DivisionModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-division")]
        public async Task<IHttpActionResult> SaveDivision([FromBody] DivisionModel model)
        {
            return Ok(new ResponseMessage<DivisionModel>
            {
                Result = await divisionService.SaveDivision(0, model)
            });
        }



        [HttpPut]
        [Route("update-division/{id}")]
        public async Task<IHttpActionResult> UpdateDivision(int id, [FromBody] DivisionModel model)
        {
            return Ok(new ResponseMessage<DivisionModel>
            {
                Result = await divisionService.SaveDivision(id, model)
            });
        }



        [HttpDelete]
        [Route("delete-division/{id}")]
        public async Task<IHttpActionResult> DeleteDivision(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await divisionService.DeleteDivision(id)
            });
        }


    }
}