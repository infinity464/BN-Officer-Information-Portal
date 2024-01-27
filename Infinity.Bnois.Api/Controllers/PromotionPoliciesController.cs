using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PromotionPolicies)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PROMOTION_POLICIES)]

    public class PromotionPoliciesController : PermissionController
    {
        private readonly IPromotionPolicyService promotionPolicyService;
        private readonly IRankService rankService;

        public PromotionPoliciesController(IPromotionPolicyService promotionPolicyService, IRankService rankService, IRoleFeatureService roeFeatureService):base(roeFeatureService)
        {
            this.promotionPolicyService = promotionPolicyService;
            this.rankService = rankService;
        }

        [HttpGet]
        [Route("get-promotion-policies")]
        public IHttpActionResult GetPromotionPolicies()
        {
            List<PromotionPolicyModel> models = promotionPolicyService.GetPromotionPolicies();
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PROMOTION_POLICIES);
            return Ok(new ResponseMessage<List<PromotionPolicyModel>>()
            {
                Result = models,
                Permission = permission
            });
        }

        [HttpGet]
        [Route("get-promotion-policy")]
        public async Task<IHttpActionResult> GetPromotionPolicy(int id)
        {
            PromotionPolicyViewModel vm = new PromotionPolicyViewModel();
            vm.PromotionPolicy = await promotionPolicyService.GetPromotionPolicy(id);
            vm.Ranks = await rankService.GetRankSelectModelsByRankCategory(1);
            return Ok(new ResponseMessage<PromotionPolicyViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-promotion-policy")]
        public async Task<IHttpActionResult> SavePromotionPolicy([FromBody] PromotionPolicyModel model)
        {
            return Ok(new ResponseMessage<PromotionPolicyModel>
            {
                Result = await promotionPolicyService.SavePromotionPolicy(0, model)
            });
        }



        [HttpPut]
        [Route("update-promotion-policy/{id}")]
        public async Task<IHttpActionResult> UpdateDivision(int id, [FromBody] PromotionPolicyModel model)
        {
            return Ok(new ResponseMessage<PromotionPolicyModel>
            {
                Result = await promotionPolicyService.SavePromotionPolicy(id, model)
            });
        }



        [HttpDelete]
        [Route("delete-promotion-policy/{id}")]
        public async Task<IHttpActionResult> DeletePromotionPolicy(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await promotionPolicyService.DeletePromotionPolicy(id)
            });
        }


    }
}