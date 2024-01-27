using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface ILeaveTypeService
	{
		List<LeaveTypeModel> GetLeaveTypes(int ps, int pn, string qs, out int total);
		Task<LeaveTypeModel> GetLeaveType(int id);
		Task<LeaveTypeModel> SaveLeaveType(int v, LeaveTypeModel model);
		Task<bool> DeleteLeave(int id);
		Task<List<SelectModel>> GetLeaveTypeSelectModel();
	}
}
