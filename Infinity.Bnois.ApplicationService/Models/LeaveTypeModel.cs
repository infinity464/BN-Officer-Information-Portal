using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class LeaveTypeModel
	{
		public int LeaveTypeId { get; set; }
		public string TypeName { get; set; }
		public string ShartName { get; set; }
		public string Description { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		public string ModifiedBy { get; set; }
		public bool IsActive { get; set; }
	}
}
