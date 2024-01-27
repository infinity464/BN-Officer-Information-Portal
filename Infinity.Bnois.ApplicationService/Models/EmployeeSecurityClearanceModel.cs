using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class EmployeeSecurityClearanceModel
  {
        public int EmployeeSecurityClearanceId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        [Required]
        public Nullable<System.DateTime> ClearanceDate { get; set; }
        public Nullable<System.DateTime> Expirydate { get; set; }
        public int SecurityClearanceReasonId { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }

        public virtual SecurityClearanceReasonModel SecurityClearanceReason { get; set; }
        public virtual RankModel Rank { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual TransferModel Transfer { get; set; }
    }
}
