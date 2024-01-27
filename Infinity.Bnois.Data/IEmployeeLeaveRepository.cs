using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
    public interface IEmployeeLeaveRepository : IBnoisRepository<EmployeeLeave>
    {
        Task<List<EmployeeLeaveBalance>> GetEmployeeRepository(int employeeId, int leaveType);
		Task<List<LeaveBreakDown>> GetLeaveBreakDown(int emId);
		Task<List<LeaveBreakDown>> GetLeaveBreakDownByPno(string pId);
	    Task<List<SpGetEmployeeLeaveInfoByPNo>> GetLeaveDetailsByPno(string employeePNo);
        Task<List<EmployeeLeaveBalance>> GetTotalEmployeeRepository(string employeeId);
        Task<List<EmployeeLeaveBalance>> GetEmployeeDueRepository(int employeeId, int leaveType);
    }
}
