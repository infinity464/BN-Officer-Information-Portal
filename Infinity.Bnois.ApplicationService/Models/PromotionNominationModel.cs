using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PromotionNominationModel
    {
        public int PromotionNominationId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> PromotionBoardId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> TransferId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> FromRankId { get; set; }
        public Nullable<int> ToRankId { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> ExecutionRemarkId { get; set; }
        public Nullable<System.DateTime> ExecutionDate { get; set; }
      
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public bool ExType { get; set; }
        public Nullable<double> Opr { get; set; }
        public Nullable<double> Pft { get; set; }
        public Nullable<double> Course { get; set; }
        public Nullable<double> Bonus { get; set; }
        public Nullable<double> Penalty { get; set; }
        public string TraceMark { get; set; }
        public string BranchPosition { get; set; }
        public string BatBraPosition { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }


        public virtual EmployeeModel Employee { get; set; }
        public virtual ExecutionRemarkModel ExecutionRemark { get; set; }
        public virtual PromotionBoardModel PromotionBoard { get; set; }
        public virtual RankModel Rank { get; set; }
        public virtual RankModel Rank1 { get; set; }
    }
}
