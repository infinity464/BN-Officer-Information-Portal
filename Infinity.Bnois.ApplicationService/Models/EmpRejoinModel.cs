using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmpRejoinModel
    {
        public int EmpRejoinId { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime RejoinDate { get; set; }
        public int RankId { get; set; }
        public string ReasonToJoin { get; set; }

        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual RankModel Rank { get; set; }
    }
}