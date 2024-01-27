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
    [RoutePrefix(BnoisRoutePrefix.PublicationCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUBLICATION_CATEGORIES)]
    public class PublicationCategoriesController : PermissionController
    {
        private readonly IPublicationCategoryService publicationCategoryService;

        public PublicationCategoriesController(IPublicationCategoryService publicationCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.publicationCategoryService = publicationCategoryService;
        }

        [HttpGet]
        [Route("get-publication-categories")]
        public IHttpActionResult GetPublicationCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = publicationCategoryService.GetPublicationCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUBLICATION_CATEGORIES);
            return Ok(new ResponseMessage<List<PublicationCategoryModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-publication-category")]
        public async Task<IHttpActionResult> GetPublicationCategory(int id)
        {
            var model = await publicationCategoryService.GetPublicationCategory(id);
            return Ok(new ResponseMessage<PublicationCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-publication-category")]
        public async Task<IHttpActionResult> SavePublicationCategory([FromBody] PublicationCategoryModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<PublicationCategoryModel>
            {
                Result = await publicationCategoryService.SavePublicationCategory(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-publication-category/{id}")]
        public async Task<IHttpActionResult> UpdatePublicationCategory(int id, [FromBody] PublicationCategoryModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<PublicationCategoryModel>
            {
                Result = await publicationCategoryService.SavePublicationCategory(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-publication-category/{id}")]
        public async Task<IHttpActionResult> DeletePublicationCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await publicationCategoryService.DeletePublicationCategory(id)
            });
        }
    }
}