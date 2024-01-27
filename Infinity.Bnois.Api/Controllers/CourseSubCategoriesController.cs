
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
    [RoutePrefix(BnoisRoutePrefix.CourseSubCategories)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COURSE_SUB_CATEGORIES)]

    public class CourseSubCategoriesController : PermissionController
    {
        private readonly ICourseSubCategoryService courseSubCategoryService;
        private readonly ICourseCategoryService courseCategoryService;
        
        public CourseSubCategoriesController(ICourseSubCategoryService courseSubCategoryService, ICourseCategoryService courseCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.courseSubCategoryService = courseSubCategoryService;
            this.courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        [Route("get-course-sub-categories")]
        public IHttpActionResult GetCourseSubCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<CourseSubCategoryModel> models = courseSubCategoryService.GetCourseSubCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COURSE_SUB_CATEGORIES);
            return Ok(new ResponseMessage<List<CourseSubCategoryModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-course-sub-category")]
        public async Task<IHttpActionResult> GetCourseSubCategory(int id)
        {
            CourseSubCategoryViewModel vm = new CourseSubCategoryViewModel();
            vm.CourseSubCategory = await courseSubCategoryService.GetCourseSubCategory(id);
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            return Ok(new ResponseMessage<CourseSubCategoryViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-course-sub-category")]
        public async Task<IHttpActionResult> SaveCourseSubCategory([FromBody] CourseSubCategoryModel model)
        {
            return Ok(new ResponseMessage<CourseSubCategoryModel>
            {
                Result = await courseSubCategoryService.SaveCourseSubCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-course-sub-category/{id}")]
        public async Task<IHttpActionResult> UpdateCourseSubCategory(int id, [FromBody] CourseSubCategoryModel model)
        {
            return Ok(new ResponseMessage<CourseSubCategoryModel>
            {
                Result = await courseSubCategoryService.SaveCourseSubCategory(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-course-sub-category/{id}")]
        public async Task<IHttpActionResult> DeleteCourseSubCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await courseSubCategoryService.DeleteCourseSubCategory(id)
            });
        }
    }
}