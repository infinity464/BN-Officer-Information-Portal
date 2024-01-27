
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
    [RoutePrefix(BnoisRoutePrefix.EmployeeCourseFuturePlans)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_COURSE_FUTURE_PLANS)]

    public class EmployeeCourseFuturePlansController : PermissionController
    {
        private readonly IEmployeeCourseFuturePlanService employeeCourseFuturePlanService;
        private readonly ICourseService courseService;
        private readonly ICourseCategoryService courseCategoryService;
        private readonly ICourseSubCategoryService courseSubCategoryService;
 

        
        public EmployeeCourseFuturePlansController(IEmployeeCourseFuturePlanService employeeCourseFuturePlanService,
            ICourseService courseService, ICourseSubCategoryService courseSubCategoryService,
            ICourseCategoryService courseCategoryService, IRoleFeatureService roleFeatureService):base(roleFeatureService)
        {
            this.employeeCourseFuturePlanService = employeeCourseFuturePlanService;
            this.courseService = courseService;
            this.courseSubCategoryService = courseSubCategoryService;
            this.courseCategoryService = courseCategoryService;
    

        }

        [HttpGet]
        [Route("get-course-future-plans")]
        public IHttpActionResult GetEmployeeCourseFuturePlans( string pNo)
        {

            List<EmployeeCourseFuturePlanModel> models = employeeCourseFuturePlanService.GetEmployeeCourseFuturePlans(pNo);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_COURSE_FUTURE_PLANS);
            return Ok(new ResponseMessage<List<EmployeeCourseFuturePlanModel>>()
            {
                Result = models,
                Permission = permission

            });
        }

        [HttpGet]
        [Route("get-course-future-plan")]
        public async Task<IHttpActionResult> GetEmployeeCourseFuturePlan(int id)
        {
            EmployeeCourseFuturePlanViewModel vm = new EmployeeCourseFuturePlanViewModel();
            vm.CourseFuturePlan = await employeeCourseFuturePlanService.GetEmployeeCourseFuturePlan(id);
            vm.CourseCategories = await courseCategoryService.GetCourseCategorySelectModels();
            if (vm.CourseFuturePlan.CourseCategoryId != null && vm.CourseFuturePlan.CourseSubCategoryId != null)
            {
                vm.CourseSubCategories = await courseSubCategoryService.GetCourseSubCategorySelectModels(vm.CourseFuturePlan.CourseCategoryId);
                vm.Courses = await courseService.GetCourseBySubCategory(vm.CourseFuturePlan.CourseSubCategoryId??0);

            }



            return Ok(new ResponseMessage<EmployeeCourseFuturePlanViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-course-future-plan")]
        public async Task<IHttpActionResult> SaveEmployeeCourseFuturePlan([FromBody] EmployeeCourseFuturePlanModel model)
        {
            return Ok(new ResponseMessage<EmployeeCourseFuturePlanModel>
            {
                Result = await employeeCourseFuturePlanService.SaveEmployeeCourseFuturePlan(0, model)
            });
        }

        [HttpPut]
        [Route("update-course-future-plan/{id}")]
        public async Task<IHttpActionResult> UpdateEmployeeCourseFuturePlan(int id, [FromBody] EmployeeCourseFuturePlanModel model)
        {
            return Ok(new ResponseMessage<EmployeeCourseFuturePlanModel>
            {
                Result = await employeeCourseFuturePlanService.SaveEmployeeCourseFuturePlan(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-course-future-plan/{id}")]
        public async Task<IHttpActionResult> DeleteEmployeeCourseFuturePlan(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await employeeCourseFuturePlanService.DeleteEmployeeCourseFuturePlan(id)
            });
        }
    }
}