using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class LprCalculateInfoModel
	{
		public int LprCalculateId { get; set; }
		public int EmployeeId { get; set; }
		public Nullable<int> SailorDue { get; set; }
		public Nullable<int> LPR { get; set; }
		public Nullable<int> TerminalLeave { get; set; }
		public Nullable<int> SurveyLeave { get; set; }
		public Nullable<int> FlLeave { get; set; }
		public string CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public string ModifiedBy { get; set; }
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		public bool IsActive { get; set; }
		public EmployeeModel Employee { get; set; }

	}
}
