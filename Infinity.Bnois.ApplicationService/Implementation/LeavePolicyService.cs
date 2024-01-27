using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class LeavePolicyService : ILeavePolicyService
	{
		private readonly IBnoisRepository<LeavePolicy> leavePolicyRepository;
		public LeavePolicyService(IBnoisRepository<LeavePolicy> leavePolicyRepository)
		{
			this.leavePolicyRepository = leavePolicyRepository;
		}

		public async Task<bool> DeleteLeavePolicy(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			LeavePolicy batch = await leavePolicyRepository.FindOneAsync(x => x.LeavePolicyId == id);
			if (batch == null)
			{
				throw new InfinityNotFoundException("Leave Policy not found");
			}
			else
			{
				return await leavePolicyRepository.DeleteAsync(batch);
			}
		}

		public async Task<List<SelectModel>> GetDurationType()
		{
			List<SelectModel> selectModels =
				Enum.GetValues(typeof(DurationStatus)).Cast<DurationStatus>()
					.Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v).ToString() })
					.ToList();
			return selectModels;
		}

		public async Task<LeavePolicyModel> GetLeavePolicy(int id)
		{
			if (id <= 0)
			{
				return new LeavePolicyModel();
			}
			LeavePolicy LeavePolicy = await leavePolicyRepository.FindOneAsync(x => x.LeavePolicyId == id);
			if (LeavePolicy == null)
			{
				throw new InfinityNotFoundException("Leave Policy not found");
			}
			LeavePolicyModel model = ObjectConverter<LeavePolicy, LeavePolicyModel>.Convert(LeavePolicy);
			return model;
		}

        public List<LeavePolicyModel> GetLeavePolicyies(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        //public List<LeavePolicyModel> GetLeavePolicyies(int ps, int pn, string qs, out int total)
        //{
        //	IQueryable<LeavePolicy> leavePolicy = leavePolicyRepository.FilterWithInclude(x => x.IsActive && (x.LeaveType.TypeName.Contains(qs) || String.IsNullOrEmpty(qs)
        //	|| (x.CommissionType.TypeName.Contains(qs) || String.IsNullOrEmpty(qs))), "CommissionType", "LeaveType");
        //	total = leavePolicy.Count();
        //	leavePolicy = leavePolicy.OrderByDescending(x => x.LeavePolicyId).Skip((pn - 1) * ps).Take(ps);
        //	List<LeavePolicyModel> models = ObjectConverter<LeavePolicy, LeavePolicyModel>.ConvertList(leavePolicy.ToList()).ToList();
        //	return models;
        //}



        public async Task<LeavePolicyModel> SaveLeavePolicy(int id, LeavePolicyModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Leave Policy data missing");
			}
			//bool isExist = leavePolicyRepository.Exists(x => x.TypeName == model.TypeName);
			//if (isExist)
			//{
			//	throw new InfinityInvalidDataException("Data already exists !");
			//}

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			LeavePolicy leavePolicy = ObjectConverter<LeavePolicyModel, LeavePolicy>.Convert(model);
			if (id > 0)
			{
				leavePolicy = await leavePolicyRepository.FindOneAsync(x => x.LeavePolicyId == id);
				if (leavePolicy == null)
				{
					throw new InfinityNotFoundException("Leave Policy not found !");
				}

			    leavePolicy.ModifiedDate = DateTime.Now;
			    leavePolicy.ModifiedBy = userId;
            }
			else
			{
				leavePolicy.IsActive = true;
				leavePolicy.CreatedDate = DateTime.Now;
				leavePolicy.CreatedBy = userId;
			}
			leavePolicy.CommissionTypeId = model.CommissionTypeId;
			leavePolicy.LeaveTypeId = model.LeaveTypeId;
			leavePolicy.LeaveDuration = model.LeaveDuration;
			leavePolicy.LeaveDurationType = model.LeaveDurationType != null ? model.LeaveDurationType.Trim() : model.LeaveDurationType;
			leavePolicy.Slot = model.Slot;
			leavePolicy.ForeignDuration = model.ForeignDuration;
			leavePolicy.ForeignDurationType = model.ForeignDurationType != null ? model.ForeignDurationType.Trim() : model.ForeignDurationType;
			leavePolicy.TmYear = model.WATP == "Y" ? 1 : model.TmYear;
			leavePolicy.WATP = model.WATP;

			await leavePolicyRepository.SaveAsync(leavePolicy);
			model.LeavePolicyId = leavePolicy.LeavePolicyId;
			return model;
		}
	}
}
