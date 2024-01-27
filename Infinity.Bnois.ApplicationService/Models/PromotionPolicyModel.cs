using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class PromotionPolicyModel
    {
        public int PromotionPolicyId { get; set; }
        public int RankFromId { get; set; }
        public int RankToId { get; set; }
        public Nullable<int> ServiceYear { get; set; }
        public bool IsOneYearPreRank { get; set; }
        public bool IsOprRecom { get; set; }
        public bool IsPassLfCdrQExam { get; set; }
        public bool IsSpcialCourse { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual RankModel Rank { get; set; }
        public virtual RankModel Rank1 { get; set; }
    }
}
