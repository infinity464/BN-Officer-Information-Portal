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
    [RoutePrefix(BnoisRoutePrefix.PreCommsionRanks)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PRE_COMMISSION_RANKS)]

    public class PreCommissionRanksController : PermissionController
    {
        private readonly IPreCommissionRankService preCommissionRankService;
        public PreCommissionRanksController(IPreCommissionRankService preCommissionRankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.preCommissionRankService = preCommissionRankService;
        }

        [HttpGet]
        [Route("get-pre-commission-ranks")]
        public IHttpActionResult GetPreCommissionRanks(int ps, int pn, string qs)
        {
            int total = 0;
            List<PreCommissionRankModel> models = preCommissionRankService.GetPreCommissionRanks(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PRE_COMMISSION_RANKS);
            return Ok(new ResponseMessage<List<PreCommissionRankModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-pre-commission-rank")]
        public async Task<IHttpActionResult> GetPreCommissionRank(int id)
        {
            PreCommissionRankModel model = await preCommissionRankService.GetPreCommissionRank(id);
            return Ok(new ResponseMessage<PreCommissionRankModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-pre-commission-rank")]
        public async Task<IHttpActionResult> SavePreCommissionRank([FromBody] PreCommissionRankModel model)
        {
            return Ok(new ResponseMessage<PreCommissionRankModel>
            {
                Result = await preCommissionRankService.SavePreCommissionRank(0, model)
            });
        }

        [HttpPut]
        [Route("update-pre-commission-rank/{id}")]
        public async Task<IHttpActionResult> UpdatePreCommissionRank(int id, [FromBody] PreCommissionRankModel model)
        {
            return Ok(new ResponseMessage<PreCommissionRankModel>
            {
                Result = await preCommissionRankService.SavePreCommissionRank(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-pre-commission-rank/{id}")]
        public async Task<IHttpActionResult> DeletePreCommissionRank(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await preCommissionRankService.DeletePreCommissionRank(id)
            });
        }
    }
}
