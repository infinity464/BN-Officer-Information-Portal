using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class PoorCourseResultModel
    {
        public int PoorCourseResultId { get; set; }
        public int ResultTypeId { get; set; }
        public int TraceSettingId { get; set; }
        public Nullable<double> DeductPoint { get; set; }
        public Nullable<double> PoorCourseReport { get; set; }
        public int CountryType { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ResultTypeModel ResultType { get; set; }
        public virtual TraceSettingModel TraceSetting { get; set; }
    }
}
