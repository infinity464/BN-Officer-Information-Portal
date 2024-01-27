using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRankService
    {
        List<RankModel> GetRanks(int pageSize, int pageNumber, string searchText, out int total);
        Task<RankModel> GetRank(int rankId);
        Task<RankModel> SaveRank(int rankId, RankModel model);
        Task<bool> DeleteRank(int departmentId);
        Task<List<SelectModel>> GetRanksSelectModel();
        Task<List<SelectModel>> GetRankSelectModels();
        Task<List<SelectModel>> GetRankSelectModelsByRankCategory(int rankCategoryId);
        Task<List<SelectModel>> GetConfirmRankSelectModels();
        Task<List<SelectModel>> GetActingRankSelectModels();
    }

}
