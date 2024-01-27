using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class BonusPtAwardModel
    {
        public int BonusPtAwardId { get; set; }
        public int TraceSettingId { get; set; }
        public int AwardId { get; set; }
        public Nullable<double> Point { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual AwardModel Award { get; set; }
        public virtual TraceSettingModel TraceSetting { get; set; }
    }
}
