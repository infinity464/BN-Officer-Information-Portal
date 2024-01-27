using Infinity.Bnois.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class EmployeeLeaveModel
	{
        public EmployeeLeaveModel()
        {
            this.EmployeeLeaveCountries = new HashSet<EmployeeLeaveCountry>();
            this.EmployeeLeaveYears = new HashSet<EmployeeLeaveYear>();
        }

        public int EmpLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public int LeaveTypeId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> Duration { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> ExBdLeave { get; set; }
        public string AccompanyBy { get; set; }
        public Nullable<int> Purpose { get; set; }
        public string FileName { get; set; }
        public int OfficeAppointmentId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeavePurpose LeavePurpose { get; set; }
        public virtual LeaveType LeaveType { get; set; }     
        public virtual ICollection<EmployeeLeaveCountry> EmployeeLeaveCountries { get; set; }     
        public virtual ICollection<EmployeeLeaveYear> EmployeeLeaveYears { get; set; }
   
        public List<EmployeeLeaveBalance> LeaveBalances { get; set; }
        public int[] CountryIds { get; set; }
        public string LeaveDueCount { get; set; }

    }
}
