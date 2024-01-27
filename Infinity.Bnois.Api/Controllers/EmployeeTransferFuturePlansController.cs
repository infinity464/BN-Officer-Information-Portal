
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
    [RoutePrefix(BnoisRoutePrefix.EmployeeTransferFuturePlans)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_TRANSFER_FUTURE_PLANS)]

    public class EmployeeTransferFuturePlansController : PermissionController
    {
        private readonly IEmployeeTransferFuturePlanService employeeTransferFuturePlanService; 
        private readonly IAppointmentNatureService appointmentNatureService;
        private readonly IAppointmentCategoryService appointmentCategoryService;
        private readonly IOfficeService officeService;
        private readonly IPatternService patternService;
        private readonly ICountryService countryService;
 

        
        public EmployeeTransferFuturePlansController(IEmployeeTransferFuturePlanService employeeTransferFuturePlanService,
            IAppointmentNatureService appointmentNatureService, IAppointmentCategoryService appointmentCategoryService,
            IOfficeService officeService, IPatternService patternService, ICountryService countryService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.employeeTransferFuturePlanService = employeeTransferFuturePlanService;
            this.appointmentNatureService = appointmentNatureService;
            this.appointmentCategoryService = appointmentCategoryService;
            this.officeService = officeService;
            this.patternService = patternService;
            this.countryService = countryService;
    

        }

        [HttpGet]
        [Route("get-transfer-future-plans")]
        public IHttpActionResult GetEmployeeTransferFuturePlans(string pNo)
        {
            
            List<EmployeeTransferFuturePlanModel> models = employeeTransferFuturePlanService.GetEmployeeTransferFuturePlans(pNo);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_TRANSFER_FUTURE_PLANS);
            return Ok(new ResponseMessage<List<EmployeeTransferFuturePlanModel>>()
            {
                Result = models,
                Permission = permission
                
            });
        }

        [HttpGet]
        [Route("get-transfer-future-plan")]
        public async Task<IHttpActionResult> GetEmployeeTransferFuturePlan(int id)
        {
            EmployeeTransferFuturePlanViewModel vm = new EmployeeTransferFuturePlanViewModel();
            vm.TransferFuturePlan = await employeeTransferFuturePlanService.GetEmployeeTransferFuturePlan(id);
            vm.AptNats = await appointmentNatureService.GetNatureSelectList();
            vm.Offices = await officeService.GetParentOfficeSelectModel();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.Patterns = await patternService.GetPatternTypeSelectModels();

            if (vm.TransferFuturePlan.AptnatId != null )
            {
                vm.AptCats = await appointmentCategoryService.GetCategorySelectListByNature(vm.TransferFuturePlan.AptnatId??0);

            }



            return Ok(new ResponseMessage<EmployeeTransferFuturePlanViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-transfer-future-plan")]
        public async Task<IHttpActionResult> SaveEmployeeTransferFuturePlan([FromBody] EmployeeTransferFuturePlanModel model)
        {
            return Ok(new ResponseMessage<EmployeeTransferFuturePlanModel>
            {
                Result = await employeeTransferFuturePlanService.SaveEmployeeTransferFuturePlan(0, model)
            });
        }

        [HttpPut]
        [Route("update-transfer-future-plan/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeTransferFuturePlan(int id, [FromBody] EmployeeTransferFuturePlanModel model)
        {
            return Ok(new ResponseMessage<EmployeeTransferFuturePlanModel>
            {
                Result = await employeeTransferFuturePlanService.SaveEmployeeTransferFuturePlan(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-transfer-future-plan/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeTransferFuturePlan(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeTransferFuturePlanService.DeleteEmployeeTransferFuturePlan(id)
            });
        }
    }
}