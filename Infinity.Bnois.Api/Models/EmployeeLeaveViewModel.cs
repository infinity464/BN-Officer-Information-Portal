using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public class EmployeeLeaveViewModel
    {
        public List<EmployeeLeaveModel> EmployeeLeaves { get; set; }
        public EmployeeModel Employee { get; set; }
        public FileModel File { get; set; }
        public List<LeaveBreakDown> LeaveBreakDown { get; set; }
		public List<EmployeeLeaveBalance> LeaveBalances { get; set; }
		public List<SpGetEmployeeLeaveInfoByPNo> LeaveDetails { get; set; }
	    public EmployeeLeaveModel EmployeeLeave { get; set; }
        public List<SelectModel> LeaveTypes { get; set; }
        public List<SelectModel> Countries { get; set; }
	    public List<SelectModel> LeavePurposeList { get; set; }
	    public List<SelectModel> CommandingOfficerList { get; set; }
       public List<object> employeeLeaveForNS { get; set; }
        public List<object> approvedLeaves { get; set; }


    }
}
