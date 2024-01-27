using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Controllers
{
    public  class PermissionController:BaseController
    {
        private readonly IRoleFeatureService roleFeatureService;
        public PermissionController(IRoleFeatureService roleFeatureService)
        {
            this.roleFeatureService = roleFeatureService;
        }

        public  RoleFeature GetFeature(int featureCode)
        {
            RoleFeature roleFeature = roleFeatureService.GetPermitedRoleFeatures(featureCode);
            return roleFeature;
        }

    }
}
