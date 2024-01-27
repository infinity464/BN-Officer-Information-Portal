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
    public class PreviousPunishmentService : IPreviousPunishmentService
    {
        private readonly IBnoisRepository<PreviousPunishment> previousPunishmentRepository;
        public PreviousPunishmentService(IBnoisRepository<PreviousPunishment> previousPunishmentRepository)
        {
            this.previousPunishmentRepository = previousPunishmentRepository;
        }

        public async Task<PreviousPunishmentModel> GetPreviousPunishment(int previousPunishmentId)
        {
            if (previousPunishmentId <= 0)
            {
                return new PreviousPunishmentModel(){Type = 1};
            }
            PreviousPunishment previousPunishment = await previousPunishmentRepository.FindOneAsync(x => x.PreviousPunishmentId == previousPunishmentId);
            if (previousPunishment == null)
            {
                return new PreviousPunishmentModel();
            }

            PreviousPunishmentModel model = ObjectConverter<PreviousPunishment, PreviousPunishmentModel>.Convert(previousPunishment);
            return model;
        }

        public List<PreviousPunishmentModel> GetPreviousPunishments(int employeeId)
        {
            List<PreviousPunishment> previousPunishments = previousPunishmentRepository.FilterWithInclude(x => x.EmployeeId == employeeId).ToList();
            List<PreviousPunishmentModel> models = ObjectConverter<PreviousPunishment, PreviousPunishmentModel>.ConvertList(previousPunishments.ToList()).ToList();
            return models;
        }

        public async Task<PreviousPunishmentModel> SavePreviousPunishment(int previousPunishmentId, PreviousPunishmentModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Punishment data missing!");
            }

            PreviousPunishment previousPunishment = ObjectConverter<PreviousPunishmentModel, PreviousPunishment>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (previousPunishmentId > 0)
            {
                previousPunishment = await previousPunishmentRepository.FindOneAsync(x => x.PreviousPunishmentId == previousPunishmentId);
                if (previousPunishment == null)
                {
                    throw new InfinityNotFoundException("Punishment Not found !");
                }

                previousPunishment.ModifiedDate = DateTime.Now;
                previousPunishment.ModifiedBy = userId;
            }
            else
            {
                previousPunishment.EmployeeId = model.EmployeeId;
                previousPunishment.CreatedBy = userId;
                previousPunishment.CreatedDate = DateTime.Now;
                previousPunishment.IsActive = true;
            }

            previousPunishment.Type = model.Type;
            previousPunishment.Remarks = model.Remarks;
            previousPunishment.Description = model.Description;
            previousPunishment.Remarks = model.Remarks;
            await previousPunishmentRepository.SaveAsync(previousPunishment);
            model.PreviousPunishmentId = previousPunishment.PreviousPunishmentId;
            return model;
        }


        public async Task<bool> DeletePreviousPunishment(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PreviousPunishment previousPunishment = await previousPunishmentRepository.FindOneAsync(x => x.PreviousPunishmentId == id);
            if (previousPunishment == null)
            {
                throw new InfinityNotFoundException("Punishment not found");
            }
            else
            {
                return await previousPunishmentRepository.DeleteAsync(previousPunishment);
            }
        }
    }
}
