using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.AppointmentNature)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.APPOINTMENT_NATURE)]
	public class AppointmentNatureController : PermissionController
    {
		private readonly IAppointmentNatureService appointmentNatureService;
		public AppointmentNatureController(IAppointmentNatureService appointmentNatureService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.appointmentNatureService = appointmentNatureService;
		}

		[HttpGet]
		[Route("get-appointment-Natures")]
		public IHttpActionResult GetAppointmentNatures(int ps, int pn, string qs)
		{
			int total = 0;
			List<AptNatModel> models = appointmentNatureService.GetAppointmentNaturies(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.APPOINTMENT_NATURE);
            return Ok(new ResponseMessage<List<AptNatModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}


		[HttpGet]
		[Route("get-appointment-Nature")]
		public async Task<IHttpActionResult> GetAppointmentNature(int id)
		{
			AptNatModel model = await appointmentNatureService.GetAppointmentNature(id);
			return Ok(new ResponseMessage<AptNatModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-appointment-Nature")]
		public async Task<IHttpActionResult> SaveAppointmentNature([FromBody] AptNatModel model)
		{
			return Ok(new ResponseMessage<AptNatModel>
			{
				Result = await appointmentNatureService.SaveAppointmentNature(0, model)
			});
		}

		[HttpPut]
		[Route("update-appointment-Nature/{id}")]
		public async Task<IHttpActionResult> UpdateAppointmentNature(int id, [FromBody] AptNatModel model)
		{
			return Ok(new ResponseMessage<AptNatModel>
			{
				Result = await appointmentNatureService.SaveAppointmentNature(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-appointment-Nature/{id}")]
		public async Task<IHttpActionResult> DeleteAppointmentNature(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await appointmentNatureService.DeleteAppointmentNature(id)
			});
		}
	}
}
