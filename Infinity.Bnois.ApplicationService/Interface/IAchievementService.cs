using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IAchievementService
    {
        List<AchievementModel> GetAchievements(int ps, int pn, string qs, out int total);
        Task<AchievementModel> GetAchievement(int id);
        Task<AchievementModel> SaveAchievement(int id, AchievementModel model);
        Task<bool> DeleteAchievement(int id);
        List<SelectModel> GetGivenByTypeSelectModels();
        List<SelectModel> GetAchievementComTypeSelectModels();
        


    }
}
