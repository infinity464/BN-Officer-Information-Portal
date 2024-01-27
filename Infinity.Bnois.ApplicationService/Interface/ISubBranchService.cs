using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISubBranchService
    {
        List<SubBranchModel> GetSubBranches(int ps, int pn, string qs, out int total);
        Task<SubBranchModel> GetSubBranch(int id);
        Task<SubBranchModel> SaveSubBranch(int v, SubBranchModel model);
        Task<bool> DeleteSubBranch(int id);
        Task<List<SelectModel>> GetSubBranchSelectModels();
    }
}
