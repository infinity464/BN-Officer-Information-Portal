using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMemberRoleService
    {
        List<MemberRoleModel> GetMemberRoles(int ps, int pn, string qs, out int total);
        Task<MemberRoleModel> GetMemberRole(int id);
        Task<MemberRoleModel> SaveMemberRole(int v, MemberRoleModel model);
        Task<bool> DeleteMemberRole(int id);
        Task<List<SelectModel>> GetMemberRoleSelectModels();
    }
}
