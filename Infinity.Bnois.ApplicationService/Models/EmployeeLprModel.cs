using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class EmployeeLprModel
	{
        public int EmpLprId { get; set; }
        public int EmployeeId { get; set; }
        public int TerminationTypeId { get; set; }
        public Nullable<int> CurrentStatus { get; set; }
        public Nullable<System.DateTime> LprDate { get; set; }
        public Nullable<int> DurationMonth { get; set; }
        public Nullable<int> DurationDay { get; set; }
        public Nullable<System.DateTime> TerminationDate { get; set; }
        public Nullable<System.DateTime> RetireDate { get; set; }
        public int RStatus { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual TerminationTypeModel TerminationType { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}
