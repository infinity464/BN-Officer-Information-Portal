using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Configuration.Data
{
    public class RoleFeatureRepository : CompanyConfigRepository<Infinity.Bnois.Configuration.Models.RoleFeature>, IRoleFeatureRepository
    {
        public RoleFeatureRepository(ConfigurationDbContext context) : base(context)
        {
        }

      
    }
}