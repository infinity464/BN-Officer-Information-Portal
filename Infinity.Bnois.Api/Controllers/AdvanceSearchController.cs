using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.AdvanceSearch)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.ADVANCE_SEARCH)]

    public class AdvanceSearchController : BaseController
    {
        private readonly IAdvanceSearchService advanceSearchService;
        private readonly IZoneService zoneService;
        private readonly ITransferService transferService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly IBatchService batchService;
        private readonly IBranchService branchService;
        private readonly ISubBranchService subBranchService;
        private readonly IBloodGroupService bloodGroupService;
        private readonly IExaminationService examinationService;
        private readonly IResultService resultService;
        private readonly IInstituteService instituteService;
        private readonly ICommissionTypeService commissionTypeService;
        private readonly  IEmployeeService employeeService;
        private readonly  IDistrictService districtService;
        private readonly  IMedicalCategoryService medicalCategoryService;
        private readonly  IRankService rankService;
        private readonly  ISubjectService subjectService;
        private readonly  ICountryService countryService;
        private readonly  IVisitCategoryService visitCategoryService;
        private readonly  IExecutionRemarkService executionRemarkService;
        private readonly  ICategoryService categoryService;
        private readonly  ISubCategoryService subCategoryService;
        private readonly IReligionService religionService;
        private readonly IReligionCastService religionCastService;
        private readonly IOprOccasionService oprOccasionService;
        private readonly IServiceExamCategoryService serviceExamCategoryService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly ITrainingInstituteService trainingInstituteService;
        private readonly ICourseService courseService;
        private readonly IEducationService educationService;
        private readonly IPunishmentCategoryService punishmentCategory;
        private readonly IPunishmentSubCategoryService punishmentSubCategoryService;
        private readonly IMedalService medalService;
        private readonly IAwardService awardService;
        private readonly IPublicationCategoryService publicationCategoryService;
        private readonly IPublicationService publicationService;
        private readonly ICommendationService commendationService;
        private readonly ISecurityClearanceReasonService securityClearanceReasonService;
        private readonly IVisitSubCategoryService visitSubCategoryService;
        private readonly IAppointmentCategoryService appointmentCategoryService;



        public AdvanceSearchController(IAdvanceSearchService advanceSearchService, IZoneService zoneService,ITransferService transferService, IOfficeService officeService, IOfficeAppointmentService officeAppointmentService,
            IBatchService batchService, IBranchService branchService, ISubBranchService subBranchService, IBloodGroupService bloodGroupService,
            IExaminationService examinationService, IResultService resultService, IInstituteService instituteService, ICommissionTypeService commissionTypeService,
            IEmployeeService employeeService, IDistrictService districtService, IMedicalCategoryService medicalCategoryService,  IRankService rankService, ISubjectService subjectService,
            ICountryService countryService, IVisitCategoryService visitCategoryService, IExecutionRemarkService executionRemarkService, ICategoryService categoryService, ISubCategoryService subCategoryService,
            IReligionService religionService, IReligionCastService religionCastService, IOprOccasionService oprOccasionService, IServiceExamCategoryService serviceExamCategoryService,
            ICourseCategoryService courseCategoryService, ICourseSubCategoryService courseSubCategoryService, ITrainingInstituteService trainingInstituteService, ICourseService courseService,IEducationService educationService,
                IPunishmentCategoryService punishmentCategory, IPunishmentSubCategoryService punishmentSubCategoryService, IMedalService medalService, IAwardService awardService, IPublicationCategoryService publicationCategoryService,
            IPublicationService publicationService, ICommendationService commendationService, ISecurityClearanceReasonService securityClearanceReasonService, IVisitSubCategoryService visitSubCategoryService, IAppointmentCategoryService appointmentCategoryService)
        {
            this.advanceSearchService = advanceSearchService;
            this.zoneService = zoneService;
            this.transferService = transferService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;
            this.batchService = batchService;
            this.branchService = branchService;
            this.subBranchService = subBranchService;
            this.bloodGroupService = bloodGroupService;
            this.examinationService = examinationService;
            this.resultService = resultService;
            this.instituteService = instituteService;
            this.commissionTypeService = commissionTypeService;
            this.employeeService = employeeService;
            this.districtService = districtService;
            this.medicalCategoryService = medicalCategoryService;
            this.rankService = rankService;
            this.subjectService = subjectService;
            this.countryService = countryService;
            this.visitCategoryService = visitCategoryService;
            this.executionRemarkService = executionRemarkService;
            this.categoryService = categoryService;
            this.subCategoryService = subCategoryService;
            this.religionCastService = religionCastService;
            this.religionService = religionService;
            this.oprOccasionService = oprOccasionService;
            this.serviceExamCategoryService = serviceExamCategoryService;
            this.courseCategoryService = courseCategoryService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.trainingInstituteService = trainingInstituteService;
            this.courseService = courseService;
            this.educationService = educationService;
            this.punishmentCategory = punishmentCategory;
            this.punishmentSubCategoryService = punishmentSubCategoryService;
            this.awardService = awardService;
            this.medalService = medalService;
            this.publicationService = publicationService;
            this.publicationCategoryService = publicationCategoryService;
            this.commendationService = commendationService;
            this.securityClearanceReasonService = securityClearanceReasonService;
            this.visitSubCategoryService = visitSubCategoryService;
            this.appointmentCategoryService = appointmentCategoryService;
        }

        [HttpGet]
        [Route("get-advance-search-select-models")]
        public async Task<IHttpActionResult> GetAdvanceSearchSelectModels()
        {
	        AdvanceSearchViewModel vm = new AdvanceSearchViewModel();

	        vm.ColumnFilters = advanceSearchService.GetColumnFilterSelectModels();
	        vm.ColumnDisplays = await advanceSearchService.GetColumnDisplaySelectModels();
	        vm.Areas = await zoneService.GetZoneSelectModels();
	        vm.AreaTypes =  transferService.GetTransferTypeSelectModels();
	        vm.AdminAuthorities = await officeService.GetAdminAuthoritySelectModel();
	        vm.Batches = await batchService.GetBatchSelectModels();
	        vm.Branches = await branchService.GetBranchSelectModels();
	        vm.SubBranches = await subBranchService.GetSubBranchSelectModels();
	        vm.BloodGroups = await bloodGroupService.GetBloodGroupSelectModels();
	        vm.Exams = await examinationService.GetExaminationSelectModels();
	        vm.Results = await resultService.ResultSelectModels();
	        vm.Institutes = await instituteService.GetInstitutesSelectModels();
	        vm.CommissionTypes = await commissionTypeService.GetCommissionTypeSelectModels();
            vm.CurrentStatus = await employeeService.GetEmployeeStatusSelectModels();
            vm.Districts = await districtService.GetDistrictSelectModels();
            vm.MedicalCategories = await medicalCategoryService.GetMedicalCategorySelectModels();
            vm.Ranks = await rankService.GetRankSelectModels();
            vm.Subjects = await subjectService.GetSubjectSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.VisitCategories = await visitCategoryService.GetVisitCategorySelectModels();
            vm.PhysicalStructures =  officeService.GetShipTypeSelectModels();
            vm.PromotionStatus = await executionRemarkService.GetExecutionRemarkSelectModels(1);
            vm.Religions = await religionService.GetReligionSelectModels();
            vm.ReligionCasts = await religionCastService.GetReligionCastSelectModels();
            vm.Categories = await categoryService.GetCategorySelectModels();
            vm.SubCategories = await subCategoryService.GetSubCategorySelectModels();
            vm.OfficerServices = await employeeService.GetOfficerTypeSelectModels();
            vm.Occasions = await oprOccasionService.GetOprOccasionSelectModels();
            vm.ServiceExamCategories = await serviceExamCategoryService.GetServiceExamCategorySelectModels();
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels();
            vm.TrainingInstitutes = await trainingInstituteService.GetTrainingInstituteSelectModels();
            vm.Courses = await courseService.GetCourseSelectModels();
            vm.PassingYears =  educationService.GetYearSelectModel();
            vm.TransferAreas = transferService.GetTransferForSelectModels();
            vm.Offices = await officeService.GetParentOfficeSelectModel();
            vm.OfficeAppointments = await officeAppointmentService.GetOfficeAppointmentSelectModels();
            vm.PunishmentCategories = await punishmentCategory.GetPunishmentCategorySelectModels();
            vm.PunishmentSubCategories = await punishmentSubCategoryService.GetPunishmentSubCategorySelectModels();
            vm.Awards = await awardService.GetAwardSelectModels();
            vm.Medals = await medalService.GetMedalSelectModels();
            vm.Publications = await publicationService.GetPublicationSelectModels();
            vm.PublicationCategories = await publicationCategoryService.GetPublicationCategorySelectModels();
            vm.Patterns = await officeService.GetMinistryOfficeSelectModel();
            vm.Achievements = await commendationService.GetCommendationAppreciationSelectModels();
            vm.Clearances = await securityClearanceReasonService.GetSecurityClearanceReasonSelectModels();
            vm.Ships = await officeService.GetShipOfficeSelectModels();
            vm.VisitSubCategories = await visitSubCategoryService.GetVisitSubCategorySelectModels();
            vm.AppointmentCategories = await appointmentCategoryService.GetCategorySelectModel();


            return Ok(new ResponseMessage<AdvanceSearchViewModel>()
	        {
		        Result = vm

			});
		}



        [HttpGet]
        [ModelValidation]
        [Route("search-officers-result")]
        public  IHttpActionResult SearchOfficersResult()
        {
          
            return Ok(new ResponseMessage<IEnumerable>
            {
                Result = advanceSearchService.SearchOfficersResult(base.UserId)
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("search-officers")]
        public IHttpActionResult SearchOfficers([FromBody] AdvanceSearchModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = advanceSearchService.SearchOfficers(model, base.UserId)
            });
        }

     



        [HttpPost]
		[Route("save-checked-column")]
		public  IHttpActionResult SaveCheckedColumn(bool check, string value )
		{
		  bool result=	advanceSearchService.SaveCheckedValue(check, value, base.UserId);

			return Ok(new ResponseMessage<bool>()
			{
				Result = result

			});
		}




		[HttpDelete]
		[Route("delete-checked-column")]
		public  IHttpActionResult DeleteCheckedColumn()
		{
		  bool result=	advanceSearchService.DeleteCheckedColumn(base.UserId);

			return Ok(new ResponseMessage<bool>()
			{
				Result = result

			});
		}



	}
}
