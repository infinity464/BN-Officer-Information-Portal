using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.SubBranches)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.SUB_BRANCHES)]

    public class SubBranchesController : PermissionController
    {
        private readonly ISubBranchService subBranchService;
        public SubBranchesController(ISubBranchService subBranchService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.subBranchService = subBranchService;
        }

        [HttpGet]
        [Route("get-sub-branches")]
        public IHttpActionResult GetSubBranches(int ps, int pn, string qs)
        {
            int total = 0;
            List<SubBranchModel> models = subBranchService.GetSubBranches(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.SUB_BRANCHES);
            return Ok(new ResponseMessage<List<SubBranchModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-sub-branch")]
        public async Task<IHttpActionResult> GetSubBranch(int id)
        {
            SubBranchModel model = await subBranchService.GetSubBranch(id);
            return Ok(new ResponseMessage<SubBranchModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-sub-branch")]
        public async Task<IHttpActionResult> SaveSubBranch([FromBody] SubBranchModel model)
        {
            return Ok(new ResponseMessage<SubBranchModel>
            {
                Result = await subBranchService.SaveSubBranch(0, model)
            });
        }

        [HttpPut]
        [Route("update-sub-branch/{id}")]
        public async Task<IHttpActionResult> UpdateSubBranch(int id, [FromBody] SubBranchModel model)
        {
            return Ok(new ResponseMessage<SubBranchModel>
            {
                Result = await subBranchService.SaveSubBranch(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-sub-branch/{id}")]
        public async Task<IHttpActionResult> DeleteSubBranch(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await subBranchService.DeleteSubBranch(id)
            });
        }

    }
}
