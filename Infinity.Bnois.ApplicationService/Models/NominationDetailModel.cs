using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class NominationDetailModel
    {
        public long NominationDetailId { get; set; }
        public int NominationId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<bool> IsApporved { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual NominationModel Nomination { get; set; }
    }
}