using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PreviousExperienceModel
    {
        public int PreviousExperienceId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> PreCommissionRankId { get; set; }
        public string ServiceNo { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<int> LeaveMonths { get; set; }
        public Nullable<int> LeaveDays { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string LeavingReason { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> ISSB { get; set; }
        public Nullable<int> ISSBResult { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool Active { get; set; }

        public virtual CategoryModel Category { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual PreCommissionRankModel PreCommissionRank { get; set; }
    }
}
