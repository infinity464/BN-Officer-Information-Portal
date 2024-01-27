using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmpRunMissingModel
    {
        public int EmpRunMissingId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public int Type { get; set; }
        public System.DateTime Date { get; set; }
        public string TypeName { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}