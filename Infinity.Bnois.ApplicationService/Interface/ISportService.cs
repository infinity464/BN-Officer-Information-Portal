using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ISportService
    {
        List<SportModel> GetSports(int ps, int pn, string qs, out int total);
        Task<SportModel> GetSport(int id);
        Task<SportModel> SaveSport(int v, SportModel model);
        Task<bool> DeleteSport(int id);
        Task<List<SelectModel>> GetSportsSelectModels();
    }
}
