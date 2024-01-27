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
   public class PtDeductPunishmentService: IPtDeductPunishmentService
    {
        private readonly IBnoisRepository<PtDeductPunishment> ptDeductPunishmentRepository;
        public PtDeductPunishmentService(IBnoisRepository<PtDeductPunishment> ptDeductPunishmentRepository)
        {
            this.ptDeductPunishmentRepository = ptDeductPunishmentRepository;
        }

        public async Task<PtDeductPunishmentModel> GetPtDeductPunishment(int ptDeductPunishmentId)
        {
            if (ptDeductPunishmentId <= 0)
            {
                return new PtDeductPunishmentModel();
            }
            PtDeductPunishment ptDeductPunishment = await ptDeductPunishmentRepository.FindOneAsync(x => x.PtDeductPunishmentId == ptDeductPunishmentId, new List<string> {"PunishmentSubCategory" });

            if (ptDeductPunishment == null)
            {
                throw new InfinityNotFoundException("Point Deduct for Punishment not found!");
            }
            PtDeductPunishmentModel model = ObjectConverter<PtDeductPunishment, PtDeductPunishmentModel>.Convert(ptDeductPunishment);
            return model;
        }

        public List<PtDeductPunishmentModel> GetPtDeductPunishments(int traceSettingId)
        {
            List<PtDeductPunishment> ptDeductPunishments = ptDeductPunishmentRepository.FilterWithInclude(x => x.TraceSettingId == traceSettingId, "PunishmentSubCategory","PunishmentNature").ToList();
            List<PtDeductPunishmentModel> models = ObjectConverter<PtDeductPunishment, PtDeductPunishmentModel>.ConvertList(ptDeductPunishments.ToList()).ToList();
            return models;
        }

        public async Task<PtDeductPunishmentModel> SavePtDeductPunishment(int ptDeductPunishmentId, PtDeductPunishmentModel model)
        {
            bool isExist = await ptDeductPunishmentRepository.ExistsAsync(x =>(x.PunishmentSubCategoryId == model.PunishmentSubCategoryId && x.PunishmentNatureId==model.PunishmentNatureId) && x.PtDeductPunishmentId != ptDeductPunishmentId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Point Deduct for Punishment data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Point Deduct for Punishment data missing");
            }
            PtDeductPunishment ptDeductPunishment = ObjectConverter<PtDeductPunishmentModel, PtDeductPunishment>.Convert(model);

            if (ptDeductPunishmentId > 0)
            {
                ptDeductPunishment = await ptDeductPunishmentRepository.FindOneAsync(x => x.PtDeductPunishmentId == ptDeductPunishmentId);
                if (ptDeductPunishment == null)
                {
                    throw new InfinityNotFoundException("Point Deduct for Punishment not found!");
                }
                ptDeductPunishment.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                ptDeductPunishment.ModifiedDate = DateTime.Now;
            }
            else
            {
                ptDeductPunishment.CreatedBy = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
                ptDeductPunishment.CreatedDate = DateTime.Now;
            }
            ptDeductPunishment.TraceSettingId = model.TraceSettingId;
            ptDeductPunishment.PunishmentSubCategoryId = model.PunishmentSubCategoryId;
            ptDeductPunishment.PunishmentNatureId = model.PunishmentNatureId;
            ptDeductPunishment.PunishmentValue = model.PunishmentValue;
            ptDeductPunishment.SkipYear = model.SkipYear;
            ptDeductPunishment.DeductPercentage = model.DeductPercentage;
            ptDeductPunishment.DeductionYear = model.DeductionYear;

            await ptDeductPunishmentRepository.SaveAsync(ptDeductPunishment);
            model.PtDeductPunishmentId = ptDeductPunishment.PtDeductPunishmentId;
            return model;
        }
    }
}
