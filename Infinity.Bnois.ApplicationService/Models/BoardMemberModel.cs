using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class BoardMemberModel
    {
        public int BoardMemberId { get; set; }
        public int PromotionBoardId { get; set; }
        public int EmployeeId { get; set; }
        public int MemberRoleId { get; set; }
        public bool IsVoteAllowed { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual MemberRoleModel MemberRole { get; set; }
        public virtual PromotionBoardModel PromotionBoard { get; set; }
    }
}
