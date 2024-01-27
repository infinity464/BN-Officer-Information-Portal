using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public class LeavePolicyModel
	{
	    public int LeavePolicyId { get; set; }
	    public int CommissionTypeId { get; set; }
	    public int LeaveTypeId { get; set; }
	    public int LeaveDuration { get; set; }
	    public string LeaveDurationType { get; set; }
	    public Nullable<int> Slot { get; set; }
	    public Nullable<int> ForeignDuration { get; set; }
	    public string ForeignDurationType { get; set; }
	    public Nullable<int> TmYear { get; set; }
	    public string WATP { get; set; }
	    public System.DateTime CreatedDate { get; set; }
	    public string CreatedBy { get; set; }
	    public Nullable<System.DateTime> ModifiedDate { get; set; }
	    public string ModifiedBy { get; set; }
	    public bool IsActive { get; set; }
        public virtual CommissionTypeModel CommissionType { get; set; }
		public virtual LeaveTypeModel LeaveType { get; set; }
	}
}
