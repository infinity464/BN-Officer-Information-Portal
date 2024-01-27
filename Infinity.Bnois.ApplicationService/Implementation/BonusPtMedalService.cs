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
  public  class BonusPtMedalService: IBonusPtMedalService
    {
        private readonly IBnoisRepository<BonusPtMedal> bonusPtMedalRepository;
        public BonusPtMedalService(IBnoisRepository<BonusPtMedal> bonusPtMedalRepository)
        {
            this.bonusPtMedalRepository = bonusPtMedalRepository;
        }

        public async Task<BonusPtMedalModel> GetBonusPtMedal(int bonusPtMedalId)
        {
            if (bonusPtMedalId <= 0)
            {
                return new BonusPtMedalModel();
            }
            BonusPtMedal bonusPtMedal = await bonusPtMedalRepository.FindOneAsync(x => x.BonusPtMedalId == bonusPtMedalId, new List<string> { "Medal" });

            if (bonusPtMedal == null)
            {
                throw new InfinityNotFoundException("Bonus Point for Medal not found!");
            }
            BonusPtMedalModel model = ObjectConverter<BonusPtMedal, BonusPtMedalModel>.Convert(bonusPtMedal);
            return model;
        }

        public List<BonusPtMedalModel> GetBonusPtMedals(int traceSettingId)
        {
            List<BonusPtMedal> bonusPtMedals = bonusPtMedalRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "Medal").ToList();
            List<BonusPtMedalModel> models = ObjectConverter<BonusPtMedal, BonusPtMedalModel>.ConvertList(bonusPtMedals.ToList()).ToList();
            return models;
        }

        public async Task<BonusPtMedalModel> SaveBonusPtMedal(int bonusPtMedalId, BonusPtMedalModel model)
        {
            bool isExist = await bonusPtMedalRepository.ExistsAsync(x => x.MedalId == model.MedalId && x.BonusPtMedalId != bonusPtMedalId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Bonus Point for Medal data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Bonus Point for Medal data missing");
            }
            BonusPtMedal bonusPtMedal = ObjectConverter<BonusPtMedalModel, BonusPtMedal>.Convert(model);

            if (bonusPtMedalId > 0)
            {
                bonusPtMedal = await bonusPtMedalRepository.FindOneAsync(x => x.BonusPtMedalId == bonusPtMedalId);
                if (bonusPtMedal == null)
                {
                    throw new InfinityNotFoundException("Bonus Point for Medal not found!");
                }
                bonusPtMedal.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtMedal.ModifiedDate = DateTime.Now;
            }
            else
            {
                bonusPtMedal.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtMedal.CreatedDate = DateTime.Now;
            }
            bonusPtMedal.TraceSettingId = model.TraceSettingId;
            bonusPtMedal.MedalId = model.MedalId;
            bonusPtMedal.Point = model.Point;

            await bonusPtMedalRepository.SaveAsync(bonusPtMedal);
            model.BonusPtMedalId = bonusPtMedal.BonusPtMedalId;
            return model;
        }
    }
}
