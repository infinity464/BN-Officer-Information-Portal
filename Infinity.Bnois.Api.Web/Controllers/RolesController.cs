using Infinity.Bnois;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Web;
using Infinity.Bnois.Api.Web.Models;
using Infinity.Bnois.Api.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Right;

namespace Softcode.Erp.IdentityServer.Controllers
{
    [RoutePrefix(IdentityRoutePrefix.Roles)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.ROLES)]

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController'
    public class RolesController : BaseController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController'
    {
        private readonly IRoleService _roleService;
        private readonly Infinity.Bnois.Configuration.IRoleFeatureService roleFeature;
        private readonly Infinity.Bnois.Api.Web.Services.IUserService _userService;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.RolesController(IRoleService, IUserService, IRoleFeatureService)'
        public RolesController(IRoleService roleService, Infinity.Bnois.Api.Web.Services.IUserService userService, Infinity.Bnois.Configuration.IRoleFeatureService roleFeature)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.RolesController(IRoleService, IUserService, IRoleFeatureService)'
        {
            this._roleService = roleService;
            this._userService = userService;
            this.roleFeature = roleFeature;
        }

     
        [HttpGet]
        [Route("get-roles")]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRoles(int, int, string)'
        public IHttpActionResult GetRoles(int ps, int pn, string q)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRoles(int, int, string)'
        {
            int total = 0;
            return Ok(new ResponseMessage<List<RoleModel>>
            {
                Result = _roleService.GetRoles(_userService.GetUser(UserId)),
                Total = total
            });
        }

        [HttpGet]
        [Route("get-role")]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRole(string)'
        public IHttpActionResult GetRole(string roleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRole(string)'
        {
            return Ok(new ResponseMessage<Role>
            {
                Result = _roleService.GetRole(roleId)
            });
        }

        [HttpPost]
        [ModelValidation]
        [Route("save-role")]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.SaveRole(Role)'
        public async Task<IHttpActionResult> SaveRole([FromBody] Role model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.SaveRole(Role)'
        {
            UserModel userModel = _userService.GetUser(UserId);
            model.CompanyId = userModel.CompanyId;
            return Ok(new ResponseMessage<Role>
            {
                Result = await _roleService.Save("", model)
            });
        }

        [HttpPut]
        [ModelValidation]
        [Route("update-role/{roleId}")]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.UpdateRole(string, Role)'
        public async Task<IHttpActionResult>  UpdateRole(string roleId, [FromBody] Role model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.UpdateRole(string, Role)'
        {
            return Ok(new ResponseMessage<Role>
            {
                Result = await _roleService.Save(roleId, model)
            });
        }

        [HttpDelete]
        [Route("delete-role/{roleId}")]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.DeleteRole(string)'
        public async Task<IHttpActionResult> DeleteRole(string roleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.DeleteRole(string)'
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await _roleService.Delete(roleId)
            });
        }

        [HttpGet]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [Route("get-role-features/{roleId}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRoleFeatures(string)'
        public IHttpActionResult GetRoleFeatures(string roleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.GetRoleFeatures(string)'
        {
            List<SelectModel> featureTypes =
               Enum.GetValues(typeof(FeatureType)).Cast<FeatureType>().Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt32(v) })
              .ToList();


            return Ok(new ResponseMessage<RoleFeatureViewModel>
            {
                Result = new RoleFeatureViewModel
                {
                    Role = _roleService.GetRole(roleId),
                    RoleFeatures = roleFeature.GetRoleFeatures(roleId),

                    FeatureTypes= featureTypes
                }
            });
        }

        [HttpPut]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [Route("update-role-features/{roleId}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.SaveRoleFeatures(string, RoleFeatureModel[])'
        public IHttpActionResult SaveRoleFeatures(string roleId, Infinity.Bnois.Configuration.ServiceModel.RoleFeatureModel[] roleFeatures)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.SaveRoleFeatures(string, RoleFeatureModel[])'
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = roleFeature.SaveRoleFeatures(roleId, roleFeatures),
            });
        }

        [HttpPut]
//        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        [Route("assign-role-feature/{roleId}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RolesController.AssignRoleFeatures(string, RoleFeatureModel)'
        public IHttpActionResult AssignRoleFeatures(string roleId, Infinity.Bnois.Configuration.ServiceModel.RoleFeatureModel rfeature)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RolesController.AssignRoleFeatures(string, RoleFeatureModel)'
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = roleFeature.AssignRoleFeatures(roleId, rfeature),
            });
        }
    }
}