using Infinity.Bnois.ApplicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IExamSubjectService
    {
        List<ExamSubjectModel> GetExamSubjects(int ps, int pn, string qs, out int total);
        Task<ExamSubjectModel> GetExamSubject(int examSubjectId);
        Task<ExamSubjectModel> SaveExamSubject(int v, ExamSubjectModel model);
        Task<bool> DeleteExamSubject(int examSubjectId);
    }
}
