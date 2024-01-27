using Infinity.Bnois.ExceptionHelper;
using Infinity.Bnois.Api.Web.Data;
using Infinity.Bnois.Api.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.Configuration.ServiceModel;

namespace Infinity.Bnois.Api.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService'
    public class RoleService : RoleManager<Role>, IRoleService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService'
    {
       
        private readonly RoleStore _roleStore;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.RoleService(RoleStore)'
        public RoleService(RoleStore roleStore) : base(roleStore)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.RoleService(RoleStore)'
        {
            this._roleStore = roleStore;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.IsRoleExist(string)'
        public bool IsRoleExist(string name)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.IsRoleExist(string)'
        {
            return base.RoleExistsAsync(name).Result;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles()'
        public  List<Role> GetRoles()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles()'
        {
            return base.Roles.ToList();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles(UserModel)'
        public List<RoleModel> GetRoles(UserModel user)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles(UserModel)'
        {
            List<Role> roles;

            if (string.IsNullOrWhiteSpace(user.CompanyId))
            {
                roles = _roleStore.Roles.OrderBy(x=>x.CompanyId.Length).ThenBy(x=>x.CompanyId).ToList();
            }
            else
            {
                roles = _roleStore.Roles.Where(x => x.CompanyId == user.CompanyId).ToList();
            }

            return roles.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name,
                CompanyId=x.CompanyId,
                TotalUser = x.Users.Count
            }).ToList();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRole(string)'
        public Role GetRole(string roleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRole(string)'
        {
            if (String.IsNullOrWhiteSpace(roleId))
            {
                return new Role();
            }
            Role role = _roleStore.Roles.FirstOrDefault(p => p.Id == roleId);
            if (role == null)
            {
                throw new InfinityNotFoundException("Role not found !");
            }
            return role;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.Save(string, Role)'
        public async Task<Role> Save(string roleId, Role model)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.Save(string, Role)'
        {
            if (!String.IsNullOrWhiteSpace(roleId))
            {
                var role = _roleStore.Roles.SingleOrDefault(x => x.Id == roleId);

                if (role == null)
                {
                    throw new InfinityNotFoundException("Role not found !");
                }
                role.CompanyId = model.CompanyId;
                role.Name = model.Name;
                await _roleStore.UpdateAsync(role);
            }

            else
            {
                await _roleStore.CreateAsync(model);
            }

            return model;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.Delete(string)'
        public async Task<int>  Delete(string roleId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.Delete(string)'
        {
            if (string.IsNullOrWhiteSpace(roleId))
            {
                throw new InfinityArgumentMissingException("Invalid Role !");
            }

            Models.Role role = _roleStore.Roles.SingleOrDefault(x => x.Id == roleId);

            if (role == null)
            {
                throw new InfinityNotFoundException("roles.html");
            }
        
           await _roleStore.DeleteAsync(role);

            return 1;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles(string)'
        public List<Role> GetRoles(string companyId)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleService.GetRoles(string)'
        {
            return base.Roles.Where(x => x.CompanyId == companyId).ToList();
        }


    
    }
}