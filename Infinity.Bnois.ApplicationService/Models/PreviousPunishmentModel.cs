using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PreviousPunishmentModel
    {
        public int PreviousPunishmentId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> Type { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}