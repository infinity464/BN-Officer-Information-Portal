using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IBranchService
	{
		List<BranchModel> GetBranchs(int ps, int pn, string qs, out int total);
		Task<BranchModel> GetBranch(int id);
		Task<BranchModel> SaveBranch(int id, BranchModel model);
		Task<bool> DeleteBranch(int id);
        Task<List<SelectModel>> GetBranchSelectModels();
    }
}
