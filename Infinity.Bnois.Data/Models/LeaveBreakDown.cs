using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	[JsonObject(IsReference = false)]
	public class LeaveBreakDown
	{
		public string FullNameEng { get; set; }
		public string LeaveType { get; set; }
		public int LeaveId { get; set; }
		public Nullable<int> EmpId { get; set; }
		public Nullable<int> LeaveTypeId { get; set; }
		public Nullable<int> CommissionTypeId { get; set; }
		public Nullable<int> LeaveDuration { get; set; }
		public string LeaveDurationType { get; set; }
		public Nullable<int> ForeignDuration { get; set; }
		public string ForeignDurationType { get; set; }
		public Nullable<int> TmYear { get; set; }
		public Nullable<int> YearText { get; set; }
		public Nullable<System.DateTime> CreateDate { get; set; }
		public Nullable<System.DateTime> FormDate { get; set; }
		public Nullable<System.DateTime> ToDate { get; set; }
		public Nullable<int> Duration { get; set; }
		public string Slot { get; set; }
		public string SlotDays { get; set; }
		public Nullable<int> EmpLeaveId { get; set; }
		public string Country { get; set; }
		public List<LeaveBreakDown> LeaveBreakDowns { get; set; }
	}
}
