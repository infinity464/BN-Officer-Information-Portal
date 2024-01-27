using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class EmployeeLeaveYearModel
	{
		public int EmpLeaveYearId { get; set; }
		public int EmpLeaveId { get; set; }
		public Nullable<int> YearText { get; set; }
		public System.DateTime LeaveDate { get; set; }

		public virtual EmployeeLeaveModel EmployeeLeave { get; set; }

	}
}
