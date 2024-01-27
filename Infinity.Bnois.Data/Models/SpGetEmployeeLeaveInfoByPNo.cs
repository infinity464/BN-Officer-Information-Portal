using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infinity.Bnois.Data.Models
{
	[JsonObject(IsReference = false)]
	public class SpGetEmployeeLeaveInfoByPNo
	{
        public string AccompanyBy { get; set; }
        public int EmpLeaveId { get; set; }
		public int EmployeeId { get; set; }
        public string FileName { get; set; }
        public string ShartName { get; set; }
        public string LeaveTypeName { get; set; }
		public Nullable<int> LeaveTypeId { get; set; }
		public System.DateTime FromDate { get; set; }
		public Nullable<System.DateTime> ToDate { get; set; }
		public Nullable<int> Duration { get; set; }
		public string Remarks { get; set; }
		public string Slot { get; set; }
		public string Country { get; set; }
		public System.DateTime CreatedDate { get; set; }
        public int? ApprovedStatus { get; set; }
		public List<SpGetEmployeeLeaveInfoByPNo> SpGetEmployeeLeaveInfoes { get; set; }
	}
}
