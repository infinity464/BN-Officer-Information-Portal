using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface ITrainingPlanService
    {
        List<TrainingPlanModel> GetTrainingPlans(int ps, int pn, string qs, out int total);
        Task<TrainingPlanModel> GetTrainingPlan(int id);
        Task<TrainingPlanModel> SaveTrainingPlan(int v, TrainingPlanModel model);
        Task<bool> DeleteTrainingPlan(int id);
        List<SelectModel> GetCountryTypeSelectModels();  
        Task<List<SelectModel>> GetTrainingPlanSelectModels();
        Task<List<SelectModel>> GetTrainingPlanSelectModels(int trainingPlanId);
    }
}
