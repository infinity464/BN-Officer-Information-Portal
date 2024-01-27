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
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Categories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.CATEGORIES)]

    public class CategoriesController:PermissionController
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService,IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        [Route("get-categories")]
        public IHttpActionResult GetCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<CategoryModel> categories = categoryService.GetCategories(ps, pn, qs, out total);
            var permission = base.GetFeature(MASTER_SETUP.CATEGORIES);
            return Ok(new ResponseMessage<List<CategoryModel>>()
            {
                Result = categories,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-category")]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            CategoryModel model = await categoryService.GetCategory(id);
            return Ok(new ResponseMessage<CategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-category")]
        public async Task<IHttpActionResult> SaveCategory([FromBody] CategoryModel model)
        {
            return Ok(new ResponseMessage<CategoryModel>
            {
                Result = await categoryService.SaveCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-category/{id}")]
        public async Task<IHttpActionResult> UpdateCategory(int id, [FromBody] CategoryModel model)
        {
            return Ok(new ResponseMessage<CategoryModel>
            {
                Result = await categoryService.SaveCategory(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-category/{id}")]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await categoryService.DeleteCategoryAsync(id)
            });
        }



        [HttpGet]
        [Route("get-category-select-models")]
        public async Task<IHttpActionResult> GetCategorySelectModels()
        {
            List<SelectModel> selectModels = await categoryService.GetCategorySelectModels();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = selectModels
            });
        }
    }
}
