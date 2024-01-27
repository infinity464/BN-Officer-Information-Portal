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
    public class PreviousMissionService : IPreviousMissionService
    {
        private readonly IBnoisRepository<PreviousMission> previousMissionRepository;


        public PreviousMissionService(IBnoisRepository<PreviousMission> previousMissionRepository)
        {
            this.previousMissionRepository = previousMissionRepository;
          
        }

        public async Task<PreviousMissionModel> GetPreviousMission(int previousMissionId)
        {
            if (previousMissionId <= 0)
            {
                return new PreviousMissionModel();
            }
            PreviousMission previousMission = await previousMissionRepository.FindOneAsync(x => x.PreviousMissionId == previousMissionId);
            if (previousMission == null)
            {
                return new PreviousMissionModel();
            }

            PreviousMissionModel model = ObjectConverter<PreviousMission, PreviousMissionModel>.Convert(previousMission);
            return model;
        }

        public List<PreviousMissionModel> GetPreviousMissions(int employeeId)
        {
            List<PreviousMission> previousMissions = previousMissionRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "Country").ToList();
            List<PreviousMissionModel> models = ObjectConverter<PreviousMission, PreviousMissionModel>.ConvertList(previousMissions.ToList()).ToList();
            return models;
        }

        public async Task<PreviousMissionModel> SavePreviousMission(int previousMissionId, PreviousMissionModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Mission data missing!");
            }

            PreviousMission previousMission = ObjectConverter<PreviousMissionModel, PreviousMission>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (previousMissionId > 0)
            {
                previousMission = await previousMissionRepository.FindOneAsync(x => x.PreviousMissionId == previousMissionId);
                if (previousMission == null)
                {
                    throw new InfinityNotFoundException("Mission Not found !");
                }

                previousMission.ModifiedDate = DateTime.Now;
                previousMission.ModifiedBy = userId;
            }
            else
            {
                previousMission.EmployeeId = model.EmployeeId;
                previousMission.CreatedBy = userId;
                previousMission.CreatedDate = DateTime.Now;
                previousMission.IsActive = true;
            }

            previousMission.CountryId = model.CountryId;
            previousMission.Title = model.Title;
            previousMission.FromDate = model.FromDate;
            previousMission.ToDate = model.ToDate;
            previousMission.Remarks = model.Remarks;
            await previousMissionRepository.SaveAsync(previousMission);
            model.PreviousMissionId = previousMission.PreviousMissionId;
            return model;
        }


        public async Task<bool> DeletePreviousMission(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PreviousMission previousMission = await previousMissionRepository.FindOneAsync(x => x.PreviousMissionId == id);
            if (previousMission == null)
            {
                throw new InfinityNotFoundException("Mission not found");
            }
            else
            {
                return await previousMissionRepository.DeleteAsync(previousMission);
            }
        }
    }
}
