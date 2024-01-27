using Infinity.Bnois.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel'
    public class FeatureViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.Modules'
        public List<SelectModel> Modules { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.Modules'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.Feature'
        public Infinity.Bnois.Configuration.ServiceModel.FeatureModel Feature { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.Feature'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.FeatureTypes'
        public List<SelectModel> FeatureTypes { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FeatureViewModel.FeatureTypes'

    }
}