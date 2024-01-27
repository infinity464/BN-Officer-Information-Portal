

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
    [RoutePrefix(BnoisRoutePrefix.EmployeeServiceExtension)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SERVICE_EXTENSIONS)]

    public class EmployeeSeviceExtensionsController : PermissionController
    {
        private readonly IEmployeeServiceExtensionService employeeServiceExtensionService;

        public EmployeeSeviceExtensionsController(IEmployeeServiceExtensionService employeeServiceExtensionService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.employeeServiceExtensionService = employeeServiceExtensionService;
        }

        [HttpGet]
        [Route("get-employee-service-extensions")]
        public IHttpActionResult GetEmployeeSeviceExtensions(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeServiceExtensionModel> models = employeeServiceExtensionService.GetEmployeeServiceExtensions(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SERVICE_EXTENSIONS);
            return Ok(new ResponseMessage<List<EmployeeServiceExtensionModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-service-extension")]
        public async Task<IHttpActionResult> GetEmployeeSeviceExtension(int id)
        {
            EmployeeServiceExtensionModel model = await employeeServiceExtensionService.GetEmployeeServiceExtension(id);
            return Ok(new ResponseMessage<EmployeeServiceExtensionModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-employee-service-extension")]
        public async Task<IHttpActionResult> SaveEmployeeSeviceExtension([FromBody] EmployeeServiceExtensionModel model)
        {
            return Ok(new ResponseMessage<EmployeeServiceExtensionModel>
            {
                Result = await employeeServiceExtensionService.SaveEmployeeServiceExtension(0, model)
            });
        }



        [HttpPut]
        [Route("update-employee-service-extension/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeSeviceExtension(int id, [FromBody] EmployeeServiceExtensionModel model)
        {
            return Ok(new ResponseMessage<EmployeeServiceExtensionModel>
            {
                Result = await employeeServiceExtensionService.SaveEmployeeServiceExtension(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-employee-service-extension/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeSeviceExtension(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeServiceExtensionService.DeleteEmployeeServiceExtension(id)
            });
        }


    }
}