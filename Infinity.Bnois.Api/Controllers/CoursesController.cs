
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

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Courses)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COURSES)]

    public class CoursesController : PermissionController
    {
        private readonly ICourseService courseService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly ICountryService countryService;
        
        public CoursesController(ICourseService courseService, ICourseCategoryService courseCategoryService, 
            ICourseSubCategoryService courseSubCategoryService, ICountryService countryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.courseService = courseService;
            this.courseCategoryService = courseCategoryService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.countryService = countryService;
        }

        [HttpGet]
        [Route("get-courses")]
        public IHttpActionResult GetCourses(int ps, int pn, string qs)
        {
            int total = 0;
            List<CourseModel> models = courseService.GetCourses(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COURSES);
            return Ok(new ResponseMessage<List<CourseModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-course")]
        public async Task<IHttpActionResult> GetCourse(int id)
        {
            CourseViewModel vm = new CourseViewModel();
            vm.Course = await courseService.GetCourse(id);
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            if (vm.Course.CourseId > 0)
            {
                vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels(vm.Course.CourseCategoryId);
            }

            return Ok(new ResponseMessage<CourseViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-course-sub-categories-by-course")]
        public async Task<IHttpActionResult> GetCourseSubCategoriesByCourse(int id)
        {
            CourseViewModel vm = new CourseViewModel();
            vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels(id);
            return Ok(new ResponseMessage<CourseViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-course-by-sub-category")]
        public async Task<IHttpActionResult> GetCourseBySubCategory(int id)
        {
          
            List<SelectModel>  selectModels= await courseService.GetCourseBySubCategory(id);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }


        [HttpGet]
        [Route("get-course-by-category")]
        public async Task<IHttpActionResult> GetCourseByCategory(int id)
        {

            List<SelectModel> selectModels = await courseService.GetCourseByCategory(id);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-course")]
        public async Task<IHttpActionResult> SaveCourse([FromBody] CourseModel model)
        {
            return Ok(new ResponseMessage<CourseModel>
            {
                Result = await courseService.SaveCourse(0, model)
            });
        }

        [HttpPut]
        [Route("update-course/{id}")]
        public async Task<IHttpActionResult> UpdateCourse(int id, [FromBody] CourseModel model)
        {
            return Ok(new ResponseMessage<CourseModel>
            {
                Result = await courseService.SaveCourse(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-course/{id}")]
        public async Task<IHttpActionResult> DeleteCourse(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await courseService.DeleteCourse(id)
            });
        }
    }
}