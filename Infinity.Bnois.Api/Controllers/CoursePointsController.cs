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
    [RoutePrefix(BnoisRoutePrefix.CoursePoints)]
    [EnableCors("*", "*", "*")]
    public class CoursePointsController : BaseController
    {
        private readonly ICoursePointService coursePointService;
        private readonly ICourseCategoryService courseCategoryService;
        public CoursePointsController(ICoursePointService coursePointService, ICourseCategoryService courseCategoryService)
        {
            this.coursePointService = coursePointService;
            this.courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        [Route("get-course-points")]
        public IHttpActionResult GetCoursePoints(int id)
        {
            List<CoursePointModel> models = coursePointService.GetCoursePoints(id);
            return Ok(new ResponseMessage<List<CoursePointModel>>()
            {
                Result = models
            });

        }
        [HttpGet]
        [Route("get-course-point")]
        public async Task<IHttpActionResult> GetCoursePoint(int traceSettingId, int coursePointId)
        {
            CoursePointViewModel vm = new CoursePointViewModel();
            vm.CoursePoint = await coursePointService.GetCoursePoint(coursePointId);
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModelsByTrace();
            return Ok(new ResponseMessage<CoursePointViewModel>()
            {
                Result = vm
            });
        }
        [HttpPost]
        [ModelValidation]
        [Route("save-course-point/{traceSettingId}")]
        public async Task<IHttpActionResult> SaveCoursePoint(int traceSettingId, [FromBody] CoursePointModel model)
        {
            model.TraceSettingId = traceSettingId;
            return Ok(new ResponseMessage<CoursePointModel>()
            {
                Result = await coursePointService.SaveCoursePoint(0, model)
            });
        }
        [HttpPut]
        [ModelValidation]
        [Route("update-course-point/{coursePointId}")]
        public async Task<IHttpActionResult> UpdateCoursePoint(int coursePointId, [FromBody] CoursePointModel model)
        {
            return Ok(new ResponseMessage<CoursePointModel>()
            {
                Result = await coursePointService.SaveCoursePoint(coursePointId, model)
            });
        }
    }
}
