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
    public class BonusPtPublicService : IBonusPtPublicService
    {
        private readonly IBnoisRepository<BonusPtPublic> bonusPtPublicRepository;
        public BonusPtPublicService(IBnoisRepository<BonusPtPublic> bonusPtPublicRepository)
        {
            this.bonusPtPublicRepository = bonusPtPublicRepository;
        }

        public async Task<BonusPtPublicModel> GetBonusPtPublic(int bonusPtPublicId)
        {
            if (bonusPtPublicId <= 0)
            {
                return new BonusPtPublicModel();
            }
            BonusPtPublic bonusPtPublic = await bonusPtPublicRepository.FindOneAsync(x => x.BonusPtPublicId == bonusPtPublicId);

            if (bonusPtPublic == null)
            {
                throw new InfinityNotFoundException("Bonus Point for Public not found!");
            }
            BonusPtPublicModel model = ObjectConverter<BonusPtPublic, BonusPtPublicModel>.Convert(bonusPtPublic);
            return model;
        }

        public List<BonusPtPublicModel> GetBonusPtPublics(int traceSettingId)
        {
            List<BonusPtPublic> bonusPtPublics = bonusPtPublicRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId).ToList();
            List<BonusPtPublicModel> models = ObjectConverter<BonusPtPublic, BonusPtPublicModel>.ConvertList(bonusPtPublics.ToList()).ToList();
            return models;
        }

        public async Task<BonusPtPublicModel> SaveBonusPtPublic(int bonusPtPublicId, BonusPtPublicModel model)
        {
            bool isExist = await bonusPtPublicRepository.ExistsAsync(x =>x.TraceSettingId==model.TraceSettingId && x.BonusPtPublicId != bonusPtPublicId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Bonus Point for Publication data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Bonus Point for Publication data missing");
            }
            BonusPtPublic bonusPtPublic = ObjectConverter<BonusPtPublicModel, BonusPtPublic>.Convert(model);

            if (bonusPtPublicId > 0)
            {
                bonusPtPublic = await bonusPtPublicRepository.FindOneAsync(x => x.BonusPtPublicId == bonusPtPublicId);
                if (bonusPtPublic == null)
                {
                    throw new InfinityNotFoundException("Bonus Point for Publication not found!");
                }
                bonusPtPublic.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtPublic.ModifiedDate = DateTime.Now;
            }
            else
            {
                bonusPtPublic.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                bonusPtPublic.CreatedDate = DateTime.Now;
            }
            bonusPtPublic.TraceSettingId = model.TraceSettingId;
            bonusPtPublic.Point = model.Point;
            bonusPtPublic.Count = model.Count;
            await bonusPtPublicRepository.SaveAsync(bonusPtPublic);
            model.BonusPtPublicId = bonusPtPublic.BonusPtPublicId;
            return model;
        }
    }
}
