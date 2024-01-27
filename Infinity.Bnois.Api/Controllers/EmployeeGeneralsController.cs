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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.EmployeeGenerals)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class EmployeeGeneralsController : BaseController
    {
        private readonly IEmployeeGeneralService employeeGeneralService;
        private readonly ICategoryService categoryService;
        private readonly ISubCategoryService subCategoryService;
        private readonly ICommissionTypeService commissionTypeService;
        private readonly IBranchService branchService;
        private readonly ISubBranchService subBranchService;
        private readonly ISubjectService subjectService;
        private readonly INationalityService nationalityService;
        private readonly IMaritalTypeService maritalTypeService;
        private readonly IReligionService religionService;
        private readonly IReligionCastService religionCastService;
        private readonly IOfficerStreamService officerStreamService;
        private readonly IEmployeeService employeeService;
     
        public EmployeeGeneralsController(IEmployeeService employeeService, IEmployeeGeneralService employeeGeneralService,
            ICategoryService categoryService,
            ISubCategoryService subCategoryService,
            ICommissionTypeService commissionTypeService,
            IBranchService branchService,
            ISubBranchService subBranchService,
            ISubjectService subjectService,
            INationalityService nationalityService,
            IMaritalTypeService maritalTypeService,
            IReligionService religionService,
            IReligionCastService religionCastService,
            IOfficerStreamService officerStreamService)
        {
            this.employeeGeneralService = employeeGeneralService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.commissionTypeService = commissionTypeService;
            this.branchService = branchService;
            this.subBranchService = subBranchService;
            this.subjectService = subjectService;
            this.nationalityService = nationalityService;
            this.maritalTypeService = maritalTypeService;
            this.religionService = religionService;
            this.religionCastService = religionCastService;
            this.officerStreamService = officerStreamService;
            this.employeeService = employeeService;          
        }

        [HttpGet]
        [Route("get-employee-generals")]

        public async Task<IHttpActionResult> GetEmployeeGenerals(int employeeId)
        {
            EmployeeGeneralModel model = await employeeGeneralService.GetEmployeeGenerals(employeeId);
            return Ok(new ResponseMessage<EmployeeGeneralModel>()
            {
                Result = model
            });
        }



        [HttpGet]
        [Route("get-employee-general")]
        public async Task<IHttpActionResult> GetEmployeeGeneral(int employeeId)
        {
            EmployeeGeneralViewModel vm = new EmployeeGeneralViewModel();
            vm.EmployeeGeneral = await employeeGeneralService.GetEmployeeGenerals(employeeId);
            vm.Employee = await employeeService.GetEmployee(employeeId);
            if (vm.Employee.Rank.ShortName.Equals("Lt"))
            {
                if (vm.EmployeeGeneral.EmployeeGeneralId <= 0)
                {
                    vm.EmployeeGeneral.IsShowLieutenantDate = true;
                }
            }

            if (vm.EmployeeGeneral.EmployeeGeneralId > 0)
            {
                vm.SubCategories = await subCategoryService.GetSubCategorySelectModelsByCategory(vm.EmployeeGeneral.CategoryId);
                vm.ReligionCasts = await religionCastService.GetReligionCastSelectModels();
            }   
            vm.Categories = await categoryService.GetCategorySelectModels();
            vm.CommissionTypes = await commissionTypeService.GetCommissionTypeSelectModels();
            vm.Branches = await branchService.GetBranchSelectModels();
            vm.SubBranches = await subBranchService.GetSubBranchSelectModels();
            vm.Subjects = await subjectService.GetSubjectSelectModels();
            vm.Nationalities = await nationalityService.GetNationalitySelectModels();
            vm.MaritalTypes = await maritalTypeService.GetMaritalTypeSelectModels();
            vm.Religions = await religionService.GetReligionSelectModels();
            vm.OfficerStreams = await officerStreamService.GetOfficerStreamSelectModels();
            return Ok(new ResponseMessage<EmployeeGeneralViewModel>()
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-employee-general-by-pno")]

        public async Task<IHttpActionResult> GetEmployeeGeneralByPNo(string pno)
        {
            EmployeeGeneralModel model = await employeeGeneralService.GetEmployeeGeneralByPNo(pno);
            return Ok(new ResponseMessage<EmployeeGeneralModel>()
            {
                Result = model
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-employee-general/{employeeId}")]

        public async Task<IHttpActionResult> UpdateEmployeeGeneral(int employeeId, [FromBody] EmployeeGeneralModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<EmployeeGeneralModel>()
            {
                Result = await employeeGeneralService.SaveEmployeeGeneral(employeeId, model)
            });
        }

        [HttpGet]
        [Route("get-sub-category")]

        public async Task<IHttpActionResult> GetSubcategory(int categoryId)
        {
            EmployeeGeneralViewModel vm = new EmployeeGeneralViewModel();
            vm.SubCategories = await subCategoryService.GetSubCategorySelectModelsByCategory(categoryId);
            return Ok(new ResponseMessage<EmployeeGeneralViewModel>()
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-religion-casts")]

        public async Task<IHttpActionResult> GetReligionCasts(int religionId)
        {
            EmployeeGeneralViewModel vm = new EmployeeGeneralViewModel();
            vm.ReligionCasts = await religionCastService.GetReligionCasts(religionId);
            return Ok(new ResponseMessage<EmployeeGeneralViewModel>()
            {
                Result = vm
            });
        }
    }
}
