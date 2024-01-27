using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeSports)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public  class EmployeeSportsController:PermissionController
    {
        private readonly IEmployeeSportService employeeSportService;
        private readonly ISportService sportService;
        public EmployeeSportsController(IEmployeeSportService employeeSportService, ISportService sportService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.employeeSportService = employeeSportService;
            this.sportService = sportService;
        }

        //[HttpGet]
        //[Route("get-employee-sports")]
        //public async Task<IHttpActionResult> GetSportsAndHobby(int employeeId)
        //{
        //    EmployeeSportModel model = await employeeSportService.GetEmployeeSport(employeeId);
        //    return Ok(new ResponseMessage<EmployeeSportModel>()
        //    {
        //        Result = model
        //    });
        //}

        //[HttpPut]
        //[ModelValidation]
        //[Route("update-employee-sport/{employeeId}")]
        //
        //public async Task<IHttpActionResult> UpdateEmployeeSport(int employeeId, [FromBody] EmployeeSportModel model)
        //{
        //    model.CreatedBy = base.UserId;
        //    return Ok(new ResponseMessage<EmployeeSportModel>()
        //    {
        //        Result = await employeeSportService.SaveEmployeeSport(employeeId, model)
        //    });
        //}


        [HttpGet]
        [Route("get-employee-sports")]
        public IHttpActionResult GetEmployeeSports(int employeeId)
        {
            List<EmployeeSportModel> models = employeeSportService.GetEmployeeSports(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<EmployeeSportModel>>()
            {
                Result = models,
                Permission = permission
            });

        }
        [HttpGet]
        [Route("get-employee-sport")]
        public async Task<IHttpActionResult> GetEmployeeSport(int employeeId, int employeeSportId)
        {
            EmployeeSportViewModel vm = new EmployeeSportViewModel();
            vm.Sports = await sportService.GetSportsSelectModels();
            vm.EmployeeSport = await employeeSportService.GetEmployeeSport(employeeSportId);
            return Ok(new ResponseMessage<EmployeeSportViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-employee-sport/{employeeId}")]
        public async Task<IHttpActionResult> SaveEmployeeSport(int employeeId, [FromBody] EmployeeSportModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<EmployeeSportModel>()
            {
                Result = await employeeSportService.SaveEmployeeSport(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-employee-sport/{employeeSportId}")]
        public async Task<IHttpActionResult> UpdateEmployeeSport(int employeeSportId, [FromBody] EmployeeSportModel model)
        {
            return Ok(new ResponseMessage<EmployeeSportModel>()
            {
                Result = await employeeSportService.SaveEmployeeSport(employeeSportId, model)
            });
        }


        [HttpDelete]
        [Route("delete-employee-sport/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeSport(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeSportService.DeleteEmployeeSport(id)
            });
        }
    }
}
