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
	[RoutePrefix(BnoisRoutePrefix.LeavePurpose)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LEAVE_PURPOSE)]
	public class LeavePurposeController : PermissionController
    {
		private readonly ILeavePurposeService leavePurposeService;
		public LeavePurposeController(ILeavePurposeService leavePurposeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.leavePurposeService = leavePurposeService;
		}
		[HttpGet]
		[Route("get-purposes")]
		public IHttpActionResult GetPurposes(int ps, int pn, string qs)
		{
			int total = 0;
			List<LeavePurposeModel> colors = leavePurposeService.GetPurposes(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LEAVE_PURPOSE);
            return Ok(new ResponseMessage<List<LeavePurposeModel>>()
			{
				Result = colors,
				Total = total, Permission=permission
			});
		}
		[HttpGet]
		[Route("get-purpose")]
		public async Task<IHttpActionResult> GetPurpose(int id)
		{
			LeavePurposeModel vm = await leavePurposeService.GetPurpose(id);
			return Ok(new ResponseMessage<LeavePurposeModel>
			{
				Result = vm
			});
		}
		[HttpPost]
		[ModelValidation]
		[Route("save-purpose")]
		public async Task<IHttpActionResult> SaveColor([FromBody] LeavePurposeModel model)
		{
			return Ok(new ResponseMessage<LeavePurposeModel>
			{
				Result = await leavePurposeService.SavePurpose(0, model)
			});
		}

		[HttpPut]
		[Route("update-purpose/{id}")]
		public async Task<IHttpActionResult> UpdatePurpose(int id, [FromBody] LeavePurposeModel model)
		{
			return Ok(new ResponseMessage<LeavePurposeModel>
			{
				Result = await leavePurposeService.SavePurpose(id, model)
			});
		}
		[HttpDelete]
		[Route("delete-purpose/{id}")]
		public async Task<IHttpActionResult> DeletePurpose(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await leavePurposeService.DeletePurpose(id)
			});
		}
	}
}
