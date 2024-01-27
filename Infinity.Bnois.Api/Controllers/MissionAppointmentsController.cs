
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
    [RoutePrefix(BnoisRoutePrefix.MissionAppointments)]
    [EnableCors("*","*","*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MISSION_APPOINTMENTS)]

    public class MissionAppointmentsController : PermissionController
    {
        private readonly IMissionAppointmentService MissionAppointmentService;
        private readonly IAppointmentNatureService appointmentNatureService;
        private readonly IAppointmentCategoryService appointmentCategoryService;
        private readonly INominationScheduleService nominationScheduleService;
        private readonly IBranchService branchService;
        private readonly IRankService rankService;

        
        public MissionAppointmentsController(IMissionAppointmentService MissionAppointmentService, IAppointmentCategoryService appointmentCategoryService,
            IAppointmentNatureService appointmentNatureService, INominationScheduleService nominationScheduleService,
            IBranchService branchService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.MissionAppointmentService = MissionAppointmentService;
            this.appointmentNatureService = appointmentNatureService;
            this.appointmentCategoryService = appointmentCategoryService;
            this.nominationScheduleService = nominationScheduleService;

            this.branchService = branchService;
            this.rankService = rankService;

        }

        [HttpGet]
        [Route("get-mission-appointments")]
        public IHttpActionResult GetMissionAppointments(int ps, int pn, string qs)
        {
            int total = 0;
            List<MissionAppointmentModel> models = MissionAppointmentService.GetMissionAppointments(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MISSION_APPOINTMENTS);
            return Ok(new ResponseMessage<List<MissionAppointmentModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-mission-appointment")]
        public async Task<IHttpActionResult> GetMissionAppointment(int id)
        {
            MissionAppointmentViewModel vm = new MissionAppointmentViewModel();
            vm.MissionAppointment = await MissionAppointmentService.GetMissionAppointment(id);
            vm.Missions = await  nominationScheduleService.GetMissionNominationScheduleSelectModels();
            vm.AptNats = await  appointmentNatureService.GetNatureSelectList();

            if (vm.MissionAppointment.MissionAppointmentId >0)
            {
                vm.AptCats = await appointmentCategoryService.GetCategorySelectListByNature(vm.MissionAppointment.AppointmentNatureId);
            }
            vm.BranchList= await branchService.GetBranchSelectModels();
            vm.RankList= await rankService.GetRankSelectModels();

            return Ok(new ResponseMessage<MissionAppointmentViewModel>
            {
                Result = vm
            });
        }



        [HttpGet]
        [Route("get-mission-appointment-by-mission")]
        public async Task<IHttpActionResult> GetMissionAppointmentByMission(int MissionId)
        {
            List<SelectModel> selectModels = await MissionAppointmentService.GetMissionAppointmentsByMissionId(MissionId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }


        [HttpGet]
        [Route("get-mission-schedule")]
        public IHttpActionResult GetMissionSchedule(int missionId)
        {

            return Ok(new ResponseMessage<string>
            {
                Result = MissionAppointmentService.GetMissionSchedule(missionId)

            });
        }





        [HttpPost]
        [ModelValidation]
        [Route("save-mission-appointment")]
        public async Task<IHttpActionResult> SaveMissionAppointment([FromBody] MissionAppointmentModel model)
        {
            return Ok(new ResponseMessage<MissionAppointmentModel>
            {
                Result = await MissionAppointmentService.SaveMissionAppointment(0, model)
            });
        }

        [HttpPut]
        [Route("update-mission-appointment/{id}")]
        public async Task<IHttpActionResult> UpdateMissionAppointment(int id, [FromBody] MissionAppointmentModel model)
        {
            return Ok(new ResponseMessage<MissionAppointmentModel>
            {
                Result = await MissionAppointmentService.SaveMissionAppointment(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-mission-appointment/{id}")]
        public async Task<IHttpActionResult> DeleteMissionAppointment(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await MissionAppointmentService.DeleteMissionAppointment(id)
            });
        }
    }
}