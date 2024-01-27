using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IEmployeeLprService
	{
		List<EmployeeLprModel> GetEmployeeLprs(int ps, int pn, string qs, out int total);
		Task<EmployeeLprModel> SaveEmployeeLpr(int v, EmployeeLprModel model);
		Task<bool> DeleteEmployeeLpr(int id);
		Task<EmployeeLprModel> GetEmployeeLpr(int id);
		Task<List<SelectModel>> GetRetirementStatusSelectModels();
		Task<List<SelectModel>> GetDurationStatusSelectModels();
	}
}
