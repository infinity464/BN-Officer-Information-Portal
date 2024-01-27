using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IGenderService
    {
        List<GenderModel> GetGenders(int pageSize, int pageNumber, string serchText, out int total);
        Task<GenderModel> GetGender(int genderId);
        Task<GenderModel> SaveGender(int genderId, GenderModel model);
        Task<bool> DeleteGender(int genderId);
        Task<List<SelectModel>> GetGenderSelectModels();
    }
}
