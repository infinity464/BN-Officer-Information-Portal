using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class TraceSettingModel
    {
        public int TraceSettingId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public int OPR { get; set; }
        public int Course { get; set; }
        public int PFT { get; set; }
        public int TotalPoint { get; set; }
        public int WeightPreRank { get; set; }
        public int WeightPrevRank { get; set; }
        public string OprCount { get; set; }
        public int OprLastYear { get; set; }
        public int DivisionalFactor { get; set; }
        public int PftCountYear { get; set; }
        public double DductPtPerPft { get; set; }
        public Nullable<int> OWPenalCLOpr { get; set; }
        public Nullable<double> DductPtPerOWKG { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
