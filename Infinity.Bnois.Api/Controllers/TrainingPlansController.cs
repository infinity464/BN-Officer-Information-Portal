
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
    [RoutePrefix(BnoisRoutePrefix.TrainingPlans)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.TRAINING_PLANS)]

    public class TrainingPlansController : PermissionController
    {
        private readonly ITrainingPlanService trainingPlanService;
        private readonly ICourseService courseService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly ITrainingInstituteService trainingInstituteService;
        private readonly ICountryService countryService;
        private readonly IBranchService branchService;
        private readonly IRankService rankService;

        
        public TrainingPlansController(ITrainingPlanService trainingPlanService, ICourseService courseService, 
            ICourseCategoryService courseCategoryService, ICourseSubCategoryService courseSubCategoryService,
            ITrainingInstituteService trainingInstituteService, ICountryService countryService,
            IBranchService branchService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.trainingPlanService = trainingPlanService;
            this.courseService = courseService;
            this.courseCategoryService = courseCategoryService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.trainingInstituteService = trainingInstituteService;
            this.countryService = countryService;
            this.branchService = branchService;
            this.rankService = rankService;

        }

        [HttpGet]
        [Route("get-training-plans")]
        public IHttpActionResult GetTrainingPlans(int ps, int pn, string qs)
        {
            int total = 0;
            List<TrainingPlanModel> models = trainingPlanService.GetTrainingPlans(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.TRAINING_PLANS);
            return Ok(new ResponseMessage<List<TrainingPlanModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-training-plan")]
        public async Task<IHttpActionResult> GetTrainingPlan(int id)
        {
            TrainingPlanViewModel vm = new TrainingPlanViewModel();
            vm.TrainingPlan = await trainingPlanService.GetTrainingPlan(id);
            vm.CountryTypes =  trainingPlanService.GetCountryTypeSelectModels();

            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            if (vm.TrainingPlan !=null)
            {
                vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels(vm.TrainingPlan.CourseCategoryId);
                vm.Courses = await courseService.GetCourseSelectModels(vm.TrainingPlan.CourseCategoryId, vm.TrainingPlan.CourseSubCategoryId,vm.TrainingPlan.CountryId);
                vm.TrainingInstitutes = await trainingInstituteService.GetTrainingInstituteSelectModels(vm.TrainingPlan.CourseCategoryId);
            }
       
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.BranchList= await branchService.GetBranchSelectModels();
            vm.RankList= await rankService.GetRankSelectModels();

            return Ok(new ResponseMessage<TrainingPlanViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-courses-by-category-sub-category")]
        public async Task<IHttpActionResult> GetCoursesByCatAndSubCat(int catId,int subCatId,int countryId)
        {
            TrainingPlanViewModel vm = new TrainingPlanViewModel();
            vm.Courses = await courseService.GetCourseSelectModels(catId,subCatId,countryId);
            return Ok(new ResponseMessage<TrainingPlanViewModel>
            {
                Result = vm
            });
        }



      


        [HttpGet]
        [Route("get-training-plan-select-model")]
        public async Task<IHttpActionResult> GetTrainingPlanSelectModel(int trainingPlanId)
        {

            List<SelectModel> selectModels = await trainingPlanService.GetTrainingPlanSelectModels(trainingPlanId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-training-plan")]
        public async Task<IHttpActionResult> SaveTrainingPlan([FromBody] TrainingPlanModel model)
        {
            return Ok(new ResponseMessage<TrainingPlanModel>
            {
                Result = await trainingPlanService.SaveTrainingPlan(0, model)
            });
        }

        [HttpPut]
        [Route("update-training-plan/{id}")]
        public async Task<IHttpActionResult> UpdateTrainingPlan(int id, [FromBody] TrainingPlanModel model)
        {
            return Ok(new ResponseMessage<TrainingPlanModel>
            {
                Result = await trainingPlanService.SaveTrainingPlan(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-training-plan/{id}")]
        public async Task<IHttpActionResult> DeleteTrainingPlan(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await trainingPlanService.DeleteTrainingPlan(id)
            });
        }
    }
}