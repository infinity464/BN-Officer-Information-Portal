using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface ILprCalculateInfoService
	{
		List<LprCalculateInfoModel> GetLprCalculates(int ps, int pn, string qs, out int total);
		Task<LprCalculateInfoModel> GetLprCalculate(int id);
		Task<LprCalculateInfoModel> SaveLprCalculate(int v, LprCalculateInfoModel model);
		Task<bool> DeleteLprCalculate(int id);
		Task<LprCalculateInfoModel> GetLprCalculateByEmpId(int employeeId);
	}
}
