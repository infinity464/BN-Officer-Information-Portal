using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface ILeavePolicyService
	{
		List<LeavePolicyModel> GetLeavePolicyies(int ps, int pn, string qs, out int total);
		Task<LeavePolicyModel> GetLeavePolicy(int id);
		Task<LeavePolicyModel> SaveLeavePolicy(int v, LeavePolicyModel model);
		Task<bool> DeleteLeavePolicy(int id);

		Task<List<SelectModel>> GetDurationType();
	}
}
