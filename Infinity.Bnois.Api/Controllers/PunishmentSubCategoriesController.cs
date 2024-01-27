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
    [RoutePrefix(BnoisRoutePrefix.PunishmentSubCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.PUNISHMENT_SUB_CATEGORIES)]

    public class PunishmentSubCategoriesController : PermissionController
    {
        private readonly IPunishmentSubCategoryService punishmentSubCategoryService;
        private readonly IPunishmentCategoryService punishmentCategoryService;

        public PunishmentSubCategoriesController(IPunishmentSubCategoryService punishmentSubCategoryService, IPunishmentCategoryService punishmentCategoryService,
            IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.punishmentSubCategoryService = punishmentSubCategoryService;
            this.punishmentCategoryService = punishmentCategoryService;
        }

        [HttpGet]
        [Route("get-punishment-sub-categories")]
        public IHttpActionResult GetPunishmentSubCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<PunishmentSubCategoryModel> models = punishmentSubCategoryService.GetPunishmentSubCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.PUNISHMENT_SUB_CATEGORIES);
            return Ok(new ResponseMessage<List<PunishmentSubCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-punishment-sub-category")]
        public async Task<IHttpActionResult> GetPunishmentSubCategory(int id)
        {
            PunishmentSubCategoryViewModel vm = new PunishmentSubCategoryViewModel();
            vm.PunishmentSubCategory = await punishmentSubCategoryService.GetPunishmentSubCategory(id);
            vm.PunishmentCategories = await punishmentCategoryService.GetPunishmentCategorySelectModels();
            return Ok(new ResponseMessage<PunishmentSubCategoryViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-punishment-sub-category")]
        public async Task<IHttpActionResult> SavePunishmentSubCategory([FromBody] PunishmentSubCategoryModel model)
        {
            return Ok(new ResponseMessage<PunishmentSubCategoryModel>
            {
                Result = await punishmentSubCategoryService.SavePunishmentSubCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-punishment-sub-category/{id}")]
        public async Task<IHttpActionResult> UpdatePunishmentSubCategory(int id, [FromBody] PunishmentSubCategoryModel model)
        {
            return Ok(new ResponseMessage<PunishmentSubCategoryModel>
            {
                Result = await punishmentSubCategoryService.SavePunishmentSubCategory(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-punishment-sub-category/{id}")]
        public async Task<IHttpActionResult> DeletePunishmentSubCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await punishmentSubCategoryService.DeletePunishmentSubCategory(id)
            });
        }


        [HttpGet]
        [Route("get-punishment-sub-categories-by-punishment-category")]
        public async Task<IHttpActionResult> GetPunishmentSubCategorySelectModelsByPunishmentCategory(int punishmentCategoryId)
        {
            List<SelectModel> selectModels = await punishmentSubCategoryService.GetPunishmentSubCategorySelectModelsByPunishmentCategory(punishmentCategoryId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }
    }
}