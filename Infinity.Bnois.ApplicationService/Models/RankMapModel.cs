using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class RankMapModel
    {
        public int RankMapId { get; set; }
        public int NavyRankId { get; set; }
        public int ArmyRankId { get; set; }
        public int AirForceRankId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public  RankModel Rank { get; set; }
        public  RankModel Rank1 { get; set; }
        public  RankModel Rank2 { get; set; }
    }
}
