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
    [RoutePrefix(BnoisRoutePrefix.VisitSubCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.VISIT_SUB_CATEGORIES)]

    public class VisitSubCategoriesController : PermissionController
    {
        private readonly IVisitSubCategoryService visitSubCategoryService;
        private readonly IVisitCategoryService visitCategoryService;

        public VisitSubCategoriesController(IVisitSubCategoryService visitSubCategoryService, IVisitCategoryService visitCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.visitSubCategoryService = visitSubCategoryService;
            this.visitCategoryService = visitCategoryService;
        }

        [HttpGet]
        [Route("get-visit-sub-categories")]
        public IHttpActionResult GetVisitSubCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<VisitSubCategoryModel> models = visitSubCategoryService.GetVisitSubCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.VISIT_SUB_CATEGORIES);
        return Ok(new ResponseMessage<List<VisitSubCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-visit-sub-category")]
        public async Task<IHttpActionResult> GetVisitSubCategory(int id)
        {
            VisitSubCategoryViewModel vm = new VisitSubCategoryViewModel();
            vm.VisitSubCategory = await visitSubCategoryService.GetVisitSubCategory(id);
            vm.VisitCategories = await visitCategoryService.GetVisitCategorySelectModels();
            return Ok(new ResponseMessage<VisitSubCategoryViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-visit-sub-category")]
        public async Task<IHttpActionResult> SaveVisitSubCategory([FromBody] VisitSubCategoryModel model)
        {
            return Ok(new ResponseMessage<VisitSubCategoryModel>
            {
                Result = await visitSubCategoryService.SaveVisitSubCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-visit-sub-category/{id}")]
        public async Task<IHttpActionResult> UpdateVisitSubCategory(int id, [FromBody] VisitSubCategoryModel model)
        {
            return Ok(new ResponseMessage<VisitSubCategoryModel>
            {
                Result = await visitSubCategoryService.SaveVisitSubCategory(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-visit-sub-category/{id}")]
        public async Task<IHttpActionResult> DeleteVisitSubCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await visitSubCategoryService.DeleteVisitSubCategory(id)
            });
        }


        [HttpGet]
        [Route("get-visit-sub-categories-by-Visit-category")]
        public async Task<IHttpActionResult> GetVisitSubCategorySelectModelsByVisitCategory(int visitCategoryId)
        {
            List<SelectModel> selectModels = await visitSubCategoryService.GetVisitSubCategorySelectModelsByVisitCategory(visitCategoryId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }
    }
}