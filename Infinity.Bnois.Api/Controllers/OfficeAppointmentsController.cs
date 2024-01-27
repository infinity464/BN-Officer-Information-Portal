
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
    [RoutePrefix(BnoisRoutePrefix.OfficeAppointments)]
    [EnableCors("*","*","*")]
  

    public class OfficeAppointmentsController : PermissionController
    {
        private readonly IOfficeAppointmentService officeAppointmentService;
        private readonly IAppointmentNatureService appointmentNatureService;
        private readonly IAppointmentCategoryService appointmentCategoryService;
        private readonly IBranchService branchService;
        private readonly IRankService rankService;

        
        public OfficeAppointmentsController(IOfficeAppointmentService officeAppointmentService, IAppointmentCategoryService appointmentCategoryService,
            IAppointmentNatureService appointmentNatureService,
            IBranchService branchService, IRankService rankService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.officeAppointmentService = officeAppointmentService;
            this.appointmentNatureService = appointmentNatureService;
            this.appointmentCategoryService = appointmentCategoryService;

            this.branchService = branchService;
            this.rankService = rankService;

        }

        [HttpGet]
        [Route("get-office-appointments")]
        public IHttpActionResult GetOfficeAppointments(int ps, int pn, string qs, int officeId,int type)
        {
            int total = 0;
            List<OfficeAppointmentModel> models = officeAppointmentService.GetOfficeAppointments(ps, pn, qs, officeId,type, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.OFFICES);
            return Ok(new ResponseMessage<List<OfficeAppointmentModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-office-appointment")]
        public async Task<IHttpActionResult> GetOfficeAppointment(int id,int officeId)
        {
            OfficeAppointmentViewModel vm = new OfficeAppointmentViewModel();
            vm.OfficeAppointment = await officeAppointmentService.GetOfficeAppointment(id);
            vm.AptNats = await  appointmentNatureService.GetNatureSelectList();

            if (vm.OfficeAppointment.OffAppId >0)
            {
                vm.AptCats = await appointmentCategoryService.GetCategorySelectListByNature(vm.OfficeAppointment.AptNatId);
                vm.ParentAppointments = await officeAppointmentService.GetParentAppointmentSelectModels(officeId, vm.OfficeAppointment.OffAppId);
            }
            else
            {
                vm.ParentAppointments = await officeAppointmentService.GetParentAppointmentSelectModels(officeId,0);
            }
            vm.BranchList= await branchService.GetBranchSelectModels();
            vm.RankList= await rankService.GetRankSelectModels();
            

            return Ok(new ResponseMessage<OfficeAppointmentViewModel>
            {
                Result = vm
            });
        }



        [HttpGet]
        [Route("get-office-appointment-by-office")]
        public async Task<IHttpActionResult> GetOfficeAppointmentByOffice(int officeId)
        {
            List<SelectModel> selectModels = await officeAppointmentService.GetOfficeAppointmentsByOfficeId(officeId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }


        [HttpGet]
        [Route("get-additional-appointment-by-office")]
        public async Task<IHttpActionResult> GetAdditionalAppointmentByOffice(int officeId)
        {
            List<SelectModel> selectModels = await officeAppointmentService.GetOfficeAdditionalAppointmentsByOfficeId(officeId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }


        [HttpGet]
        [Route("get-appointment-by-ship-type")]
        public async Task<IHttpActionResult> GetAppointmentByShipType(int shipType)
        {
            List<SelectModel> selectModels = await officeAppointmentService.GetAppointmentByShipType(shipType);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }

        [HttpGet]
        [Route("get-appointment-by-organization-pattern")]
        public async Task<IHttpActionResult> GetAppointmentByOrganizationPattern(int officeId)
        {
            List<SelectModel> selectModels = await officeAppointmentService.GetAppointmentByOrganizationPattern(officeId);
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = selectModels
            });
        }



        [HttpGet]
        [Route("get-category-by-nature")]
        public async Task<IHttpActionResult> GetCategoryByNature(int id)
        {
            OfficeAppointmentViewModel vm = new OfficeAppointmentViewModel();
            vm.AptCats = await appointmentCategoryService.GetCategorySelectListByNature(id);
            return Ok(new ResponseMessage<OfficeAppointmentViewModel>
            {
                Result = vm
            });
        }




        [HttpPost]
        [ModelValidation]
        [Route("save-office-appointment")]
        public async Task<IHttpActionResult> SaveOfficeAppointment([FromBody] OfficeAppointmentModel model)
        {
            return Ok(new ResponseMessage<OfficeAppointmentModel>
            {
                Result = await officeAppointmentService.SaveOfficeAppointment(0, model)
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-office-additional-appointment")]
        public async Task<IHttpActionResult> SaveOfficeAdditionalAppointment([FromBody] OfficeAppointmentModel model)
        {
            return Ok(new ResponseMessage<OfficeAppointmentModel>
            {
                Result = await officeAppointmentService.SaveOfficeAdditionalAppointment(0, model)
            });
        }




        [HttpPut]
        [Route("update-office-appointment/{id}")]
        public async Task<IHttpActionResult> UpdateOfficeAppointment(int id, [FromBody] OfficeAppointmentModel model)
        {
            return Ok(new ResponseMessage<OfficeAppointmentModel>
            {
                Result = await officeAppointmentService.SaveOfficeAppointment(id, model)

            });
        }

        [HttpPut]
        [Route("update-office-additional-appointment/{id}")]
        public async Task<IHttpActionResult> UpdateOfficeAdditionalAppointment(int id, [FromBody] OfficeAppointmentModel model)
        {
            return Ok(new ResponseMessage<OfficeAppointmentModel>
            {
                Result = await officeAppointmentService.SaveOfficeAdditionalAppointment(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-office-appointment/{id}")]
        public async Task<IHttpActionResult> DeleteOfficeAppointment(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await officeAppointmentService.DeleteOfficeAppointment(id)
            });
        }
    }
}