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
    [RoutePrefix(BnoisRoutePrefix.Ranks)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RANKS)]
    public class RankMapsController: PermissionController
    {
        private readonly IRankMapService rankMapService;
        public RankMapsController(IRankMapService rankMapService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.rankMapService=  rankMapService;
        }

        [HttpGet]
        [Route("get-rank-maps")]
        public IHttpActionResult GetRanks(int ps, int pn, string qs)
        {
            int total = 0;
            List<RankMapModel> rankModels = rankMapService.GetRankMaps(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RANKS);
            return Ok(new ResponseMessage<List<RankMapModel>>()
            {
                Result = rankModels,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-rank-map")]
        public async Task<IHttpActionResult> GetRankMap(int id)
        {
            RankMapModel model = await rankMapService.GetRankMap(id);
            return Ok(new ResponseMessage<RankMapModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-rank-map")]
        public async Task<IHttpActionResult> SaveRankMap([FromBody] RankMapModel model)
        {
            return Ok(new ResponseMessage<RankMapModel>
            {
                Result = await rankMapService.SaveRankMap(0, model)
            });
        }

        [HttpPut]
        [Route("update-rank-map/{id}")]
        public async Task<IHttpActionResult> UpdateRankMap(int id, [FromBody] RankMapModel model)
        {
            return Ok(new ResponseMessage<RankMapModel>
            {
                Result = await rankMapService.SaveRankMap(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-rank-map/{id}")]
        public async Task<IHttpActionResult> DeleteRankMap(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await rankMapService.DeleteRankMap(id)
            });
        }
    }
}
