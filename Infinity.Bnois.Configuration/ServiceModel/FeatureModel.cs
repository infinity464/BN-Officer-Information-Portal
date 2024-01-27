using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration.ServiceModel
{
    public class FeatureModel
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string ActionNgHref { get; set; }
        public int ModuleId { get; set; }
        public int FeatureCode { get; set; }
        public int OrderNo { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> EditedDate { get; set; }
        public Nullable<Guid> EditedBy { get; set; }
        public ModuleModel Module { get; set; }
        public bool IsReport { get; set; }
        public FeatureType FeatureTypeId { get; set; }
        public bool IsActive { get; set; }
      
    }
}
