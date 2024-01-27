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
	public class LeaveTypeService : ILeaveTypeService
	{
		private readonly IBnoisRepository<LeaveType> leaveTypeRepository;
		public LeaveTypeService(IBnoisRepository<LeaveType> leaveTypeRepository)
		{
			this.leaveTypeRepository = leaveTypeRepository;
		}

		public async Task<bool> DeleteLeave(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			LeaveType batch = await leaveTypeRepository.FindOneAsync(x => x.LeaveTypeId == id);
			if (batch == null)
			{
				throw new InfinityNotFoundException("Leave Type not found");
			}
			else
			{
				return await leaveTypeRepository.DeleteAsync(batch);
			}
		}

		public async Task<LeaveTypeModel> GetLeaveType(int id)
		{
			if (id <= 0)
			{
				return new LeaveTypeModel();
			}
			LeaveType leaveType = await leaveTypeRepository.FindOneAsync(x => x.LeaveTypeId == id);
			if (leaveType == null)
			{
				throw new InfinityNotFoundException("Leave Type not found");
			}
			LeaveTypeModel model = ObjectConverter<LeaveType, LeaveTypeModel>.Convert(leaveType);
			return model;
		}

		public List<LeaveTypeModel> GetLeaveTypes(int ps, int pn, string qs, out int total)
		{
			IQueryable<LeaveType> leaveTypes = leaveTypeRepository.FilterWithInclude(x => x.IsActive && (x.TypeName.Contains(qs) || String.IsNullOrEmpty(qs) || (x.ShartName.Contains(qs) || String.IsNullOrEmpty(qs))));
			total = leaveTypes.Count();
			leaveTypes = leaveTypes.OrderByDescending(x => x.LeaveTypeId).Skip((pn - 1) * ps).Take(ps);
			List<LeaveTypeModel> models = ObjectConverter<LeaveType, LeaveTypeModel>.ConvertList(leaveTypes.ToList()).ToList();
			return models;
		}

		public async Task<List<SelectModel>> GetLeaveTypeSelectModel()
		{
			ICollection<LeaveType> leaveType = await leaveTypeRepository.FilterAsync(x => x.IsActive);
			List<SelectModel> selectModels = leaveType.Select(x => new SelectModel
			{
				Text = x.TypeName + "__" +x.ShartName,
				Value = x.LeaveTypeId
			}).ToList();
			return selectModels;
		}

		public async Task<LeaveTypeModel> SaveLeaveType(int id, LeaveTypeModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Leave Type data missing");
			}
		

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			LeaveType leaveType = ObjectConverter<LeaveTypeModel, LeaveType>.Convert(model);
			if (id > 0)
			{
				leaveType = await leaveTypeRepository.FindOneAsync(x => x.LeaveTypeId == id);
				if (leaveType == null)
				{
					throw new InfinityNotFoundException("Leave Type not found !");
				}
				bool isExist = leaveTypeRepository.Exists(x => x.TypeName == model.TypeName);
				if (isExist)
				{
					throw new InfinityInvalidDataException("Data already exists !");
				}

				leaveType.ModifiedDate = DateTime.Now;
			    leaveType.ModifiedBy = userId;
            }
			else
			{
				leaveType.IsActive = true;
				leaveType.CreatedDate = DateTime.Now;
				leaveType.CreatedBy = userId;
			}
			leaveType.TypeName = model.TypeName;
			leaveType.ShartName = model.ShartName;
			leaveType.Description = model.Description;
		
			await leaveTypeRepository.SaveAsync(leaveType);
			model.LeaveTypeId = leaveType.LeaveTypeId;
			return model;
		}
	}
}
