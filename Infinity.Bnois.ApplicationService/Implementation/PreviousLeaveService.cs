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
    public class PreviousLeaveService : IPreviousLeaveService
    {
        private readonly IBnoisRepository<PreviousLeave> PreviousLeaveRepository;
        public PreviousLeaveService(IBnoisRepository<PreviousLeave> PreviousLeaveRepository)
        {
            this.PreviousLeaveRepository = PreviousLeaveRepository;
        }

        public async Task<PreviousLeaveModel> GetPreviousLeave(int previousLeaveId)
        {
            if (previousLeaveId <= 0)
            {
                return new PreviousLeaveModel();
            }
            PreviousLeave previousLeave = await PreviousLeaveRepository.FindOneAsync(x => x.PreviousLeaveId == previousLeaveId);
            if (previousLeave == null)
            {
                return new PreviousLeaveModel();
            }

            PreviousLeaveModel model = ObjectConverter<PreviousLeave, PreviousLeaveModel>.Convert(previousLeave);
            return model;
        }

        public List<PreviousLeaveModel> GetPreviousLeaves(int employeeId)
        {
            List<PreviousLeave> previousLeaves = PreviousLeaveRepository.FilterWithInclude(x => x.EmployeeId == employeeId, "LeaveType").ToList();
            List<PreviousLeaveModel> models = ObjectConverter<PreviousLeave, PreviousLeaveModel>.ConvertList(previousLeaves.ToList()).ToList();
            return models;
        }

        public async Task<PreviousLeaveModel> SavePreviousLeave(int previousLeaveId, PreviousLeaveModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Sports data missing!");
            }

            PreviousLeave previousLeave = ObjectConverter<PreviousLeaveModel, PreviousLeave>.Convert(model);
            string userId= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            if (previousLeaveId > 0)
            {
                previousLeave = await PreviousLeaveRepository.FindOneAsync(x => x.PreviousLeaveId == previousLeaveId);
                if (previousLeave == null)
                {
                    throw new InfinityNotFoundException("Leave Not found !");
                }

                previousLeave.ModifiedDate = DateTime.Now;
                previousLeave.ModifiedBy = userId;
            }
            else
            {
                previousLeave.EmployeeId = model.EmployeeId;
                previousLeave.CreatedBy = userId;
                previousLeave.CreatedDate = DateTime.Now;
                previousLeave.IsActive = true;
            }

            previousLeave.LeaveTypeId = model.LeaveTypeId;
            previousLeave.Year = model.Year;
            previousLeave.FromDate = model.FromDate;
            previousLeave.ToDate = model.ToDate;
            previousLeave.Remarks = model.Remarks;
            await PreviousLeaveRepository.SaveAsync(previousLeave);
            model.PreviousLeaveId = previousLeave.PreviousLeaveId;
            return model;
        }


        public async Task<bool> DeletePreviousLeave(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PreviousLeave previousLeave = await PreviousLeaveRepository.FindOneAsync(x => x.PreviousLeaveId == id);
            if (previousLeave == null)
            {
                throw new InfinityNotFoundException("Leave not found");
            }
            else
            {
                return await PreviousLeaveRepository.DeleteAsync(previousLeave);
            }
        }
    }
}
