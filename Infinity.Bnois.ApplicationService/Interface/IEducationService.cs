using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IEducationService
    {
        List<EducationModel> GetEducations(int employeeId);
        Task<EducationModel> GetEducation(int educationId);
        Task<EducationModel> SaveEducation(int educationId, EducationModel model);
        List<SelectModel> GetYearSelectModel();
        List<SelectModel> GetDurationSelectModel();
        Task<bool> DeleteEducation(int id);
        Task<EducationModel> UpdateEducation(EducationModel education);
    }
}
