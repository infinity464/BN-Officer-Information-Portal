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
    [RoutePrefix(BnoisRoutePrefix.BloodGroups)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.BLOOD_GROUPS)]

    public class BloodGroupsController : PermissionController
    {
        private readonly IBloodGroupService bloodGroupService;
        public BloodGroupsController(IBloodGroupService bloodGroupService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.bloodGroupService = bloodGroupService;
        }

        [HttpGet]
        [Route("get-blood-groups")]
        public IHttpActionResult GetBloodGroups(int ps, int pn, string qs)
        {
            int total = 0;
            List<BloodGroupModel> models = bloodGroupService.GetBloodGroups(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.BLOOD_GROUPS);
            return Ok(new ResponseMessage<List<BloodGroupModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }


        [HttpGet]
        [Route("get-blood-group")]
        public async Task<IHttpActionResult> GetBloodGroup(int id)
        {
            BloodGroupModel model = await bloodGroupService.GetBloodGroup(id);
            return Ok(new ResponseMessage<BloodGroupModel>
            {
                Result = model
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-blood-group")]
        public async Task<IHttpActionResult> SaveBloodGroup([FromBody] BloodGroupModel model)
        {
            return Ok(new ResponseMessage<BloodGroupModel>
            {
                Result = await bloodGroupService.SaveBloodGroup(0, model)
            });
        }

        [HttpPut]
        [Route("update-blood-group/{id}")]
        public async Task<IHttpActionResult> UpdateBloodGroup(int id, [FromBody] BloodGroupModel model)
        {
            return Ok(new ResponseMessage<BloodGroupModel>
            {
                Result = await bloodGroupService.SaveBloodGroup(id, model)
            });
        }

        [HttpDelete]
        [Route("delete-blood-group/{id}")]
        public async Task<IHttpActionResult> DeleteBloodGroup(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await bloodGroupService.DeleteBloodGroup(id)
            });
        }
    }
}
