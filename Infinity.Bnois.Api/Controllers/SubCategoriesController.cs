
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
    [RoutePrefix(BnoisRoutePrefix.SubCategories)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SUB_CATEGORIES)]

    public class SubCategoriesController : PermissionController
    {
        private readonly ISubCategoryService subCategoryService;
        private readonly ICategoryService categoryService;
        
        public SubCategoriesController(ISubCategoryService subCategoryService, ICategoryService categoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.subCategoryService = subCategoryService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Route("get-sub-categories")]
        public IHttpActionResult GetSubCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<SubCategoryModel> models = subCategoryService.GetSubCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SUB_CATEGORIES);
            return Ok(new ResponseMessage<List<SubCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-sub-category")]
        public async Task<IHttpActionResult> GetSubCategory(int id)
        {
            SubCategoryViewModel vm = new SubCategoryViewModel();
            vm.SubCategory = await subCategoryService.GetSubCategory(id);
            vm.SubCategories = await categoryService.GetCategorySelectModels();
            return Ok(new ResponseMessage<SubCategoryViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-sub-category")]
        public async Task<IHttpActionResult> SaveSubCategory([FromBody] SubCategoryModel model)
        {
            return Ok(new ResponseMessage<SubCategoryModel>
            {
                Result = await subCategoryService.SaveSubCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-sub-category/{id}")]
        public async Task<IHttpActionResult> UpdateSubCategory(int id, [FromBody] SubCategoryModel model)
        {
            return Ok(new ResponseMessage<SubCategoryModel>
            {
                Result = await subCategoryService.SaveSubCategory(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-sub-category/{id}")]
        public async Task<IHttpActionResult> DeleteSubCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await subCategoryService.DeleteSubCategory(id)
            });
        }


        [HttpGet]
        [Route("get-sub-category-select-models")]
        public async Task<IHttpActionResult> GetSubCategorySelectModels()
        {
            List<SelectModel> selectModels = await subCategoryService.GetSubCategorySelectModels();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }
    }
}