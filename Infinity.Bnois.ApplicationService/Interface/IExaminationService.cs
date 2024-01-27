using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExaminationService
    {
        List<ExaminationModel> GetExaminations(int pageSize, int pageNumber, string searchText, out int total);
        Task<ExaminationModel> GetExamination(int examinationId);
        Task<ExaminationModel> SaveExamination(int examinationId, ExaminationModel model);
        Task<bool> DeleteExamination(int examinationId);
        List<SelectModel> GetExaminations();
        Task<List<SelectModel>> GetExaminationSelectModelByExamCategory(int? examCategoryId);
        Task<List<SelectModel>> GetExaminationSelectModels();
    }
}
