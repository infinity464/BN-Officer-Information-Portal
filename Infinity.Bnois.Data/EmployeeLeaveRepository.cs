using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.Data
{
  public class EmployeeLeaveRepository : BnoisRepository<EmployeeLeave>,IEmployeeLeaveRepository
    {
        public EmployeeLeaveRepository(BnoisDbContext context) : base(context)
        {
        }

        public async Task<List<EmployeeLeaveBalance>> GetEmployeeDueRepository(int employeeId, int leaveType)
        {
            return await context.Database.SqlQuery<EmployeeLeaveBalance>("exec [SpGetEmployeeLeaveDue] " + employeeId + "," + leaveType + "").ToListAsync();

        }

        public async Task<List<EmployeeLeaveBalance>> GetEmployeeRepository(int employeeId, int leaveType)
        {
            return await context.Database.SqlQuery<EmployeeLeaveBalance>("exec [SpGetEmployeeLeaveBalance] " + employeeId + "," + leaveType + "").ToListAsync();
        }

		public async Task<List<LeaveBreakDown>> GetLeaveBreakDown(int emId)
		{
			return await context.Database.SqlQuery<LeaveBreakDown>("exec [SpGetEmployeeLeaveBreakDown] " + emId + "").ToListAsync();
		

		}

		public async Task<List<LeaveBreakDown>> GetLeaveBreakDownByPno(string pId)
		{
			return await context.Database.SqlQuery<LeaveBreakDown>("exec SpGetEmployeeLeaveBreakDownByPNo " + pId + "").ToListAsync();
		}

	    public async Task<List<SpGetEmployeeLeaveInfoByPNo>> GetLeaveDetailsByPno(string employeePNo)
	    {
            List<SpGetEmployeeLeaveInfoByPNo> models= await context.Database.SqlQuery<SpGetEmployeeLeaveInfoByPNo>("exec SpGetEmployeeLeaveInfoByPNo " + "'" + employeePNo + "'" + "").ToListAsync();

            return models;
		}

        public async Task<List<EmployeeLeaveBalance>> GetTotalEmployeeRepository(string employeeId)
        {
            return await context.Database.SqlQuery<EmployeeLeaveBalance>("exec [SpGetTotalEmployeeLeaveBalance] " + "'" + employeeId + "'" + "").ToListAsync();
        }
    }
}
