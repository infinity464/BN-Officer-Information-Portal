using Infinity.Bnois;
using Infinity.Bnois.Api;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Ers.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Controllers;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Ers.Admin.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.MemberRoles)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.MEMBER_ROLES)]

    public class MemberRolesController : PermissionController
    {
        private readonly IMemberRoleService memberRoleService;      

        public MemberRolesController(IMemberRoleService memberRoleService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.memberRoleService = memberRoleService;
        }

        [HttpGet]
        [Route("get-member-roles")]
        public IHttpActionResult GetMemberRoles(int ps, int pn, string qs)
        {
            int total = 0;
            List<MemberRoleModel> models = memberRoleService.GetMemberRoles(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.MEMBER_ROLES);
            return Ok(new ResponseMessage<List<MemberRoleModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }
      
        [HttpGet]
        [Route("get-member-role")]
        public async Task<IHttpActionResult> GetMemberRole(int id)
        {
            MemberRoleModel model = await memberRoleService.GetMemberRole(id);
            return Ok(new ResponseMessage<MemberRoleModel>
            {
                Result = model
            });
        }
      
        [HttpPost]
        [ModelValidation]
        [Route("save-member-role")]
        public async Task<IHttpActionResult> SaveMemberRole([FromBody] MemberRoleModel model)
        {
            model.CreatedBy = base.UserId;
            return Ok(new ResponseMessage<MemberRoleModel>
            {
                Result = await memberRoleService.SaveMemberRole(0, model)
            });
        }
       
        [HttpPut]
        [ModelValidation]
        [Route("update-member-role/{id}")]
        public async Task<IHttpActionResult> UpdateMemberRole(int id, [FromBody] MemberRoleModel model)
        {
            model.ModifiedBy = base.UserId;
            return Ok(new ResponseMessage<MemberRoleModel>
            {
                Result = await memberRoleService.SaveMemberRole(id, model)

            });
        }
    
        [HttpDelete]
        [Route("delete-member-role/{id}")]
        public async Task<IHttpActionResult> DeleteMemberRole(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await memberRoleService.DeleteMemberRole(id)
            });
        }
    }
}
