
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
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeServiceExamResults)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_SERVICE_EXAM_RESULTS)]

    public class EmployeeServiceExamResultsController : PermissionController
    {
        private readonly IEmployeeServiceExamResultService employeeServiceExamResultService;
        private readonly IServiceExamService serviceExamService;
        private readonly IServiceExamCategoryService serviceExamCategoryService;
 

        
        public EmployeeServiceExamResultsController(IEmployeeServiceExamResultService employeeServiceExamResultService,
            IServiceExamService serviceExamService, IServiceExamCategoryService serviceExamCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeeServiceExamResultService = employeeServiceExamResultService;
            this.serviceExamService = serviceExamService;
            this.serviceExamCategoryService = serviceExamCategoryService;
    

        }

        [HttpGet]
        [Route("get-employee-service-exam-results")]
        public IHttpActionResult GetEmployeeServiceExamResults(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeServiceExamResultModel> models = employeeServiceExamResultService.GetEmployeeServiceExamResults(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_SERVICE_EXAM_RESULTS);
            return Ok(new ResponseMessage<List<EmployeeServiceExamResultModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-service-exam-result")]
        public async Task<IHttpActionResult> GetEmployeeServiceExamResult(int id)
        {
            EmployeeServiceExamResultViewModel vm = new EmployeeServiceExamResultViewModel();
            vm.EmployeeServiceExamResult = await employeeServiceExamResultService.GetEmployeeServiceExamResult(id);
            vm.ServiceExamCategories = await serviceExamCategoryService.GetServiceExamCategorySelectModels();
            if (vm.EmployeeServiceExamResult != null)
            {
                vm.ServiceExams = await serviceExamService.GetServiceExamSelectModelsByServiceExamCategory(vm.EmployeeServiceExamResult.ServiceExamCategoryId);

            }

            return Ok(new ResponseMessage<EmployeeServiceExamResultViewModel>
            {
                Result = vm
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-employee-service-exam-result")]
        public async Task<IHttpActionResult> SaveEmployeeServiceExamResult([FromBody] EmployeeServiceExamResultModel model)
        {
            return Ok(new ResponseMessage<EmployeeServiceExamResultModel>
            {
                Result = await employeeServiceExamResultService.SaveEmployeeServiceExamResult(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-service-exam-result/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeServiceExamResult(int id, [FromBody] EmployeeServiceExamResultModel model)
        {
            return Ok(new ResponseMessage<EmployeeServiceExamResultModel>
            {
                Result = await employeeServiceExamResultService.SaveEmployeeServiceExamResult(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-service-exam-result/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeServiceExamResult(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeServiceExamResultService.DeleteEmployeeServiceExamResult(id)
            });
        }
    }
}