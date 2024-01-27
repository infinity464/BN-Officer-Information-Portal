

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

    [RoutePrefix(BnoisRoutePrefix.EmployeeSecurityClearances)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_SECURTIY_CLEARANCES)]

    public class EmployeeSecurityClearancesController : PermissionController
    {
        private readonly IEmployeeSecurityClearanceService employeeSecurityClearanceService;
        private readonly ISecurityClearanceReasonService securityClearanceReasonService;

        public EmployeeSecurityClearancesController( IEmployeeSecurityClearanceService employeeSecurityClearanceService, 
            ISecurityClearanceReasonService securityClearanceReasonService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeeSecurityClearanceService = employeeSecurityClearanceService;
            this.securityClearanceReasonService = securityClearanceReasonService;
        }

        [HttpGet]
        [Route("get-employee-security-clearances")]
        public IHttpActionResult GetEmployeeSecurityClearances(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeSecurityClearanceModel> models = employeeSecurityClearanceService.GetEmployeeSecurityClearances(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_SECURTIY_CLEARANCES);
            return Ok(new ResponseMessage<List<EmployeeSecurityClearanceModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }




        [HttpGet]
        [Route("get-employee-security-clearance")]
        public async Task<IHttpActionResult> GetEmployeeSecurityClearance(int id)
        {
            EmployeeSecurityClearanceViewModel vm = new EmployeeSecurityClearanceViewModel();
            vm.EmployeeSecurityClearance = await employeeSecurityClearanceService.GetEmployeeSecurityClearance(id);
            vm.SecurityClearanceReasons = await securityClearanceReasonService.GetSecurityClearanceReasonSelectModels();
            return Ok(new ResponseMessage<EmployeeSecurityClearanceViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-employee-security-clearance")]
        public async Task<IHttpActionResult> SaveEmployeeSecurityClearance([FromBody] EmployeeSecurityClearanceModel model)
        {
            return Ok(new ResponseMessage<EmployeeSecurityClearanceModel>
            {
                Result = await employeeSecurityClearanceService.SaveEmployeeSecurityClearance(0, model)
            });
        }

        [HttpPut]
        [Route("update-employee-security-clearance/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeSecurityClearance(int id, [FromBody] EmployeeSecurityClearanceModel model)
        {
            return Ok(new ResponseMessage<EmployeeSecurityClearanceModel>
            {
                Result = await employeeSecurityClearanceService.SaveEmployeeSecurityClearance(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-security-clearance/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeSecurityClearance(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeSecurityClearanceService.DeleteEmployeeSecurityClearance(id)
            });
        }

    }
}