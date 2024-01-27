using System;
using System.Collections.Generic;
using System.Linq;
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
	[RoutePrefix(BnoisRoutePrefix.LeaveTypes)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LEAVE_TYPES)]

    public class LeaveTypesController : PermissionController
    {
		private readonly ILeaveTypeService leaveTypeService;
		public LeaveTypesController(ILeaveTypeService leaveTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.leaveTypeService = leaveTypeService;
		}
		[HttpGet]
		[Route("get-leave-types")]	
		public IHttpActionResult GetLeaveTypes(int ps, int pn, string qs)
		{
			int total = 0;
			List<LeaveTypeModel> models = leaveTypeService.GetLeaveTypes(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_TYPES);
            return Ok(new ResponseMessage<List<LeaveTypeModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-leave-type")]		
		public async Task<IHttpActionResult> GetLeaveType(int id)
		{
			LeaveTypeModel model = await leaveTypeService.GetLeaveType(id);
			return Ok(new ResponseMessage<LeaveTypeModel>
			{
				Result = model
			});
		}
		[HttpPost]		
		[Route("save-leave-type")]		
		public async Task<IHttpActionResult> SaveLeaveType([FromBody] LeaveTypeModel model)
		{
			return Ok(new ResponseMessage<LeaveTypeModel>
			{
				Result = await leaveTypeService.SaveLeaveType(0, model)
			});
		}

		[HttpPut]
		[Route("update-leave-Type/{id}")]
	
		public async Task<IHttpActionResult> UpdateLeaveType(int id, [FromBody] LeaveTypeModel model)
		{
			return Ok(new ResponseMessage<LeaveTypeModel>
			{
				Result = await leaveTypeService.SaveLeaveType(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-leave-Type/{id}")]	
		public async Task<IHttpActionResult> DeleteLeaveType(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await leaveTypeService.DeleteLeave(id)
			});
		}

	}
}
