using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IResultGradeService
    {
        List<ResultGradeModel> GetResultGrades(int ps, int pn, string qs, out int total);
        Task<ResultGradeModel> GetResultGrade(int id);
        Task<ResultGradeModel> SaveResultGrade(int v, ResultGradeModel model);
        Task<bool> DeleteResultGrade(int id);
        Task<List<SelectModel>> getGradeSelectModels();
    }
}
