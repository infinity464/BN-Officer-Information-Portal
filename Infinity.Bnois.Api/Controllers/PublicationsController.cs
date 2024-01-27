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
    [RoutePrefix(BnoisRoutePrefix.Pubications)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUBLICATIONS)]

    public class PublicationsController : PermissionController
    {
        private readonly IPublicationService publicationService;
        private readonly IPublicationCategoryService publicationCategoryService;

        public PublicationsController(IPublicationService publicationService, IPublicationCategoryService publicationCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.publicationService = publicationService;
            this.publicationCategoryService = publicationCategoryService;
        }

        [HttpGet]
        [Route("get-publications")]
        public IHttpActionResult GetPublications(int ps, int pn, string qs)
        {
            int total = 0;
            List<PublicationModel> models = publicationService.GetPublications(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUBLICATIONS);
            return Ok(new ResponseMessage<List<PublicationModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-publication")]
        public async Task<IHttpActionResult> GetPublication(int id)
        {
            PublicationViewModel vm = new PublicationViewModel();
            vm.Publication = await publicationService.GetPublication(id);
            vm.PublicationCategories = await publicationCategoryService.GetPublicationCategorySelectModels();
            return Ok(new ResponseMessage<PublicationViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-publication")]
        public async Task<IHttpActionResult> SavePublication([FromBody] PublicationModel model)
        {
            return Ok(new ResponseMessage<PublicationModel>
            {
                Result = await publicationService.SavePublication(0, model)
            });
        }

        [HttpPut]
        [Route("update-publication/{id}")]
        public async Task<IHttpActionResult> UpdatePublication(int id, [FromBody] PublicationModel model)
        {
            return Ok(new ResponseMessage<PublicationModel>
            {
                Result = await publicationService.SavePublication(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-publication/{id}")]
        public async Task<IHttpActionResult> DeletePublication(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await publicationService.DeletePublication(id)
            });
        }
    }
}