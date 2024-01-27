using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.AppointmentCategory)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.APPOINTMENT_CATEGORIES)]
	public class AppointmentCategoryController : PermissionController
    {
		private readonly IAppointmentCategoryService appointmentCategoryService;
		private readonly IAppointmentNatureService appointmentNatureService;
		public AppointmentCategoryController(IAppointmentNatureService appointmentNatureService, IAppointmentCategoryService appointmentCategoryService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.appointmentNatureService = appointmentNatureService;
			this.appointmentCategoryService = appointmentCategoryService;
		}



		[HttpGet]
		[Route("get-appointment-Categorys")]
		public IHttpActionResult GetAppointmentCategorys(int ps, int pn, string qs)
		{
			int total = 0;
			List<AptCatModel> models = appointmentCategoryService.GetAppointmentCategorys(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.APPOINTMENT_CATEGORIES);
            return Ok(new ResponseMessage<List<AptCatModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}


		[HttpGet]
		[Route("get-appointment-Category")]
		public async Task<IHttpActionResult> GetAppointmentCategory(int id)
		{
            AppointmentCategoryViewModel vm = new AppointmentCategoryViewModel();
            vm.aptCatModel = await appointmentCategoryService.GetAppointmentCategory(id);
            vm.AppointmentNatureList = await appointmentNatureService.GetNatureSelectList();

            return Ok(new ResponseMessage<AppointmentCategoryViewModel>
			{
				Result = vm
            });
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-appointment-Category")]
		public async Task<IHttpActionResult> SaveAppointmentCategory([FromBody] AptCatModel model)
		{
			return Ok(new ResponseMessage<AptCatModel>
			{
				Result = await appointmentCategoryService.SaveAppointmentCategory(0, model)
			});
		}

		[HttpPut]
		[Route("update-appointment-Category/{id}")]
		public async Task<IHttpActionResult> UpdateAppointmentCategory(int id, [FromBody] AptCatModel model)
		{
			return Ok(new ResponseMessage<AptCatModel>
			{
				Result = await appointmentCategoryService.SaveAppointmentCategory(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-appointment-Category/{id}")]
		public async Task<IHttpActionResult> DeleteAppointmentCategory(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await appointmentCategoryService.DeleteAppointmentCategory(id)
			});
		}

	}
}
