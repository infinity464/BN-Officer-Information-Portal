using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IInstituteService
    {
        Task<InstituteModel> SaveInstitute(int id, InstituteModel model);
        List<InstituteModel> Institutes(int ps, int pn, string qs, out int total);
        Task<InstituteModel> GetInstitute(int id);
        Task<bool> DeleteInstitute(int id);
        List<SelectModel> getBoardTypeSelectModel();
        Task<List<SelectModel>> GetInstitutesSelectModelByBoard(long? boardId);
        Task<List<SelectModel>> GetInstitutesSelectModels();
    }
}
