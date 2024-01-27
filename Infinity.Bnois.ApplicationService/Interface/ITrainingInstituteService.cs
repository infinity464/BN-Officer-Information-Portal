using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITrainingInstituteService
    {

        List<TrainingInstituteModel> GetTrainingInstitutes(int ps, int pn, string qs, out int total);
        Task<TrainingInstituteModel> GetTrainingInstitute(int id);
        Task<TrainingInstituteModel> SaveTrainingInstitute(int v, TrainingInstituteModel model);
        Task<bool> DeleteTrainingInstitute(int id);
        List<SelectModel> GetCountryTypeSelectModels();
        Task<List<SelectModel>> GetTrainingInstituteSelectModels(int countryId);
        Task<List<SelectModel>> GetTrainingInstituteSelectModels();


    }
}