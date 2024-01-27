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
    [RoutePrefix(BnoisRoutePrefix.PunishmentCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUNISHMENT_CATEGORIES)]
    public class PunishmentCategoriesController : PermissionController
    {
        private readonly IPunishmentCategoryService punishmentCategoryService;

        public PunishmentCategoriesController(IPunishmentCategoryService punishmentCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.punishmentCategoryService = punishmentCategoryService;
        }

        [HttpGet]
        [Route("get-punishment-categories")]
        public IHttpActionResult GetPunishmentCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = punishmentCategoryService.GetPunishmentCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUNISHMENT_CATEGORIES);
            return Ok(new ResponseMessage<List<PunishmentCategoryModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-punishment-category")]
        public async Task<IHttpActionResult> GetPunishmentCategory(int id)
        {
            var model = await punishmentCategoryService.GetPunishmentCategory(id);
            return Ok(new ResponseMessage<PunishmentCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-punishment-category")]
        public async Task<IHttpActionResult> SavePunishmentCategory([FromBody] PunishmentCategoryModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<PunishmentCategoryModel>
            {
                Result = await punishmentCategoryService.SavePunishmentCategory(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-punishment-category/{id}")]
        public async Task<IHttpActionResult> UpdatePunishmentCategory(int id, [FromBody] PunishmentCategoryModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<PunishmentCategoryModel>
            {
                Result = await punishmentCategoryService.SavePunishmentCategory(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-punishment-category/{id}")]
        public async Task<IHttpActionResult> DeletePunishmentCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await punishmentCategoryService.DeletePunishmentCategory(id)
            });
        }
    }
}