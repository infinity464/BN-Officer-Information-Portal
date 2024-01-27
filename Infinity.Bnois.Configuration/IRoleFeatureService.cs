using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Configuration.ServiceModel;

namespace Infinity.Bnois.Configuration
{
    public interface IRoleFeatureService
    {
    
        bool SaveRoleFeatures(string roleId, Infinity.Bnois.Configuration.ServiceModel.RoleFeatureModel[] roleFeatures);
        List<RoleFeatureModel> GetRoleFeatures(string roleId);
        int[] GetUserFeature(string[] roleIds);
        bool AssignRoleFeatures(string roleId, RoleFeatureModel rfeature);
        RoleFeature GetPermitedRoleFeatures(int featureCode);
    }
}
