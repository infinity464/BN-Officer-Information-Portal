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
    [RoutePrefix(BnoisRoutePrefix.ServiceExams)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SERVICE_EXAMS)]

    public class ServiceExamsController : PermissionController
    {
        private readonly IServiceExamService serviceExamService;
        private readonly IServiceExamCategoryService serviceExamCategoryService;
        private readonly IBranchService branchService;

        public ServiceExamsController(IServiceExamService serviceExamService, IServiceExamCategoryService serviceExamCategoryService, IBranchService branchService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.serviceExamService = serviceExamService;
            this.serviceExamCategoryService = serviceExamCategoryService;
            this.branchService = branchService;
        }

        [HttpGet]
        [Route("get-service-exams")]
        public IHttpActionResult GetServiceExams(int ps, int pn, string qs)
        {
            int total = 0;
            List<ServiceExamModel> models = serviceExamService.GetServiceExams(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SERVICE_EXAMS);
            return Ok(new ResponseMessage<List<ServiceExamModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-service-exam")]
        public async Task<IHttpActionResult> GetServiceExam(int id)
        {
            ServiceExamViewModel vm = new ServiceExamViewModel();
            vm.ServiceExam = await serviceExamService.GetServiceExam(id);
            vm.ServiceExamCategories = await serviceExamCategoryService.GetServiceExamCategorySelectModels();
            vm.Branches = await branchService.GetBranchSelectModels();
            return Ok(new ResponseMessage<ServiceExamViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-service-exam-by-service-exam-category")]
        public async Task<IHttpActionResult> GetServiceExamByServiceExamCategory(int id)
        {

            List<SelectModel> serviceExams = await serviceExamService.GetServiceExamSelectModelsByServiceExamCategory(id);
           
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = serviceExams
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-service-exam")]
        public async Task<IHttpActionResult> SaveServiceExam([FromBody] ServiceExamModel model)
        {
            return Ok(new ResponseMessage<ServiceExamModel>
            {
                Result = await serviceExamService.SaveServiceExam(0, model)
            });
        }

        [HttpPut]
        [Route("update-service-exam/{id}")]
        public async Task<IHttpActionResult> UpdateServiceExam(int id, [FromBody] ServiceExamModel model)
        {
            return Ok(new ResponseMessage<ServiceExamModel>
            {
                Result = await serviceExamService.SaveServiceExam(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-service-exam/{id}")]
        public async Task<IHttpActionResult> DeleteServiceExam(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await serviceExamService.DeleteServiceExam(id)
            });
        }
    }
}