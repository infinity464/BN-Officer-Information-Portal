using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.BasicSearch)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.BASIC_SEARCH)]

    public class BasicSearchController : BaseController
    {
        private readonly IBasicSearchService basicSearchService; 
        private readonly IZoneService zoneService;
        private readonly IOfficeService officeService;
        private readonly IBranchService branchService;
        private readonly ISubBranchService subBranchService;
        private readonly IExamCategoryService examCategoryService;
        private readonly IResultTypeService resultTypeService;
        private readonly IInstituteService instituteService;
        private readonly ICommissionTypeService commissionTypeService;
        private readonly  IRankService rankService;
        private readonly  ICountryService countryService;

        private readonly IServiceExamCategoryService serviceExamCategoryService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly ITrainingInstituteService trainingInstituteService;
        private readonly ICourseService courseService;
        private readonly IEducationService educationService;


        public BasicSearchController(IBasicSearchService basicSearchService,IZoneService zoneService,IOfficeService officeService,
            IBranchService branchService, ISubBranchService subBranchService,  IExamCategoryService examCategoryService,
            IResultTypeService resultTypeService, IInstituteService instituteService, ICommissionTypeService commissionTypeService,
             IRankService rankService, ICountryService countryService, IServiceExamCategoryService serviceExamCategoryService,
            ICourseCategoryService courseCategoryService, ICourseSubCategoryService courseSubCategoryService, 
            ITrainingInstituteService trainingInstituteService, ICourseService courseService, IEducationService educationService)
        {
            this.basicSearchService = basicSearchService;
            this.zoneService = zoneService;
            this.officeService = officeService;
            this.branchService = branchService;
            this.subBranchService = subBranchService;
            this.examCategoryService = examCategoryService;
            this.resultTypeService = resultTypeService;
            this.instituteService = instituteService;
            this.commissionTypeService = commissionTypeService;
            this.rankService = rankService;
            this.countryService = countryService;
            this.serviceExamCategoryService = serviceExamCategoryService;
            this.courseCategoryService = courseCategoryService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.trainingInstituteService = trainingInstituteService;
            this.courseService = courseService;
            this.educationService = educationService;
        }


        [HttpGet]
        [Route("get-basic-search-select-models")]
        public async Task<IHttpActionResult> GetBasicSearchSelectModels()
        {
	        BasicSearchViewModel vm = new BasicSearchViewModel();

	        vm.ColumnFilters = basicSearchService.GetColumnFilterSelectModels();
	        vm.ColumnDisplays = await basicSearchService.GetColumnDisplaySelectModels();
	        vm.Areas = await zoneService.GetZoneSelectModels();
	        vm.AdminAuthorities = await officeService.GetAdminAuthoritySelectModel();
	        vm.Branches = await branchService.GetBranchSelectModels();
	        vm.SubBranches = await subBranchService.GetSubBranchSelectModels();
	        vm.Exams = await examCategoryService.GetExamCategorySelectModels();
	        vm.Results = await resultTypeService.GetResultTypeSelectModels();
	        vm.Institutes = await instituteService.GetInstitutesSelectModels();
	        vm.CommissionTypes = await commissionTypeService.GetCommissionTypeSelectModels();
            vm.Ranks = await rankService.GetRankSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.ServiceExamCategories = await serviceExamCategoryService.GetServiceExamCategorySelectModels();
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels();
            vm.TrainingInstitutes = await trainingInstituteService.GetTrainingInstituteSelectModels();
            vm.Courses = await courseService.GetCourseSelectModels();
            vm.PassingYears = educationService.GetYearSelectModel();
            return Ok(new ResponseMessage<BasicSearchViewModel>()
	        {
		        Result = vm
		});

		}




        [HttpPost]
        [ModelValidation]
        [Route("search-officers")]
        public IHttpActionResult SearchOfficers([FromBody] BasicSearchModel model)
        {
            return Ok(new ResponseMessage<BasicSearchModel>
            {
                Result = basicSearchService.SearchOfficers(model)
            });
        }


        [HttpPost]
        [Route("save-checked-column")]
        public IHttpActionResult SaveCheckedColumn(bool check, string value)
        {
            bool result = basicSearchService.SaveCheckedValue(check, value, base.UserId);

            return Ok(new ResponseMessage<bool>()
            {
                Result = result

            });
        }


        [HttpDelete]
        [Route("delete-checked-column")]
        public IHttpActionResult DeleteCheckedColumn()
        {
            bool result = basicSearchService.DeleteCheckedColumn();

            return Ok(new ResponseMessage<bool>()
            {
                Result = result

            });
        }


    }
}
