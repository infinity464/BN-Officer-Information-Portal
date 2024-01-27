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
	[RoutePrefix(BnoisRoutePrefix.Branches)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.BRANCHES)]

    public class BranchsController : PermissionController
    {
		private readonly IBranchService branchService;
		public BranchsController(IBranchService branchService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.branchService = branchService;
		}

		[HttpGet]
		[Route("get-branches")]
		public IHttpActionResult GetBranchs(int ps, int pn, string qs)
		{
			int total = 0;
			List<BranchModel> branchs = branchService.GetBranchs(ps, pn, qs, out total);
			var ss = branchs.Count();
            RoleFeature permission = base.GetFeature(MASTER_SETUP.BRANCHES);
            return Ok(new ResponseMessage<List<BranchModel>>()
			{
				Result = branchs,
				Total = total, Permission=permission
            });
		}

		[HttpGet]
		[Route("get-branch")]
		public async Task<IHttpActionResult> GetBranch(int id)
		{
			BranchModel model = await branchService.GetBranch(id);
			return Ok(new ResponseMessage<BranchModel>
			{
				Result = model
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-branch")]
		public async Task<IHttpActionResult> SaveBranch([FromBody] BranchModel model)
		{
			return Ok(new ResponseMessage<BranchModel>
			{
				Result = await branchService.SaveBranch(0, model)
			});
		}

		[HttpPut]
		[Route("update-branch/{id}")]
		public async Task<IHttpActionResult> Updatebranch(int id, [FromBody] BranchModel model)
		{
			return Ok(new ResponseMessage<BranchModel>
			{
				Result = await branchService.SaveBranch(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-branch/{id}")]
		public async Task<IHttpActionResult> DeleteBranchr(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await branchService.DeleteBranch(id)
			});
		}
	}
}
