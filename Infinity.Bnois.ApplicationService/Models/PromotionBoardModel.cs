using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PromotionBoardModel
    {
        public int PromotionBoardId { get; set; }
        public string BoardName { get; set; }
        public int Type { get; set; }
        [Required]
        public Nullable<System.DateTime> FormationDate { get; set; }
        public Nullable<int> FromRankId { get; set; }
        public Nullable<int> ToRankId { get; set; }
        public Nullable<int> LtCdrLevel { get; set; }
        public Nullable<System.DateTime> EvotingDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual RankModel Rank { get; set; }
        public virtual RankModel Rank1 { get; set; }
    }
}
