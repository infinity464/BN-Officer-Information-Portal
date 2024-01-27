using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IResultService
    {
        List<ResultModel> GetResults(int pageSize, int pageNumber, string searchText, out int total);
        Task<ResultModel> GetResult(int resultId);
        Task<ResultModel> SaveResult(int resultId, ResultModel model);
        Task<bool> DeleteResult(int resultId);
        Task<List<SelectModel>> ResultsSelectModel(int? examcategoryId);
        Task<List<SelectModel>> ResultSelectModels();
    }
}
