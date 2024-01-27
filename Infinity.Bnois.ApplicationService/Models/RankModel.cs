using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class RankModel
    {
        public int RankId { get; set; }
        public string FullName { get; set; }
        public string FullNameBan { get; set; }
        public string ShortName { get; set; }
        public string ShortNameBan { get; set; }
        public bool IsConfirm { get; set; }
        public Nullable<double> ServiceYear { get; set; }
        public Nullable<int> RankLevel { get; set; }
        public int RankOrder { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
