using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.RetiredEmployees)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.RETIRED_EMPLOYEE)]
    public class RetiredEmployeesController: PermissionController
    {
        private readonly IRetiredEmployeeService retiredEmployeeService;
        private readonly ICertificateService certificateService;
        private readonly ICountryService countryService;
       
       
        public RetiredEmployeesController(IRetiredEmployeeService retiredEmployeeService,
            ICertificateService certificateService,
            ICountryService countryService
            , IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.retiredEmployeeService = retiredEmployeeService;
            this.countryService = countryService;
            this.certificateService = certificateService;
          
          
        }



        [HttpGet]
        [Route("get-retired-employees")]
        public IHttpActionResult GetRetiredEmployees(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeModel> models = retiredEmployeeService.GetRetiredEmployees(ps, pn, qs, out total);
//            RoleFeature permission = base.GetFeature(MASTER_SETUP.RETIRED_EMPLOYEE);
            return Ok(new ResponseMessage<List<EmployeeModel>>()
            {
                Result = models,
                Total = total
            });
        }



        [HttpGet]
        [Route("get-retired-employee")]
      
        public async Task<IHttpActionResult> GetRetiredEmployee(int employeeId)
        {
            RetiredEmployeeViewModel vm = new RetiredEmployeeViewModel();
            vm.RetiredEmployee = await retiredEmployeeService.GetRetiredEmployee(employeeId);
           
            vm.Certificates = await certificateService.GetCertificateSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
          
            return Ok(new ResponseMessage<RetiredEmployeeViewModel>()
            {
                Result = vm
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-retired-employee/{employeeId}")]
      
        public async Task<IHttpActionResult> UpdateRetiredEmployee(int employeeId,[FromBody] RetiredEmployeeModel model)
        {
           
            return Ok(new ResponseMessage<RetiredEmployeeModel>()
            {
                Result = await retiredEmployeeService.SaveRetiredEmployee(employeeId, model)
            });
        }

  

    }
}
