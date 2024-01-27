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
   public class BonusPtComAppService : IBonusPtComAppService
    {
        private readonly IBnoisRepository<BonusPtComApp> bonusPtComAppRepository;
        public BonusPtComAppService(IBnoisRepository<BonusPtComApp> bonusPtComAppRepository)
        {
            this.bonusPtComAppRepository = bonusPtComAppRepository;
        }

        public async Task<BonusPtComAppModel> GetBonusPtComApp(int bonusPtComAppId)
        {
            if (bonusPtComAppId <= 0)
            {
                return new BonusPtComAppModel();
            }
            BonusPtComApp bonusPtComApp = await bonusPtComAppRepository.FindOneAsync(x => x.BonusPtComAppId == bonusPtComAppId, new List<string> { "Commendation" });

            if (bonusPtComApp == null)
            {
                throw new InfinityNotFoundException("Bonus Point for Commendation not found!");
            }
            BonusPtComAppModel model = ObjectConverter<BonusPtComApp, BonusPtComAppModel>.Convert(bonusPtComApp);
            return model;
        }

        public List<BonusPtComAppModel> GetBonusPtComApps(int traceSettingId)
        {
            List<BonusPtComApp> bonusPtComApps = bonusPtComAppRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "Commendation").ToList();
            List<BonusPtComAppModel> models = ObjectConverter<BonusPtComApp, BonusPtComAppModel>.ConvertList(bonusPtComApps.ToList()).ToList();
            return models;
        }

        public async Task<BonusPtComAppModel> SaveBonusPtComApp(int bonusPtComAppId, BonusPtComAppModel model)
        {
            bool isExist = await bonusPtComAppRepository.ExistsAsync(x =>x.TraceSettingId==model.TraceSettingId && x.CommendationId == model.CommendationId && x.BonusPtComAppId != bonusPtComAppId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Bonus Point for ComAppation data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Bonus Point for ComAppation data missing");
            }
            BonusPtComApp bonusPtComApp = ObjectConverter<BonusPtComAppModel, BonusPtComApp>.Convert(model);

            if (bonusPtComAppId > 0)
            {
                bonusPtComApp = await bonusPtComAppRepository.FindOneAsync(x => x.BonusPtComAppId == bonusPtComAppId);
                if (bonusPtComApp == null)
                {
                    throw new InfinityNotFoundException("Bonus Point for ComAppation not found!");
                }
                bonusPtComApp.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtComApp.ModifiedDate = DateTime.Now;
            }
            else
            {
                bonusPtComApp.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtComApp.CreatedDate = DateTime.Now;
            }
            bonusPtComApp.TraceSettingId = model.TraceSettingId;
            bonusPtComApp.CommendationId = model.CommendationId;
            bonusPtComApp.Point = model.Point;
            await bonusPtComAppRepository.SaveAsync(bonusPtComApp);
            model.BonusPtComAppId = bonusPtComApp.BonusPtComAppId;
            return model;
        }
    }
}
