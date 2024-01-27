using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class EmployeeSportModel
    {
        public long EmployeeSportId { get; set; }
        public int EmployeeId { get; set; }
        public int SportId { get; set; }
        public string TeamName { get; set; }
        public Nullable<System.DateTime> DateOfParticipation { get; set; }
        public string Award { get; set; }
        public string Hobby { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual SportModel Sport { get; set; }
    }
}
