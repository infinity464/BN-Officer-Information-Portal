

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
    [RoutePrefix(BnoisRoutePrefix.RetiredAges)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RETIRED_AGES)]

    public class RetiredAgesController : PermissionController
    {
        private readonly IRetiredAgeService retiredAgeService;
        private readonly ICategoryService categoryService;
        private readonly ISubCategoryService subCategoryService;
        private readonly IRankService rankService;

        public RetiredAgesController(IRetiredAgeService retiredAgeService, ICategoryService categoryService, ISubCategoryService subCategoryService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.retiredAgeService = retiredAgeService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.rankService = rankService;

        }

        [HttpGet]
        [Route("get-retired-ages")]
        public IHttpActionResult GetRetiredAges(int ps, int pn, string qs)
        {
            int total = 0;
            List<RetiredAgeModel> models = retiredAgeService.GetRetiredAges(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.RETIRED_AGES);
            return Ok(new ResponseMessage<List<RetiredAgeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-retired-age")]
        public async Task<IHttpActionResult> GetRetiredAge(int id)
        {
            RetiredAgeViewModel vm = new RetiredAgeViewModel();
            vm.RetiredAge = await retiredAgeService.GetRetiredAge(id);
            vm.ListTypes = retiredAgeService.GetRStatusSelectModels();
            vm.Categories = await categoryService.GetCategorySelectModels();
            vm.SubCategories = await subCategoryService.GetSubCategorySelectModels();
            vm.Ranks = await rankService.GetRankSelectModels();
            return Ok(new ResponseMessage<RetiredAgeViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-retired-age")]
        public async Task<IHttpActionResult> SaveRetiredAge([FromBody] RetiredAgeModel model)
        {
            return Ok(new ResponseMessage<RetiredAgeModel>
            {
                Result = await retiredAgeService.SaveRetiredAge(0, model)
            });
        }

        [HttpPut]
        [Route("update-retired-age/{id}")]
        public async Task<IHttpActionResult> UpdateRetiredAge(int id, [FromBody] RetiredAgeModel model)
        {
            return Ok(new ResponseMessage<RetiredAgeModel>
            {
                Result = await retiredAgeService.SaveRetiredAge(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-retired-age/{id}")]
        public async Task<IHttpActionResult> DeleteRetiredAge(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await retiredAgeService.DeleteRetiredAge(id)
            });
        }

    }
}