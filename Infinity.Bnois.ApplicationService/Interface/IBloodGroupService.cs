using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IBloodGroupService
    {
        List<BloodGroupModel> GetBloodGroups(int ps, int pn, string qs, out int total);
        Task<BloodGroupModel> GetBloodGroup(int id);
        Task<BloodGroupModel> SaveBloodGroup(int v, BloodGroupModel model);
        Task<bool> DeleteBloodGroup(int id);
        Task<List<SelectModel>> GetBloodGroupSelectModels();
    }
}
