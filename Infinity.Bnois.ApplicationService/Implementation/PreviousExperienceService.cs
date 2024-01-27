using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Api;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class PreviousExperienceService : IPreviousExperienceService
    {
        private readonly IBnoisRepository<PreviousExperience> previousExperienceRepository;
        private readonly IBnoisRepository<LprCalculateInfo> lprCalculateInfoRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        public PreviousExperienceService(IBnoisRepository<PreviousExperience> previousExperienceRepository, IBnoisRepository<LprCalculateInfo> lprCalculateInfoRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository)
        {
            this.previousExperienceRepository = previousExperienceRepository;
            this.lprCalculateInfoRepository = lprCalculateInfoRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
        }

        public async Task<PreviousExperienceModel> GetPreviousExperience(int employeeId)
        {
            if (employeeId <= 0)
            {
                return new PreviousExperienceModel();
            }
            PreviousExperience previousExperience = await previousExperienceRepository.FindOneAsync(x => x.EmployeeId == employeeId,new List<string> {"Category","PreCommissionRank"});
            if (previousExperience == null)
            {
                return new PreviousExperienceModel();
            }
            PreviousExperienceModel model = ObjectConverter<PreviousExperience, PreviousExperienceModel>.Convert(previousExperience);
            return model;
        }

        public async Task<PreviousExperienceModel> SavePreviousExperience(int employeeId, PreviousExperienceModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Previous Experience data missing!");
            }

            PreviousExperience previousExperience = ObjectConverter<PreviousExperienceModel, PreviousExperience>.Convert(model);
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (previousExperience.PreviousExperienceId > 0)
            {
                previousExperience = await previousExperienceRepository.FindOneAsync(x => x.EmployeeId == employeeId);
                if (previousExperience == null)
                {
                    throw new InfinityNotFoundException("Previous Experience data not found!");
                }
                previousExperience.ModifiedDate = DateTime.Now;
                previousExperience.ModifiedBy = userId;
            }
            else
            {
                previousExperience.EmployeeId = employeeId;
                previousExperience.CreatedBy = userId;
                previousExperience.CreatedDate = DateTime.Now;
                previousExperience.Active = true;
            }

            previousExperience.PreCommissionRankId = model.PreCommissionRankId;
            previousExperience.CategoryId = model.CategoryId;
            previousExperience.ServiceNo = model.ServiceNo;
            previousExperience.JoiningDate = model.JoiningDate;
            previousExperience.LeavingReason = model.LeavingReason;
            previousExperience.LeaveMonths = model.LeaveMonths;
            previousExperience.LeaveDays = model.LeaveDays;
            previousExperience.Remarks = model.Remarks;
            previousExperience.ISSB = model.ISSB;
            previousExperience.ISSBResult = model.ISSBResult;
            await previousExperienceRepository.SaveAsync(previousExperience);
            model.PreviousExperienceId = previousExperience.PreviousExperienceId;


            var lprCalculateInfo = await lprCalculateInfoRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (lprCalculateInfo != null)
            {
                var employeeGeneral =
                    await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == lprCalculateInfo.EmployeeId);


                if (employeeGeneral.CategoryId == CodeValue.PromotedOfficer || employeeGeneral.CategoryId ==CodeValue.HonoraryOfficer)
                {
                    lprCalculateInfo.ModifiedDate = DateTime.Now;
                    lprCalculateInfo.ModifiedBy = model.ModifiedBy;

                    lprCalculateInfo.SailorDue = previousExperience.LeaveDays + (previousExperience.LeaveMonths * CodeValue.Days);
                    await lprCalculateInfoRepository.SaveAsync(lprCalculateInfo);
                }
               
            }


            return model;
        }
    }
}
