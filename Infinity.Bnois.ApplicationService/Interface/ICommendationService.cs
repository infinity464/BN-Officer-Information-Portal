using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ICommendationService
    {
        List<CommendationModel> GetCommendations(int pageSize, int pageNumber, string searchText, out int total);
        Task<CommendationModel> GetCommendation(int id);
        Task<CommendationModel> SaveCommendation(int id, CommendationModel model);
        Task<bool> DeleteCommendation(int id);
        Task<List<SelectModel>> GetCommendationSelectModels();
        Task<List<SelectModel>> GetAppreciationSelectModels();
        List<SelectModel> GetCommendationTypeSelectModels();
        Task<List<SelectModel>> GetCommendationAppreciationSelectModels();
    }
}