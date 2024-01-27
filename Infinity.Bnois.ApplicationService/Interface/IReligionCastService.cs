using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IReligionCastService
    {
        List<ReligionCastModel> GetReligionCasts(int ps, int pn, string qs, out int total);
        Task<ReligionCastModel> GetReligionCast(int id);
        Task<List<SelectModel>> GetReligionSelectModels();
        Task<ReligionCastModel> SaveReligionCast(int v, ReligionCastModel model);
        Task<bool> DeleteReligionCast(int id);
        Task<List<SelectModel>> GetReligionCastSelectModels();
        Task<List<SelectModel>> GetReligionCasts(int religionId);
    }
}
