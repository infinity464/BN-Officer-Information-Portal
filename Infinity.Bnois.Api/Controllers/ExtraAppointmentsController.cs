
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
using Infinity.Ers.ApplicationService;
using System.Net.Http;
using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.ExtraAppointments)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.EXTRA_APPOINTMENTS)]

    public class ExtraAppointmentsController : PermissionController
    {
        private readonly IExtraAppointmentService ExtraAppointmentService;
        private readonly IOfficeService officeService;
        private readonly IOfficeAppointmentService officeAppointmentService;
       


        public ExtraAppointmentsController(IExtraAppointmentService ExtraAppointmentService,
            IOfficeService officeService, IOfficeAppointmentService officeAppointmentService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.ExtraAppointmentService = ExtraAppointmentService;
            this.officeService = officeService;
            this.officeAppointmentService = officeAppointmentService;

        }

        [HttpGet]
        [Route("get-extra-appointments")]
        public IHttpActionResult GetExtraAppointments(int ps, int pn, string qs)
        {
            int total = 0;
            List<ExtraAppointmentModel> models = ExtraAppointmentService.GetExtraAppointments(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.EXTRA_APPOINTMENTS);
            return Ok(new ResponseMessage<List<ExtraAppointmentModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-extra-appointment")]
        public async Task<IHttpActionResult> GetExtraAppointment(int id)
        {
            ExtraAppointmentViewModel vm = new ExtraAppointmentViewModel();
            vm.ExtraAppointment = await ExtraAppointmentService.GetExtraAppointment(id);
            vm.Offices = await officeService.GetParentOfficeSelectModel();

            if (vm.ExtraAppointment.OfficeId >0)
            {
                vm.Appointments = await officeAppointmentService.GetOfficeAppointmentsByOfficeId(vm.ExtraAppointment.OfficeId);

            }

            return Ok(new ResponseMessage<ExtraAppointmentViewModel>
            {
                Result = vm
            });
        }

       
        [HttpPost]
        [ModelValidation]
        [Route("save-extra-appointment")]
        public async Task<IHttpActionResult> SaveExtraAppointment([FromBody] ExtraAppointmentModel model)
        {
            return Ok(new ResponseMessage<ExtraAppointmentModel>
            {
                Result = await ExtraAppointmentService.SaveExtraAppointment(0, model)
            });
        }

        [HttpPut]
        [Route("update-extra-appointment/{id}")]
        public async Task<IHttpActionResult> UpdateExtraAppointment(int id, [FromBody] ExtraAppointmentModel model)
        {
            return Ok(new ResponseMessage<ExtraAppointmentModel>
            {
                Result = await ExtraAppointmentService.SaveExtraAppointment(id, model)

            });
        }

        [HttpDelete]
        [Route("delete-extra-appointment/{id}")]
        public async Task<IHttpActionResult> DeleteExtraAppointment(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await ExtraAppointmentService.DeleteExtraAppointment(id)
            });
        }

       
    }
}