using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeHajjDetails)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_HAJJ_DETAILS)]
    public class EmployeeHajjDetailController: PermissionController
    {
        private readonly IEmployeeHajjDetaitService _employeeHajjDetaitService;
        public EmployeeHajjDetailController(IEmployeeHajjDetaitService employeeHajjDetaitService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            _employeeHajjDetaitService = employeeHajjDetaitService;
        }
        [HttpGet]
        [Route("get-employee-hajj-details")]
        public IHttpActionResult GetEmployeeHajjDetails(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeHajjDetailModel> models = _employeeHajjDetaitService.GetEmployeeHajjDetails(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_HAJJ_DETAILS);
            return Ok(new ResponseMessage<List<EmployeeHajjDetailModel>>()
            {
                Result = models,
                Total = total,
                Permission = permission
            });
        }

        [HttpGet]
        [Route("get-employee-hajj-details-by-pno")]
        public IHttpActionResult GetEmployeeHajjDetailsByPno(string PNo)
        {
            return Ok(new ResponseMessage<List<EmployeeHajjDetailModel>>()
            {
                Result = _employeeHajjDetaitService.GetEmployeeHajjDetailsByPno(PNo)
        });
        }
        [HttpGet]
        [Route("get-employeeHajj-detail")]
        public async Task<IHttpActionResult> getEmployeeHajjDetail(int id)
        {
            return Ok(new ResponseMessage<EmployeeHajjDetailModel>
            {
                Result =await _employeeHajjDetaitService.getEmployeeHajjDetail(id)
            });
        }
        [HttpPost]
        [ModelValidation] 
        [Route("save-employee-hajj-detail")]
        public async Task<IHttpActionResult> saveEmployeeHajjDetail([FromBody] EmployeeHajjDetailModel model)
        {
            return Ok(new ResponseMessage<EmployeeHajjDetailModel>
            {
                Result = await _employeeHajjDetaitService.SaveEmployeeHajjDetail(0, model)
            });
        }
        [HttpPut]
        [Route("update-employee-hajj-detail/{id}")]
        public async Task<IHttpActionResult> updateEmployeeHajjDetail(int id, [FromBody] EmployeeHajjDetailModel model)
        {
            return Ok(new ResponseMessage<EmployeeHajjDetailModel>
            {
                Result = await _employeeHajjDetaitService.SaveEmployeeHajjDetail(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-employee-hajj-detail/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeHajjDetail(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await _employeeHajjDetaitService.DeleteEmployeeHajjDetail(id)
            });
        }

    }
}
