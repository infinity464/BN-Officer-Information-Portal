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
    [RoutePrefix(BnoisRoutePrefix.Awards)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.AWARDS)]

    public class AwardsController : PermissionController
    {
        private readonly IAwardService awardService;

        public AwardsController(IAwardService awardService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.awardService = awardService;
        }

        [HttpGet]
        [Route("get-awards")]
        public IHttpActionResult GetAwards(int ps, int pn, string qs)
        {
            int total = 0;
            List<AwardModel> models = awardService.GetAwards(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.AWARDS);
            return Ok(new ResponseMessage<List<AwardModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-award")]
        public async Task<IHttpActionResult> GetAward(int id)
        {
            AwardModel model = await awardService.GetAward(id);
            return Ok(new ResponseMessage<AwardModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-award")]
        public async Task<IHttpActionResult> SaveAward([FromBody] AwardModel model)
        {
            return Ok(new ResponseMessage<AwardModel>
            {
                Result = await awardService.SaveAward(0, model)
            });
        }



        [HttpPut]
        [Route("update-award/{id}")]
        public async Task<IHttpActionResult> UpdateAward(int id, [FromBody] AwardModel model)
        {
            return Ok(new ResponseMessage<AwardModel>
            {
                Result = await awardService.SaveAward(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-award/{id}")]
        public async Task<IHttpActionResult> DeleteAward(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await awardService.DeleteAward(id)
            });
        }


    }
}