using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
	public class LeaveBreakDownViewModel
	{
		public List<LeaveBreakDown> LeaveBreakDowns { get; set; }
		public EmployeeModel Employee { get; set; }
		public LprCalculateInfoModel LprCalculateInfo { get; set; }
		public EmployeeGeneralModel EmployeeGeneral { get; set; }
	}
}
