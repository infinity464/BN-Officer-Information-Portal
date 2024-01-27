using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration.ServiceModel
{
    public class RoleFeatureModel
    {
        public string RoleId { get; set; }
        public int FeatureKey { get; set; }
        public string ModuleName { get; set; }
        public string FeatureName { get; set; }
        public FeatureType FeatureTypeId { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsReport { get; set; }
        public bool Add { get; set; }

        public bool Update { get; set; }

        public bool Delete { get; set; }

        public bool Report { get; set; }
        public List<RoleFeatureModel> Nodes { get; set; }

        
    }
}
