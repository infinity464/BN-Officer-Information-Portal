using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IMedicalCategoryService
    {
        List<MedicalCategoryModel> GetMedicalCategories(int pageSize, int pageNumber, string serchText, out int total);
        Task<MedicalCategoryModel> GetMedicalCategory(int medicalCategoryId);
        Task<MedicalCategoryModel> SaveMedicalCategory(int medicalCategoryId, MedicalCategoryModel model);
        Task<bool> DeleteMedicalCategory(int medicalCategoryId);
        Task<List<SelectModel>> GetMedicalCategorySelectModels();
    }
}