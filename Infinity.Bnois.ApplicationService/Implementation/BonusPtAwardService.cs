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
    public class BonusPtAwardService : IBonusPtAwardService
    {
        private readonly IBnoisRepository<BonusPtAward> bonusPtAwardRepository;
        public BonusPtAwardService(IBnoisRepository<BonusPtAward> bonusPtAwardRepository)
        {
            this.bonusPtAwardRepository = bonusPtAwardRepository;
        }
        public async Task<BonusPtAwardModel> GetBonusPtAward(int bonusPtAwardId)
        {
            if (bonusPtAwardId <= 0)
            {
                return new BonusPtAwardModel();
            }
            BonusPtAward bonusPtAward = await bonusPtAwardRepository.FindOneAsync(x => x.BonusPtAwardId == bonusPtAwardId, new List<string> { "Award" });

            if (bonusPtAward == null)
            {
                throw new InfinityNotFoundException("Bonus Point for Award not found!");
            }
            BonusPtAwardModel model = ObjectConverter<BonusPtAward, BonusPtAwardModel>.Convert(bonusPtAward);
            return model;
        }

        public List<BonusPtAwardModel> GetBonusPtAwards(int traceSettingId)
        {
            List<BonusPtAward> bonusPtAwards = bonusPtAwardRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "Award").ToList();
            List<BonusPtAwardModel> models = ObjectConverter<BonusPtAward, BonusPtAwardModel>.ConvertList(bonusPtAwards.ToList()).ToList();
            return models;
        }

        public async Task<BonusPtAwardModel> SaveBonusPtAward(int bonusPtAwardId, BonusPtAwardModel model)
        {
            bool isExist = await bonusPtAwardRepository.ExistsAsync(x =>x.TraceSettingId==model.TraceSettingId && x.AwardId == model.AwardId && x.BonusPtAwardId != bonusPtAwardId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Bonus Point for Award data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Bonus Point for Award data missing");
            }
            BonusPtAward bonusPtAward = ObjectConverter<BonusPtAwardModel, BonusPtAward>.Convert(model);

            if (bonusPtAwardId > 0)
            {
                bonusPtAward = await bonusPtAwardRepository.FindOneAsync(x => x.BonusPtAwardId == bonusPtAwardId);
                if (bonusPtAward == null)
                {
                    throw new InfinityNotFoundException("Bonus Point for Award not found!");
                }
                bonusPtAward.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtAward.ModifiedDate = DateTime.Now;
            }
            else
            {
                bonusPtAward.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtAward.CreatedDate = DateTime.Now;
            }
            bonusPtAward.TraceSettingId = model.TraceSettingId;
            bonusPtAward.AwardId = model.AwardId;
            bonusPtAward.Point = model.Point;

            await bonusPtAwardRepository.SaveAsync(bonusPtAward);
            model.BonusPtAwardId = bonusPtAward.BonusPtAwardId;
            return model;
        }
    }
}

