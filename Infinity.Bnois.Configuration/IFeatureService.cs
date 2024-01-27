using Infinity.Bnois.Configuration.Models;
using System.Collections.Generic;

namespace Infinity.Bnois.Configuration
{
    public interface IFeatureService
    {
        List<Infinity.Bnois.Configuration.ServiceModel.FeatureModel> GetFeatures(int pageSize, int pageNumber, string searchText, out int total);

        Infinity.Bnois.Configuration.ServiceModel.FeatureModel GetFeature(int featureId);

        Infinity.Bnois.Configuration.ServiceModel.FeatureModel Save(int featureId, Infinity.Bnois.Configuration.ServiceModel.FeatureModel feature);

        int Delete(int featureId);
        byte[] downloadFeatureReport();
    }
}
