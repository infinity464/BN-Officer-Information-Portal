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
    [RoutePrefix(BnoisRoutePrefix.Employees)]
    [EnableCors("*", "*", "*")]
//    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class EmployeesController : PermissionController
    {
        private readonly IEmployeeService employeeService;
        private readonly IBatchService batchService;
        private readonly IGenderService genderService;
        private readonly IRankCategoryService rankCategoryService;
        private readonly IRankService rankService;
        private readonly ICountryService countryService;
        private readonly IExecutionRemarkService executionRemarkService;
        public EmployeesController(IRankService rankService, IEmployeeService employeeService, IBatchService batchService,
            IGenderService genderService, IRankCategoryService rankCategoryService,
            ICountryService countryService, IRoleFeatureService roleFeatureService, IExecutionRemarkService executionRemarkService) : base(roleFeatureService)
        {
            this.employeeService = employeeService;
            this.batchService = batchService;
            this.genderService = genderService;
            this.rankCategoryService = rankCategoryService;
            this.countryService = countryService;
            this.rankService = rankService;
            this.executionRemarkService = executionRemarkService;
        }

        [HttpGet]
        [Route("get-employee")]
        public async Task<IHttpActionResult> GetEmployee(int employeeId)
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            if (employeeId > 0)
            {
                vm.Employee = await employeeService.GetEmployee(employeeId);

                if (vm.Employee.RankCategoryId > 0)
                {
                    vm.Ranks = await rankService.GetRankSelectModelsByRankCategory(vm.Employee.RankCategoryId);
                }


            }
            vm.ExecutionRemarks = await executionRemarkService.GetExecutionRemarkSelectModels(1);
            vm.Batches = await batchService.GetBatchSelectModels();
            vm.Genders = await genderService.GetGenderSelectModels();
            vm.RankCategories = await rankCategoryService.GetRankCategorySelectModels();
            vm.OfficerTypes = await employeeService.GetOfficerTypeSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.EmployeeStatuses = await employeeService.GetEmployeeStatusSelectModels();
            return Ok(new ResponseMessage<EmployeeViewModel>()
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-employeesByName")]
        public IHttpActionResult GetEmployeesByName(string qs)
        {
            return Ok(new ResponseMessage<List<object>>()
            {
                Result = employeeService.GetEmployeeByName(qs)
            });
        }
        [HttpGet]
        [Route("get-employeePno")]
        public IHttpActionResult GetLoginEmployeePno()
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = employeeService.GetLoginEmployeePno()
            });
        }
        [HttpGet]
        [Route("get-employees")]
        public IHttpActionResult GetEmployees(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeModel> models = employeeService.GetEmployees(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<EmployeeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-employee-by-pno")]
        public async Task<IHttpActionResult> GetEmployeeByPno()
        {
            return Ok(new ResponseMessage<EmployeeModel>() { Result = await employeeService.GetEmployeeByPO() });
        }

        [HttpGet]
        [Route("get-employee-by-pno1")]
        public async Task<IHttpActionResult> GetEmployeeByPno1(string pno)
        {
            return Ok(new ResponseMessage<EmployeeModel>() { Result = await employeeService.GetEmployeeByPO(pno) });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-employee")]
        public async Task<IHttpActionResult> SaveEmployee([FromBody] EmployeeModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<EmployeeModel>()
            {
                Result = await employeeService.SaveEmployee(0, model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee/{id}")]
        public async Task<IHttpActionResult> UpdateEmployee(int id, [FromBody] EmployeeModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<EmployeeModel>()
            {
                Result = await employeeService.SaveEmployee(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-employee/{id}")]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await employeeService.DeleteEmployee(id)
            });
        }
        [HttpGet]
        [Route("get-officer-types")]
        public async Task<IHttpActionResult> GetOfficerTypeSelectModels()
        {
            List<SelectModel> organizationSelectModels = await employeeService.GetOfficerTypeSelectModels();
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = organizationSelectModels,
            });
        }

        [HttpGet]
        [Route("get-employees-by-dollar-sign")]
        public IHttpActionResult GetEmployeesByDollarSign(int ps, int pn, string qs)
        {
            int total = 0;
            List<EmployeeModel> models = employeeService.GetEmployeesByDollarSign(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<EmployeeModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-employee-by-dollar-sign")]
        public async Task<IHttpActionResult> GetEmployeeByDollarSign(int employeeId)
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Employee = await employeeService.GetEmployee(employeeId);
            return Ok(new ResponseMessage<EmployeeViewModel>()
            {
                Result = vm
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee-dollar-sign")]
        public async Task<IHttpActionResult> UpdateEmployeeDollarSign([FromBody] EmployeeModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<EmployeeModel>()
            {
                Result = await employeeService.UpdateEmployeeDollarSign(model)
            });
        }

        [HttpDelete]
        [Route("delete-employee-dollar-sign")]
        public async Task<IHttpActionResult> DeleteEmployeeDollarSign(int  employeeId)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await employeeService.DeleteEmployeeDollarSign(employeeId)
            });
        }
    }
}
