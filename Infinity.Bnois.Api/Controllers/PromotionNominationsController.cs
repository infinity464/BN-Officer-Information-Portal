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
    [RoutePrefix(BnoisRoutePrefix.PromotionNominations)]
    [EnableCors("*", "*", "*")]
   [ActionAuthorize(Feature = MASTER_SETUP.PROMOTION_BOARDS)]
    public class PromotionNominationsController : PermissionController
    {
        private readonly IPromotionNominationService promotionNominationService;
        private readonly IExecutionRemarkService executionRemarkService;
        public PromotionNominationsController(IPromotionNominationService promotionNominationService, IExecutionRemarkService executionRemarkService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.promotionNominationService = promotionNominationService;
            this.executionRemarkService = executionRemarkService;
        }

        [HttpGet]
        [Route("get-promotion-nominations")]
        public IHttpActionResult GetPromotionNominations(int promotionBoardId, int ps, int pn, string qs,int type)
        {
            int total = 0;
            List<PromotionNominationModel> models = promotionNominationService.GetPromotionNominations(promotionBoardId, ps, pn, qs, out total,type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_BOARDS);
            return Ok(new ResponseMessage<List<PromotionNominationModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-promotion-nomination")]
        public async Task<IHttpActionResult> GetPromotionNomination(int promotionBoardId, int promotionNominationId)
        {
            PromotionNominationViewModel vm = new PromotionNominationViewModel();
            vm.PromotionNomination = await promotionNominationService.GetPromotionNomination(promotionNominationId);
            
          
            return Ok(new ResponseMessage<PromotionNominationViewModel>()
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-promotion-nomination")]
        public async Task<IHttpActionResult> SavePromotionNomination([FromBody] PromotionNominationModel model)
        {
            return Ok(new ResponseMessage<PromotionNominationModel>
            {
                Result = await promotionNominationService.SavePromotionNomination(0, model)
            });
        }

        [HttpPut]
        [Route("update-promotion-nomination/{id}")]
        public async Task<IHttpActionResult> UpdatePromotionNomination(int id, [FromBody] PromotionNominationModel model)
        {
            return Ok(new ResponseMessage<PromotionNominationModel>
            {
                Result = await promotionNominationService.SavePromotionNomination(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-promotion-nomination/{id}")]
        public async Task<IHttpActionResult> DeletePromotionNomination(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionNominationService.DeletePromotionNomination(id)
            });
        }
        
    }
}
