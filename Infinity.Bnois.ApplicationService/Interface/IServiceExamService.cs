using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IServiceExamService
    {
        List<ServiceExamModel> GetServiceExams(int ps, int pn, string qs, out int total);
        Task<ServiceExamModel> GetServiceExam(int id);
        Task<ServiceExamModel> SaveServiceExam(int v, ServiceExamModel model);
        Task<bool> DeleteServiceExam(int id);
        Task<List<SelectModel>> GetServiceExamSelectModelsByServiceExamCategory(int id);
    }
}