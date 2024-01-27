

using System;
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
    [RoutePrefix(BnoisRoutePrefix.AgeServicePolicies)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.AGE_SERVICE_POLICIES)]

    public class AgeServicPoliciesController : PermissionController
    {
        private readonly IAgeServicePolicyService ageServicePolicyService;
        private readonly ICategoryService categoryService;
        private readonly ISubCategoryService subCategoryService;
        private readonly IRankService rankService;
        private readonly IEmployeeGeneralService employeeGeneralService;

        public AgeServicPoliciesController(IEmployeeGeneralService employeeGeneralService, IAgeServicePolicyService ageServicePolicyService, ICategoryService categoryService, 
            ISubCategoryService subCategoryService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.ageServicePolicyService = ageServicePolicyService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.rankService = rankService;
            this.employeeGeneralService = employeeGeneralService;

        }

        [HttpGet]
        [Route("get-age-service-policies")]
        public IHttpActionResult GetAgeServicePolicies(int ps, int pn, string qs)
        {
            int total = 0;
            List<AgeServicePolicyModel> models = ageServicePolicyService.GetAgeServicePolicies(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.AGE_SERVICE_POLICIES);
            return Ok(new ResponseMessage<List<AgeServicePolicyModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-age-service-policy")]
        public async Task<IHttpActionResult> GetAgeServicePolicy(int id)
        {
            AgeServicePolicyViewModel vm = new AgeServicePolicyViewModel();
            vm.AgeServicePolicy = await ageServicePolicyService.GetAgeServicePolicy(id);
            vm.EarlyStatus = ageServicePolicyService.GetEarlyStatusSelectModels();
            vm.Categories = await categoryService.GetCategorySelectModels();
            vm.SubCategories = await subCategoryService.GetSubCategorySelectModels();
            vm.Ranks = await rankService.GetRankSelectModels();
            return Ok(new ResponseMessage<AgeServicePolicyViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-age-service-policy")]
        public async Task<IHttpActionResult> SaveAgeServicePolicy([FromBody] AgeServicePolicyModel model)
        {
            return Ok(new ResponseMessage<AgeServicePolicyModel>
            {
                Result = await ageServicePolicyService.SaveAgeServicePolicy(0, model)
            });
        }

        [HttpPut]
        [Route("update-age-service-policy/{id}")]
        public async Task<IHttpActionResult> UpdateAgeServicePolicy(int id, [FromBody] AgeServicePolicyModel model)
        {
            return Ok(new ResponseMessage<AgeServicePolicyModel>
            {
                Result = await ageServicePolicyService.SaveAgeServicePolicy(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-age-service-policy/{id}")]
        public async Task<IHttpActionResult> DeleteAgeServicePolicy(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await ageServicePolicyService.DeleteAgeServicePolicy(id)
            });
        }
	    [HttpGet]
	    [Route("get-lpr-date")]
	    public async Task<IHttpActionResult> GetLprServiceDate(int id)
	    {
		    
			EmployeeGeneralModel employee = await employeeGeneralService.GetEmployeeGenerals(id);
		   DateTime? lprDate = await ageServicePolicyService.GetLprServiceDate(employee);
		
			return Ok(new ResponseMessage<DateTime?>
		    {
			    Result = lprDate
			});
	    }

	}
}