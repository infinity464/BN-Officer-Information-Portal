using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.VisitCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.VISIT_CATEGORIES)]
    public class VisitCategoriesController : PermissionController
    {
        private readonly IVisitCategoryService visitCategoryService;

        public VisitCategoriesController(IVisitCategoryService visitCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.visitCategoryService = visitCategoryService;
        }

        [HttpGet]
        [Route("get-visit-categories")]
        public IHttpActionResult GetVisitCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = visitCategoryService.GetVisitCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.VISIT_CATEGORIES);
            return Ok(new ResponseMessage<List<VisitCategoryModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-visit-category")]
        public async Task<IHttpActionResult> GetVisitCategory(int id)
        {
            var model = await visitCategoryService.GetVisitCategory(id);
            return Ok(new ResponseMessage<VisitCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-visit-category")]
        public async Task<IHttpActionResult> SaveVisitCategory([FromBody] VisitCategoryModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<VisitCategoryModel>
            {
                Result = await visitCategoryService.SaveVisitCategory(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-visit-category/{id}")]
        public async Task<IHttpActionResult> UpdateVisitCategory(int id, [FromBody] VisitCategoryModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<VisitCategoryModel>
            {
                Result = await visitCategoryService.SaveVisitCategory(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-visit-category/{id}")]
        public async Task<IHttpActionResult> DeleteVisitCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await visitCategoryService.DeleteVisitCategory(id)
            });
        }
    }
}