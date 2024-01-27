using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITrainingResultService
    {
        List<TrainingResultModel> GetTrainingResults(int ps, int pn, string qs, out int total);
        Task<TrainingResultModel> GetTrainingResult(int id);
        Task<TrainingResultModel> SaveTrainingResult(int v, TrainingResultModel model);
        Task<bool> DeleteTrainingResult(int id);
        List<SelectModel> GetResultStatusSelectModels();
        Task<TrainingResultModel> GetTrainingResultByEmployee(int id);
        Task<TrainingResultModel> UpdateTrainingResult(TrainingResultModel model);
    }
}
