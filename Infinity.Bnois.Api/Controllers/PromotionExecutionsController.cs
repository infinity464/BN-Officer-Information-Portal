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
    [RoutePrefix(BnoisRoutePrefix.PromotionExecutions)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PROMOTION_EXECUTIONS)]
    public class PromotionExecutionsController : PermissionController
    {
        private readonly IPromotionNominationService promotionNominationService;
        private readonly IExecutionRemarkService executionRemarkService;
        private readonly IPromotionBoardService promotionBoardService;
        private readonly IRankService rankService;
        public PromotionExecutionsController(IRankService rankService, IExecutionRemarkService executionRemarkService, IPromotionNominationService promotionNominationService, IPromotionBoardService promotionBoardService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.promotionNominationService = promotionNominationService;
            this.promotionBoardService = promotionBoardService;
            this.executionRemarkService = executionRemarkService;
            this.rankService = rankService;
        }

        [HttpGet]
        [Route("get-promotion-executions")]
        public IHttpActionResult GetPromotionExecutions(int ps, int pn, string qs, int type)
        {
            int total = 0;
            List<PromotionBoardModel> models = promotionBoardService.GetPromotionBoards(ps, pn, qs, out total, type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_EXECUTIONS);
            return Ok(new ResponseMessage<List<PromotionBoardModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-promotion-nominations-by-promotion-board")]
        public async Task<IHttpActionResult> GetPromotionNominationList(int promotionBoardId,int type)
        {
            PromotionNominationViewModel vm = new PromotionNominationViewModel();
            vm.PromotionNominations = promotionNominationService.GetPromotionNominations(promotionBoardId);
            vm.ExecutionRemarks = await executionRemarkService.GetExecutionRemarkSelectModels(type);

            return Ok(new ResponseMessage<PromotionNominationViewModel>()
            {
                Result = vm
            });
        }
        [HttpPut]
        [Route("update-promotion-executed-list/{promotionBoardId}")]
        public async Task<IHttpActionResult> UpdatePromotionNomination(int promotionBoardId, [FromBody] List<PromotionNominationModel> models)
        {
            return Ok(new ResponseMessage<List<PromotionNominationModel>>
            {
                Result = await promotionNominationService.UpdatePromotionNominations(promotionBoardId, models)
            });
        }

        [HttpGet]
        [Route("get-promotion-execution-without-boards")]
        public IHttpActionResult GetPromotionExecutionWithoutBoards(int ps, int pn, string qs)
        {
            int total = 0;
            List<PromotionNominationModel> models = promotionNominationService.GetPromotionExecutionWithoutBoards(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_EXECUTIONS);
            return Ok(new ResponseMessage<List<PromotionNominationModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-promotion-execution-without-board")]
        public async Task<IHttpActionResult> GetPromotionExecutionWithoutBoard(int promotionNominationId)
        {
            PromotionNominationViewModel vm = new PromotionNominationViewModel();
            vm.PromotionNomination = await promotionNominationService.GetPromotionNomination(promotionNominationId);
            vm.Ranks = await rankService.GetRankSelectModels();
            return Ok(new ResponseMessage<PromotionNominationViewModel>()
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-promotion-nomination-without-board")]
        public async Task<IHttpActionResult> SavePromotionNominationWithoutBoard([FromBody] PromotionNominationModel model)
        {
            return Ok(new ResponseMessage<PromotionNominationModel>
            {
                Result = await promotionNominationService.SavePromotionNomination(0, model)
            });
        }

        [HttpPut]
        [Route("update-promotion-execution-without-board/{promotionNominationId}")]
        public async Task<IHttpActionResult> UpdatePromotionNominationWithoutBoard(int promotionNominationId, [FromBody] PromotionNominationModel model)
        {
            return Ok(new ResponseMessage<PromotionNominationModel>
            {
                Result = await promotionNominationService.SavePromotionNomination(promotionNominationId, model)
            });
        }

    }
}
