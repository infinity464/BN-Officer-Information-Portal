
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
using Infinity.Bnois.Data;
using Infinity.Ers.ApplicationService;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Nominations)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.NOMINATIONS)]

    public class NominationsController : PermissionController
    {
        private readonly INominationService nominationService;
        private readonly ITrainingPlanService trainingPlanService;
        private readonly INominationScheduleService nominationScheduleService;
        private readonly IMissionAppointmentService missionAppointmentService;

 

        
        public NominationsController(INominationService nominationService, ITrainingPlanService trainingPlanService,
            IMissionAppointmentService missionAppointmentService,INominationScheduleService nominationScheduleService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.nominationService = nominationService;
            this.trainingPlanService = trainingPlanService;
            this.nominationScheduleService = nominationScheduleService;
            this.missionAppointmentService = missionAppointmentService;

        }

        [HttpGet]
        [Route("get-nominations")]
        public IHttpActionResult GetNominations(int ps, int pn, string qs,int type)
        {
            int total = 0;
            IQueryable<vwNomination> models = nominationService.GetNominations(ps, pn, qs, out total, type);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.NOMINATIONS);
            return Ok(new ResponseMessage<IQueryable<vwNomination>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-nomination")]
        public async Task<IHttpActionResult> GetNomination(int type, int id)
        {
            NominationViewModel vm = new NominationViewModel();
            vm.Nomination = await nominationService.GetNomination(id);
            vm.NominationTypes =  nominationService.GetNominationTypeSelectModels();
          
                if (type == 1)
                {
                    vm.NominationTypeResults = await trainingPlanService.GetTrainingPlanSelectModels();
                }
                else if(type == 2)
                {
                    if (vm.Nomination.EntityId > 0)
                    {
                        vm.MissionAppointments =
                            await missionAppointmentService.GetMissionAppointmentsByMissionId(vm.Nomination.EntityId);
                    }

                    vm.NominationTypeResults = await nominationScheduleService.GetMissionNominationScheduleSelectModels();
                }
                else if (type == 3)
                {
                    vm.NominationTypeResults =
                        await nominationScheduleService.GetForeignVisitNominationScheduleSelectModels();
                }
                else
                {
                    vm.NominationTypeResults =
                        await nominationScheduleService.GetOtherNominationScheduleSelectModels();
                }


          

            return Ok(new ResponseMessage<NominationViewModel>
            {
                Result = vm
            });
        }


        [HttpGet]
        [Route("get-nomination-type")]
        public async Task<IHttpActionResult> GetNominationType()
        {
            NominationViewModel vm = new NominationViewModel();
            vm.NominationTypes = nominationService.GetNominationTypeSelectModels();

            return Ok(new ResponseMessage<NominationViewModel>
            {
                Result = vm
            });
        }

        [HttpGet]
        [Route("get-nomination-by-type")]
        public async Task<IHttpActionResult> GetNominationByType(int type)
        {
            
            List<SelectModel> nominations = await nominationService.GetNominationSelectModels(type);

            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = nominations
            });
        }


        [HttpGet]
        [Route("get-nomination-schedule")]
        public  IHttpActionResult GetNominationSchedule(int id,int type)
        {

            

            return Ok(new ResponseMessage<string>
            {
                Result =  nominationService.GetNominationSchedule(id,type)

            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-nomination")]
        public async Task<IHttpActionResult> SaveNomination([FromBody] NominationModel model)
        {
            return Ok(new ResponseMessage<NominationModel>
            {
                Result = await nominationService.SaveNomination(0, model)
            });
        }

        [HttpPut]
        [Route("update-nomination/{id}")]
        public async Task<IHttpActionResult> UpdateNomination(int id, [FromBody] NominationModel model)
        {
            return Ok(new ResponseMessage<NominationModel>
            {
                Result = await nominationService.SaveNomination(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-nomination/{id}")]
        public async Task<IHttpActionResult> DeleteNomination(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await nominationService.DeleteNomination(id)
            });
        }


      
    }
}