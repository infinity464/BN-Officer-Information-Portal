using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class AchievementService : IAchievementService
    {
        private readonly IBnoisRepository<Achievement> achievementRepository;
        public AchievementService(IBnoisRepository<Achievement> achievementRepository)
        {
            this.achievementRepository = achievementRepository;
        }

        public List<AchievementModel> GetAchievements(int ps, int pn, string qs, out int total)
        {
            IQueryable<Achievement> achievements = achievementRepository.FilterWithInclude(x => x.IsActive
                && (String.IsNullOrEmpty(qs)), "Employee", "Employee1","Commendation", "Office");
            total = achievements.Count();
            achievements = achievements.OrderByDescending(x => x.AchievementId).Skip((pn - 1) * ps).Take(ps);
            List<AchievementModel> models = ObjectConverter<Achievement, AchievementModel>.ConvertList(achievements.ToList()).ToList();

            models = models.Select(x =>
            {
                x.TypeName = Enum.GetName(typeof(AchievementComType), x.Type);
                x.GivenByTypeName = Enum.GetName(typeof(GivenByType), x.GivenByType);
                return x;
            }).ToList();

            return models;
        }

        public async Task<AchievementModel> GetAchievement(int id)
        {
            if (id <= 0)
            {
                return new AchievementModel();
            }
            Achievement achievement = await achievementRepository.FindOneAsync(x => x.AchievementId == id, new List<string> { "Employee","Employee.Rank","Employee.Batch","Employee1", "Employee1.Rank", "Employee1.Batch" });
            if (achievement == null)
            {
                throw new InfinityNotFoundException("Achievement not found");
            }
            AchievementModel model = ObjectConverter<Achievement, AchievementModel>.Convert(achievement);
            return model;
        }

    
        public async Task<AchievementModel> SaveAchievement(int id, AchievementModel model)
        {

            if (model == null)
            {
                throw new InfinityArgumentMissingException("Achievement  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Achievement achievement = ObjectConverter<AchievementModel, Achievement>.Convert(model);
            if (id > 0)
            {
                achievement = await achievementRepository.FindOneAsync(x => x.AchievementId == id);
                if (achievement == null)
                {
                    throw new InfinityNotFoundException("Achievement not found !");
                }

                achievement.ModifiedDate = DateTime.Now;
                achievement.ModifiedBy = userId;
            }
            else
            {
                achievement.IsActive = true;
                achievement.CreatedDate = DateTime.Now;
                achievement.CreatedBy = userId;
            }
            achievement.EmployeeId = model.EmployeeId;
            achievement.GivenEmployeeId = model.GivenEmployeeId;
            achievement.GivenTransferId = model.GivenTransferId;
            achievement.CommendationId = model.CommendationId;
            achievement.PatternId = model.PatternId;
            achievement.OfficeId = model.OfficeId;
            achievement.Type = model.Type;
            achievement.Date =model.Date ?? achievement.Date;
            achievement.Remarks = model.Remarks;
            achievement.CommAppType = model.CommAppType;
            achievement.Reason = model.Reason;
            achievement.GivenByType = model.GivenByType;
            achievement.OfficerName = model.OfficerName;
            achievement.OfficerDesignation = model.OfficerDesignation;
            achievement.FileName = model.FileName;

            achievement.IsBackLog = model.IsBackLog;
            achievement.RankId = model.Employee.RankId;
            achievement.TransferId = model.Employee.TransferId;

            if (model.IsBackLog)
            {
                
                achievement.RankId = model.RankId;
                achievement.TransferId = model.TransferId;
            }


            await achievementRepository.SaveAsync(achievement);
            model.AchievementId = achievement.AchievementId;
            return model;
        }


        public async Task<bool> DeleteAchievement(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Achievement achievement = await achievementRepository.FindOneAsync(x => x.AchievementId == id);
            if (achievement == null)
            {
                throw new InfinityNotFoundException("Achievement not found");
            }
            else
            {
                return await achievementRepository.DeleteAsync(achievement);
            }
        }

        public List<SelectModel> GetGivenByTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(GivenByType)).Cast<GivenByType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public List<SelectModel> GetAchievementComTypeSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(AchievementComType)).Cast<AchievementComType>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }


      
    }
}
