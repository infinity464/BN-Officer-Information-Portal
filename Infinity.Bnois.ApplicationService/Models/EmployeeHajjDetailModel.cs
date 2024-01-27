using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeHajjDetailModel
    {
        public int EmployeeHajjDetailId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<bool> BalotyNonBaloty { get; set; }
        public bool HajjOrOmra { get; set; }
        [Required]
        public Nullable<bool> ArrangedBy { get; set; }
        public Nullable<bool> RoyelGuest { get; set; }
        [Required]
        public System.DateTime FromDate { get; set; }
        [Required]
        public System.DateTime ToDate { get; set; }
        public string ACompanyBy { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}
