using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface ILeavePurposeService
	{
		List<LeavePurposeModel> GetPurposes(int ps, int pn, string qs, out int total);
		Task<LeavePurposeModel> GetPurpose(int id);
		Task<LeavePurposeModel> SavePurpose(int i, LeavePurposeModel model);
		Task<bool> DeletePurpose(int id);
		Task<List<SelectModel>> GetLeavePurposeSelectModel();
	}
}
