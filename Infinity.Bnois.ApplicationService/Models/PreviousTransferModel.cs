using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PreviousTransferModel
    {
        public int PreviousTransferId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> RankId { get; set; }
        public string Billet { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual RankModel Rank { get; set; }
    }
}