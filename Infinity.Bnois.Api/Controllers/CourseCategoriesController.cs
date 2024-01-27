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
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Implementation;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.CourseCategories)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.COURSE_CATEGORIES)]

    public class CourseCategoriesController: PermissionController
    {
        private readonly ICourseCategoryService courseCategoryService;
        public CourseCategoriesController(ICourseCategoryService  courseCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.courseCategoryService = courseCategoryService;
        }
        [HttpGet]
        [Route("get-course-categories")]
        public IHttpActionResult GetCourseCategories(int ps, int pn, string qs)
        {
            int total = 0;
            List<CourseCategoryModel> courseCategories = courseCategoryService.GetCourseCategories(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.COURSE_CATEGORIES);
            return Ok(new ResponseMessage<List<CourseCategoryModel>>()
            {
                Result = courseCategories,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-course-category")]
        public async Task<IHttpActionResult> GetCourseCategory(int id)
        {
            CourseCategoryModel model = await courseCategoryService.GetCourseCategory(id);
            return Ok(new ResponseMessage<CourseCategoryModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-course-category")]
        public async Task<IHttpActionResult> SaveCourseCategory([FromBody] CourseCategoryModel model)
        {
            return Ok(new ResponseMessage<CourseCategoryModel>
            {
                Result = await courseCategoryService.SaveCourseCategory(0, model)
            });
        }

        [HttpPut]
        [Route("update-course-category/{id}")]
        public async Task<IHttpActionResult> UpdateCourseCategory(int id, [FromBody] CourseCategoryModel model)
        {
            return Ok(new ResponseMessage<CourseCategoryModel>
            {
                Result = await courseCategoryService.SaveCourseCategory(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-course-category/{id}")]
        public async Task<IHttpActionResult> DeleteCourseCategory(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await courseCategoryService.DeleteCourseCategory(id)
            });
        }
    }
}
