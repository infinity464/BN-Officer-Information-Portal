
using Infinity.Bnois.Api.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService'
    public interface IRoleService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.IsRoleExist(string)'
        bool IsRoleExist(string name);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.IsRoleExist(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles()'
        List<Role> GetRoles();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles()'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles(UserModel)'
        List<RoleModel> GetRoles(UserModel user);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles(UserModel)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles(string)'
        List<Role> GetRoles(string companyId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRoles(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRole(string)'
        Role GetRole(string roleId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.GetRole(string)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.Save(string, Role)'
        Task<Role> Save(string roleId, Role model);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.Save(string, Role)'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.Delete(string)'
        Task<int> Delete(string roleId);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IRoleService.Delete(string)'
    }
}