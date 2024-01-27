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
	public class LprCalculateInfoService : ILprCalculateInfoService
	{
		private readonly IBnoisRepository<LprCalculateInfo> lprCalculationRepository;
		public LprCalculateInfoService(IBnoisRepository<LprCalculateInfo> lprCalculationRepository)
		{
			this.lprCalculationRepository = lprCalculationRepository;
		}

		public async Task<bool> DeleteLprCalculate(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			LprCalculateInfo lprCalculateInfo = await lprCalculationRepository.FindOneAsync(x => x.LprCalculateId == id);
			if (lprCalculateInfo == null)
			{
				throw new InfinityNotFoundException("LPR Calculate not found");
			}
			else
			{
				return await lprCalculationRepository.DeleteAsync(lprCalculateInfo);
			}
		}

		public async Task<LprCalculateInfoModel> GetLprCalculate(int id)
		{
			if (id <= 0)
			{
				return new LprCalculateInfoModel();
			}
			LprCalculateInfo lprCalculateInfo = await lprCalculationRepository.FindOneAsync(x => x.LprCalculateId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
			if (lprCalculateInfo == null)
			{
				throw new InfinityNotFoundException("LPR Calculate not found");
			}
			LprCalculateInfoModel model = ObjectConverter<LprCalculateInfo, LprCalculateInfoModel>.Convert(lprCalculateInfo);
			return model;
		}

		public async Task<LprCalculateInfoModel> GetLprCalculateByEmpId(int employeeId)
		{
			if (employeeId <= 0)
			{
				return new LprCalculateInfoModel();
			}
			LprCalculateInfo lprCalculateInfo = await lprCalculationRepository.FindOneAsync(x => x.EmployeeId == employeeId);
			if (lprCalculateInfo == null)
			{
				throw new InfinityNotFoundException("LPR Calculate not found");
			}
			LprCalculateInfoModel model = ObjectConverter<LprCalculateInfo, LprCalculateInfoModel>.Convert(lprCalculateInfo);
			return model;
		}

        public List<LprCalculateInfoModel> GetLprCalculates(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        //public List<LprCalculateInfoModel> GetLprCalculates(int ps, int pn, string qs, out int total)
        //{
        //	IQueryable<LprCalculateInfo> lprCalculateInfo = lprCalculationRepository.FilterWithInclude(x => x.IsActive && (x.Employee.PNo.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee");
        //	total = lprCalculateInfo.Count();
        //	lprCalculateInfo = lprCalculateInfo.OrderByDescending(x => x.LprCalculateId).Skip((pn - 1) * ps).Take(ps);
        //	List<LprCalculateInfoModel> models = ObjectConverter<LprCalculateInfo, LprCalculateInfoModel>.ConvertList(lprCalculateInfo.ToList()).ToList();
        //	return models;
        //}

        public async Task<LprCalculateInfoModel> SaveLprCalculate(int id, LprCalculateInfoModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("LPR Calculate data missing");
			}


		    bool isExist = lprCalculationRepository.Exists(x => x.EmployeeId == model.Employee.EmployeeId);
		    if (isExist)
		    {
		        throw new InfinityInvalidDataException("Data already exists !");
		    }



            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			LprCalculateInfo lprCalculate = ObjectConverter<LprCalculateInfoModel, LprCalculateInfo>.Convert(model);
			if (id > 0)
			{
				lprCalculate = await lprCalculationRepository.FindOneAsync(x => x.LprCalculateId == id);
				if (lprCalculate == null)
				{
					throw new InfinityNotFoundException("LPR Calculate not found !");
				}

				lprCalculate.ModifiedDate = DateTime.Now;
				lprCalculate.ModifiedBy = userId;
			}
			else
			{
				lprCalculate.IsActive = true;
				lprCalculate.CreatedDate = DateTime.Now;
				lprCalculate.CreatedBy = userId;
			}

			lprCalculate.EmployeeId = model.Employee.EmployeeId;
			lprCalculate.LprCalculateId = model.LprCalculateId;
			lprCalculate.SailorDue = model.SailorDue;
			lprCalculate.LPR = model.LPR;
			lprCalculate.FlLeave = model.FlLeave;
			lprCalculate.TerminalLeave = model.TerminalLeave;
			lprCalculate.SurveyLeave = model.SurveyLeave;
			//lprCalculate.Employee = null;
			await lprCalculationRepository.SaveAsync(lprCalculate);
			return model;
		}
	}
}
