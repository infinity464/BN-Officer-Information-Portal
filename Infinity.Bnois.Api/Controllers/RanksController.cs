using Infinity.Bnois.Api.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Ranks)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RANKS)]
    public class RanksController:PermissionController
    {
        private readonly IRankService rankService;
        private readonly IRankCategoryService rankCategoryService;
        public RanksController(IRankService rankService, IRankCategoryService rankCategoryService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.rankService = rankService;
            this.rankCategoryService = rankCategoryService;
        }

        [HttpGet]
        [Route("get-ranks")]
        public IHttpActionResult GetRanks(int ps, int pn, string qs)
        {
            int total = 0;
            List<RankModel> ranks = rankService.GetRanks(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RANKS);
            return Ok(new ResponseMessage<List<RankModel>>()
            {
                Result = ranks,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-rank")]
        public async Task<IHttpActionResult> GetRank(int id)
        {
            RankViewModel vm = new RankViewModel();
            vm.Rank = await rankService.GetRank(id);
            vm.RankCategories = await rankCategoryService.GetRankCategorySelectModels();
            return Ok(new ResponseMessage<RankViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-rank")]
        public async Task<IHttpActionResult> SaveRank([FromBody] RankModel model)
        {
            return Ok(new ResponseMessage<RankModel>
            {
                Result = await rankService.SaveRank(0, model)
            });
        }

        [HttpPut]
        [Route("update-rank/{id}")]
        public async Task<IHttpActionResult> UpdateRank(int id, [FromBody] RankModel model)
        {
            return Ok(new ResponseMessage<RankModel>
            {
                Result = await rankService.SaveRank(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-rank/{id}")]
        public async Task<IHttpActionResult> DeleteRank(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await rankService.DeleteRank(id)
            });
        }

        [HttpGet]
        [Route("get-ranks-by-rank-category")]
        public async Task<IHttpActionResult> GetRankSelectModelsByRankCategory(int rankCategoryId)
        {
            List<SelectModel> rankSelectModels = await rankService.GetRankSelectModelsByRankCategory(rankCategoryId);
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = rankSelectModels
            });
        }

        [HttpGet]
        [Route("get-rank-select-models")]
        public async Task<IHttpActionResult> GetRankSelectModel()
        {
            List<SelectModel> rankSelectModels = await rankService.GetRanksSelectModel();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = rankSelectModels
            });
        }
    }
}