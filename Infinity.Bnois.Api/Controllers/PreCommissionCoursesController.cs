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
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.PreCommissionCourses)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEES)]
    public class PreCommissionCoursesController : PermissionController
    {
        private readonly IPreCommissionCourseService preCommissionCourseService;
        private readonly ICountryService countryService;
        private readonly IPunishmentCategoryService punishmentCategoryService;
        private readonly IPunishmentSubCategoryService punishmentSubCategoryService;
        private readonly IPunishmentNatureService punishmentNatureService;
        private readonly IMedalService medalService;

        public PreCommissionCoursesController(IPreCommissionCourseService preCommissionCourseService,
            ICountryService countryService,
            IPunishmentCategoryService punishmentCategoryService,
            IPunishmentSubCategoryService punishmentSubCategoryService,
            IPunishmentNatureService punishmentNatureService,
            IMedalService medalService, IRoleFeatureService roleFeatureService
            ):base(roleFeatureService)
        {
            this.preCommissionCourseService = preCommissionCourseService;
            this.countryService = countryService;
            this.punishmentCategoryService = punishmentCategoryService;
            this.punishmentSubCategoryService = punishmentSubCategoryService;
            this.punishmentNatureService = punishmentNatureService;
            this.medalService = medalService;
        }
        [HttpGet]
        [Route("get-pre-commission-courses")]
        public IHttpActionResult GetPreCommissionCourses(int employeeId)
        {
            List<PreCommissionCourseModel> models = preCommissionCourseService.GetPreCommissionCourses(employeeId);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEES);
            return Ok(new ResponseMessage<List<PreCommissionCourseModel>>()
            {
                Result = models,
                Permission = permission
            });
        }

        [HttpGet]
        [Route("get-pre-commission-course")]
        public async Task<IHttpActionResult> GetPreCommissionCourse(int employeeId, int preCommissionCourseId)
        {
            PreCommissionCourseViewModel vm = new PreCommissionCourseViewModel();
            vm.PreCommissionCourse = await preCommissionCourseService.GetPreCommissionCourse(preCommissionCourseId);
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.Medals = await medalService.GetMedalSelectModels(Convert.ToInt16(MedalType.PreCommission));
            return Ok(new ResponseMessage<PreCommissionCourseViewModel>
            {
                Result = vm
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-pre-commission-course/{employeeId}")]
        public async Task<IHttpActionResult> SavePreCommissionCourse(int employeeId, [FromBody] PreCommissionCourseModel model)
        {
            model.EmployeeId = employeeId;
            return Ok(new ResponseMessage<PreCommissionCourseModel>
            {
                Result = await preCommissionCourseService.SavePreCommissionCourse(0, model)
            });
        }

        [HttpPut]
        [Route("update-pre-commission-course/{preCommissionCourseId}")]
        public async Task<IHttpActionResult> UpdatePreCommissionCourse(int preCommissionCourseId, [FromBody] PreCommissionCourseModel model)
        {
            return Ok(new ResponseMessage<PreCommissionCourseModel>
            {
                Result = await preCommissionCourseService.SavePreCommissionCourse(preCommissionCourseId, model)
            });
        }

        [HttpDelete]
        [Route("delete-pre-commission-course/{preCommissionCourseId}")]
        public async Task<IHttpActionResult> DeletePreCommissionCourse(int preCommissionCourseId)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await preCommissionCourseService.DeletePreCommissionCourse(preCommissionCourseId)
            });
        }



    }
}
