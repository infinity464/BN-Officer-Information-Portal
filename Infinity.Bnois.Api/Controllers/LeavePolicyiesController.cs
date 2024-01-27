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
	[RoutePrefix(BnoisRoutePrefix.LeavePolicyies)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LEAVE_POLICIES)]

    public class LeavePolicyiesController : PermissionController
    {
		private readonly ILeavePolicyService leavePolicyService;
		private readonly ICommissionTypeService commissionTypeService;
		private readonly ILeaveTypeService leaveTypeService;
		private readonly IEffectTypeService effectTypeService;
		public LeavePolicyiesController(ILeavePolicyService leavePolicyService, ICommissionTypeService commissionTypeService, 
		    ILeaveTypeService leaveTypeService, IEffectTypeService effectTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.leavePolicyService = leavePolicyService;
			this.commissionTypeService = commissionTypeService;
			this.leaveTypeService = leaveTypeService;
			this.effectTypeService = effectTypeService;
		}

		[HttpGet]
		[Route("get-leavePolicyies")]
		public IHttpActionResult GetLeavePolicyies(int ps, int pn, string qs)
		{
			int total = 0;
			List<LeavePolicyModel> models = leavePolicyService.GetLeavePolicyies(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_POLICIES);
            return Ok(new ResponseMessage<List<LeavePolicyModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-leavePolicy")]
		public async Task<IHttpActionResult> GetLeavePolicy(int id)
		{
			LeavePolicyViewModel vm = new LeavePolicyViewModel();
			vm.LeavePolicy = await leavePolicyService.GetLeavePolicy(id);
			vm.CommissionType = await commissionTypeService.GetCommissionTypeSelectModels();
			vm.LeaveType = await leaveTypeService.GetLeaveTypeSelectModel();
			vm.EffectType = await effectTypeService.GetEffectTypeSelectModel();
          
            vm.DurationType = await leavePolicyService.GetDurationType();
			return Ok(new ResponseMessage<LeavePolicyViewModel>
			{
				Result = vm
			});
		}

		[HttpPost]
		[Route("save-leavePolicy")]
		public async Task<IHttpActionResult> SaveLeavePolicy([FromBody] LeavePolicyModel model)
		{
			return Ok(new ResponseMessage<LeavePolicyModel>
			{
				Result = await leavePolicyService.SaveLeavePolicy(0, model)
			});
		}
		[HttpPut]
		[Route("update-leavePolicy/{id}")]
		public async Task<IHttpActionResult> UpdateLeavePolicy(int id, [FromBody] LeavePolicyModel model)
		{
			return Ok(new ResponseMessage<LeavePolicyModel>
			{
				Result = await leavePolicyService.SaveLeavePolicy(id, model)
			});
		}
		[HttpDelete]
		[Route("delete-leavePolicy/{id}")]
		public async Task<IHttpActionResult> DeleteLeavePolicy(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await leavePolicyService.DeleteLeavePolicy(id)
			});
		}

	}
}
