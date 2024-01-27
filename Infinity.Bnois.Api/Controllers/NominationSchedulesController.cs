
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
    [RoutePrefix(BnoisRoutePrefix.NominationSchedules)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.NOMINATION_SCHEDULES)]

    public class NominationSchedulesController : PermissionController
    {
        private readonly INominationScheduleService nominationScheduleService;
        private readonly ICountryService countryService;
        private readonly IVisitCategoryService visitCategoryService;
        private readonly IVisitSubCategoryService visitSubCategoryService;


        public NominationSchedulesController(INominationScheduleService nominationScheduleService, ICountryService countryService,
            IVisitCategoryService visitCategoryService, IVisitSubCategoryService visitSubCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)

        {
            this.nominationScheduleService = nominationScheduleService;
            this.countryService = countryService;
            this.visitCategoryService = visitCategoryService;
            this.visitSubCategoryService = visitSubCategoryService;
        }

        [HttpGet]
        [Route("get-nomination-schedules")]
        public IHttpActionResult GetNominationSchedules(int ps, int pn, string qs, int type)
        {
            int total = 0;
            List<NominationScheduleModel> models = nominationScheduleService.GetNominationSchedules(ps, pn, qs,type, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.NOMINATION_SCHEDULES);
            return Ok(new ResponseMessage<List<NominationScheduleModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-nomination-schedule")]
        public async Task<IHttpActionResult> GetNominationScheduleInfo(int id)
        {
            NominationScheduleViewModel vm = new NominationScheduleViewModel();
          
            vm.Countries = await countryService.GetCountriesTypeSelectModel();
            vm.NominationSchedule = new NominationScheduleModel();
            vm.NominationSchedule = await nominationScheduleService.GetNominationSchedule(id);
            vm.VisitCategories = await visitCategoryService.GetVisitCategorySelectModels();
            if (vm.NominationSchedule.VisitCategoryId > 0)
            {
                vm.VisitSubCategories = await visitSubCategoryService.GetVisitSubCategorySelectModelsByVisitCategory(vm.NominationSchedule.VisitCategoryId ?? 0);
            }
                
			return Ok(new ResponseMessage<NominationScheduleViewModel>
            {
                Result = vm
            });
        }



        [HttpPost]
        [ModelValidation]
        [Route("save-nomination-schedule")]
        public async Task<IHttpActionResult> SaveNominationSchedule([FromBody] NominationScheduleModel model)
        {
            return Ok(new ResponseMessage<NominationScheduleModel>
            {
                Result = await nominationScheduleService.SaveNominationSchedule(0, model)
            });
        }

        [HttpPost]
        [Route("update-nomination-schedule/{id}")]
        public async Task<IHttpActionResult> UpdateNominationSchedule(int id, [FromBody] NominationScheduleModel model)
        {
            return Ok(new ResponseMessage<NominationScheduleModel>
            {
                Result = await nominationScheduleService.SaveNominationSchedule(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-nomination-schedule/{id}")]
        public async Task<IHttpActionResult> DeleteNominationSchedule(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await nominationScheduleService.DeleteNominationSchedule(id)
            });
        }
    }
}