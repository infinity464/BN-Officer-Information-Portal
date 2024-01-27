using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class SecurityClearanceReasonModel
    {
        public int SecurityClearanceReasonId { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

    }
}