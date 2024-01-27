using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Infinity.Ers.Applicant.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.BraCtryBraCtryCoursePoints)]
    [EnableCors("*", "*", "*")]
    public class BraCtryCoursePointsController : BaseController
    {
        private readonly IBraCtryCoursePointService braCtryCoursePointService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly IRankCategoryService rankCategoryService;
        private readonly IBranchService branchService;
        private readonly ICountryService countryService;
        public BraCtryCoursePointsController(IBraCtryCoursePointService braCtryCoursePointService,
            ICourseCategoryService courseCategoryService,
            ICourseSubCategoryService courseSubCategoryService,
            IRankCategoryService rankCategoryService,
            IBranchService branchService,
            ICountryService countryService
            )
        {
            this.braCtryCoursePointService = braCtryCoursePointService;
            this.courseCategoryService = courseCategoryService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.rankCategoryService = rankCategoryService;
            this.branchService = branchService;
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("get-bra-ctry-course-points")]
        public IHttpActionResult GetBraCtryCoursePoints(int id)
        {
            List<BraCtryCoursePointModel> models = braCtryCoursePointService.GetBraCtryCoursePoints(id);
            return Ok(new ResponseMessage<List<BraCtryCoursePointModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-bra-ctry-course-point")]
        public async Task<IHttpActionResult> GetBraCtryCoursePoint(int traceSettingId, int braCtryCoursePointId)
        {
            BraCtryCoursePointViewModel vm = new BraCtryCoursePointViewModel();
            vm.BraCtryCoursePoint = await braCtryCoursePointService.GetBraCtryCoursePoint(braCtryCoursePointId);
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModelsByTrace();
            if (braCtryCoursePointId>0)
            {
                vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels(vm.BraCtryCoursePoint.CourseCategoryId);
            }
            vm.RankCategories = await rankCategoryService.GetRankCategorySelectModels();
            vm.Branches = await branchService.GetBranchSelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();

            return Ok(new ResponseMessage<BraCtryCoursePointViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-bra-ctry-course-point/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveBraCtryCoursePoint(int traceSettingId, [FromBody] BraCtryCoursePointModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<BraCtryCoursePointModel>()
            {
                Result = await braCtryCoursePointService.SaveBraCtryCoursePoint(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-bra-ctry-course-point/{braCtryCoursePointId}")]
        public async Task<IHttpActionResult> UpdateBraCtryCoursePoint(int braCtryCoursePointId, [FromBody] BraCtryCoursePointModel model)
        {
            return Ok(new ResponseMessage<BraCtryCoursePointModel>()
            {
                Result = await braCtryCoursePointService.SaveBraCtryCoursePoint(braCtryCoursePointId, model)
            });
        }
    }
}
