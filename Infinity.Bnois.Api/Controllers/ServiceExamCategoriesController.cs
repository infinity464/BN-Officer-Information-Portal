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
    [RoutePrefix(BnoisRoutePrefix.ServiceExamCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SERVICE_EXAM_CATEGORIES)]
    public class ServiceExamCategoriesController : PermissionController
    {
        private readonly IServiceExamCategoryService serviceExamCategoryService;

        public ServiceExamCategoriesController(IServiceExamCategoryService serviceExamCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.serviceExamCategoryService = serviceExamCategoryService;
        }

        [HttpGet]
        [Route("get-service-exam-categories")]
        public IHttpActionResult GetServiceExamCategories(int ps, int pn, string qs)
        {
            var total = 0;
            var models = serviceExamCategoryService.GetServiceExamCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SERVICE_EXAM_CATEGORIES);
            return Ok(new ResponseMessage<List<ServiceExamCategoryModel>>
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-service-exam-category")]
        public async Task<IHttpActionResult> GetServiceExamCategory(int id)
        {
            var model = await serviceExamCategoryService.GetServiceExamCategory(id);
            return Ok(new ResponseMessage<ServiceExamCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-service-exam-category")]
        public async Task<IHttpActionResult> SaveServiceExamCategory([FromBody] ServiceExamCategoryModel model)
        {
            model.CreatedBy = UserId;
            return Ok(new ResponseMessage<ServiceExamCategoryModel>
            {
                Result = await serviceExamCategoryService.SaveServiceExamCategory(0, model)
            });
        }


        [HttpPut]
        [ModelValidation]
        [Route("update-service-exam-category/{id}")]
        public async Task<IHttpActionResult> UpdateServiceExamCategory(int id, [FromBody] ServiceExamCategoryModel model)
        {
            model.ModifiedBy = UserId;
            return Ok(new ResponseMessage<ServiceExamCategoryModel>
            {
                Result = await serviceExamCategoryService.SaveServiceExamCategory(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-service-exam-category/{id}")]
        public async Task<IHttpActionResult> DeleteServiceExamCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await serviceExamCategoryService.DeleteServiceExamCategory(id)
            });
        }
    }
}