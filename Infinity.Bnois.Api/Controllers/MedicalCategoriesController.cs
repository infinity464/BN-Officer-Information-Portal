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


    [RoutePrefix(BnoisRoutePrefix.MedicalCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MEDICAL_CATEGORIES)]

    public class MedicalCategoriesController : PermissionController
    {
        private readonly IMedicalCategoryService medicalCategoryService;

        public MedicalCategoriesController(IMedicalCategoryService medicalCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.medicalCategoryService = medicalCategoryService;
        }

        [HttpGet]
        [Route("get-medical-categories")]
        public IHttpActionResult GetMedicalCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<MedicalCategoryModel> models = medicalCategoryService.GetMedicalCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MEDICAL_CATEGORIES);
            return Ok(new ResponseMessage<List<MedicalCategoryModel>>()
            {
                Result = models,
                Total = total,
                Permission = permission
            });
        }


        [HttpGet]
        [Route("get-medical-category")]
        public async Task<IHttpActionResult> GetMedicalCategory(int medicalCategoryId)
        {
            MedicalCategoryModel model = await medicalCategoryService.GetMedicalCategory(medicalCategoryId);
            return Ok(new ResponseMessage<MedicalCategoryModel>()
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-medical-category")]
        public async Task<IHttpActionResult> SaveMedicalCategory([FromBody] MedicalCategoryModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<MedicalCategoryModel>()
            {
                Result = await medicalCategoryService.SaveMedicalCategory(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-medical-category/{id}")]
        public async Task<IHttpActionResult> UpdateMedicalCategory(int id, [FromBody] MedicalCategoryModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<MedicalCategoryModel>()
            {
                Result = await medicalCategoryService.SaveMedicalCategory(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-medical-category/{id}")]
        public async Task<IHttpActionResult> DeleteMedicalCategory(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await medicalCategoryService.DeleteMedicalCategory(id)
            });
        }
    }
}
