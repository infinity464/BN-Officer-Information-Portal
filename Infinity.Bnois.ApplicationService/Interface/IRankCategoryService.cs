using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IRankCategoryService
    {
        List<RankCategoryModel> GetRankCategories(int ps, int pn, string qs, out int total);
        Task<RankCategoryModel> GetRankCategory(int id);
        Task<RankCategoryModel> SaveRankCategory(int id, RankCategoryModel model);
        Task<bool> DeleteRankCategoryAsync(int id);
        Task<List<SelectModel>> GetRankCategorySelectModels();
    }
}
