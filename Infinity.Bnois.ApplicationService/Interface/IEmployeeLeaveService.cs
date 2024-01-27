using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
	public interface IEmployeeLeaveService
	{
		List<EmployeeLeaveModel> GetEmployeeLeaves(int ps, int pn, string qs, int? leaveType, out int total);
		Task<EmployeeLeaveModel> GetEmployeeLeave(int id);
		Task<EmployeeLeaveModel> SaveEmployeeLeave(int v, EmployeeLeaveModel model);
		Task<bool> DeleteEmployeeLeave(int id);
        Task<List<EmployeeLeaveBalance>> GetEmployeeLeaveBalance(int employeeId, int leaveType);
		Task<List<LeaveBreakDown>> GetLeaveBreakDowns(int emId);

		Task<List<SpGetEmployeeLeaveInfoByPNo>> GetEmployeeLeaveDetailsByPNo(string employeePNo);
        Task<List<SpGetEmployeeLeaveInfoByPNo>> GetEmployeeLeaveDetailsByPNo();
        Task<LeaveSummaryModel> GetLeaveSummary(String pno);
        Task<string> GetEmployeeLeaveDue(int employeeId, int leaveType);
        Task<List<EmployeeLeaveBalance>> GetDefaultEmployeeLeaveBalance(int employeeId, int leaveType,int fromDate);


        int CheckUserOfficeHead(string id);
        List<object> GetEmployeeLeavesForAuthorizedUser (int ps, int pn, string qs, int? leaveType);
        List<object> GetEmployeeApprovedLeavesForAuthorizedUser (int ps, int pn, string qs, int? leaveType);
        Task<string> employeeLeaveApprove(int leaveId, int leaveDetailId);
        List<object> GetEmployeeLeaveStatusData (int empLeaveId);
        
        List<object> GetLeavesForNs(string pno, int? leaveType);
        List<object> GetEmployeeLeaveStatusDatabyIndividualStatusId (int empLeaveId);
        string ApproveEmployeeLeaveByNs (int empLeaveId, string remark);
        string CancelEmployeeLeaveByNs (int empLeaveId, string remark);
        string CancelEmployeeLeave(int empLeaveId, string remark);
        Task<List<object>> leaveTrackingByLeaveId(int leaveId);
        string GetEmployeeLeaveNotifications();
        List<object> GetCurrentEmployeeInformation();
    }
}
