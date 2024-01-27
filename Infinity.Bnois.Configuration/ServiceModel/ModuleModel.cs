using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration.ServiceModel
{
    public class ModuleModel
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public int OrderNo { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<Guid> CreatedBy { get; set; }
        public Nullable<DateTime> EditedDate { get; set; }
        public Nullable<Guid> EditedBy { get; set; }
        public ICollection<FeatureModel> Features { get; set; }

        public bool IsReport { get; set; }
        public bool IsActive { get; set; }
    }
}
