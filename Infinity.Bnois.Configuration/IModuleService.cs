using Infinity.Bnois.Configuration.Models;
using Infinity.Bnois.Configuration.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infinity.Bnois.Configuration
{
    public interface IModuleService
    {
        List<ModuleModel> GetModules(int pageSize, int pageNumber, string searchText, out int total);

        ModuleModel GetModule(int moduleId);

        ModuleModel Save(int moduleId, ModuleModel module);

        int Delete(int moduleId);
        List<SelectModel> GetModules();
        List<ModuleModel> GetModuleFeatures(FeatureType featureType);
        List<Node> GetModuleReports(FeatureType feature);
        byte[] downloadModuleReport();
        Task<List<SelectModel>> GetCurrentStatusMenu();
    }
}
