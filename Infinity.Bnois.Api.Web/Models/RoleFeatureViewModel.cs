using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel'
    public class RoleFeatureViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.RoleFeatures'
        public List<Configuration.ServiceModel.RoleFeatureModel> RoleFeatures{ get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.RoleFeatures'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.Role'
        public Role Role { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.Role'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.FeatureTypes'
        public List<SelectModel> FeatureTypes { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'RoleFeatureViewModel.FeatureTypes'

    }
}