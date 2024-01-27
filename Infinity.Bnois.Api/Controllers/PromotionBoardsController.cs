using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PromotionBoards)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PROMOTION_BOARDS)]
    public class PromotionBoardsController : PermissionController
    {
        private readonly IPromotionBoardService promotionBoardService;
        private readonly IRankService rankService;
        public PromotionBoardsController(IPromotionBoardService promotionBoardService,
            IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.promotionBoardService = promotionBoardService;
            this.rankService = rankService;
        }

        [HttpGet]
        [Route("get-promotion-boards")]
        public IHttpActionResult GetPromotionBoards(int ps, int pn, string qs,int type)
        {
            int total = 0;
            List<PromotionBoardModel> models = promotionBoardService.GetPromotionBoards(ps, pn, qs, out total,type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_BOARDS);
            return Ok(new ResponseMessage<List<PromotionBoardModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-promotion-board")]
        public async Task<IHttpActionResult> GetPromotionBoard(int id)
        {
            PromotionBoardViewModel vm = new PromotionBoardViewModel();
            vm.PromotionBoard = await promotionBoardService.GetPromotionBoard(id);
            vm.FromConfirmRanks = await rankService.GetConfirmRankSelectModels();
            vm.ToActingRanks = await rankService.GetActingRankSelectModels();
            return Ok(new ResponseMessage<PromotionBoardViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-promotion-board")]
        public async Task<IHttpActionResult> SavePromotionBoard([FromBody] PromotionBoardModel model)
        {
            return Ok(new ResponseMessage<PromotionBoardModel>
            {
                Result = await promotionBoardService.SavePromotionBoard(0, model)
            });
        }
        [HttpPut]
        [Route("update-promotion-board/{id}")]
        public async Task<IHttpActionResult> UpdatePromotionBoard(int id, [FromBody] PromotionBoardModel model)
        {
            return Ok(new ResponseMessage<PromotionBoardModel>
            {
                Result = await promotionBoardService.SavePromotionBoard(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-promotion-board/{id}")]
        public async Task<IHttpActionResult> DeletePromotionBoard(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionBoardService.DeletePromotionBoard(id)
            });
        }

        [HttpGet]
        [ModelValidation]
        [Route("calculate-trace")]
        public async Task<IHttpActionResult> CalculateTrace(int promotionBoardId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionBoardService.CalculateTrace(promotionBoardId)
            });
        }

    }
}
