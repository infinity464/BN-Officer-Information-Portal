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
	public class AppointmentCategoryService : IAppointmentCategoryService
	{
		private readonly IBnoisRepository<AptCat> appoitmentCategoryRepository;
		public AppointmentCategoryService(IBnoisRepository<AptCat> appoitmentCategoryRepository)
		{
			this.appoitmentCategoryRepository = appoitmentCategoryRepository;
		}

		public async Task<bool> DeleteAppointmentCategory(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			AptCat appointmentCategory = await appoitmentCategoryRepository.FindOneAsync(x => x.AcatId == id);
			if (appointmentCategory == null)
			{
				throw new InfinityNotFoundException("Appointment Category not found");
			}
			else
			{
				return await appoitmentCategoryRepository.DeleteAsync(appointmentCategory);
			}
		}


	    public async Task<AptCatModel> GetAppointmentCategory(int id)
		{
			if (id <= 0)
			{
				return new AptCatModel();
			}
			AptCat AppointmentCategory = await appoitmentCategoryRepository.FindOneAsync(x => x.AcatId == id);
			if (AppointmentCategory == null)
			{
				throw new InfinityNotFoundException("Appointment Category not found");
			}
			AptCatModel model = ObjectConverter<AptCat, AptCatModel>.Convert(AppointmentCategory);
			return model;
		}

		//public List<AptCatModel> GetAppointmentCategorys(int ps, int pn, string qs, out int total)
		//{
		//	IQueryable<AptCat> AppointmentCategories = appoitmentCategoryRepository.FilterWithInclude(x => x.IsActive && (x.AcatNm.Contains(qs)|| x.ACatShNm.Contains(qs) || x.AptNat.ANatNm.Contains(qs) || String.IsNullOrEmpty(qs)), "AptNat");
		//	total = AppointmentCategories.Count();
		//	AppointmentCategories = AppointmentCategories.OrderByDescending(x => x.AcatId).Skip((pn - 1) * ps).Take(ps);
		//	List<AptCatModel> models = ObjectConverter<AptCat, AptCatModel>.ConvertList(AppointmentCategories.ToList()).ToList();
		//	return models;
		//}

		public async Task<AptCatModel> SaveAppointmentCategory(int id, AptCatModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Appointment Category data missing");
			}
			bool isExist = appoitmentCategoryRepository.Exists(x => x.AcatNm == model.AcatNm && x.ANatId==model.ANatId && x.AcatId != id);
			if (isExist)
			{
				throw new InfinityInvalidDataException("Appointment Category exists !");
			}

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			AptCat AppointmentCategory = ObjectConverter<AptCatModel, AptCat>.Convert(model);
			if (id > 0)
			{
				AppointmentCategory = await appoitmentCategoryRepository.FindOneAsync(x => x.AcatId == id);
				if (AppointmentCategory == null)
				{
					throw new InfinityNotFoundException("Appointment Category not found !");
				}

				AppointmentCategory.ModifiedDate = DateTime.Now;
				AppointmentCategory.ModifiedBy = userId;
			}
			else
			{
				AppointmentCategory.IsActive = true;
				AppointmentCategory.CreatedDate = DateTime.Now;
				AppointmentCategory.CreatedBy = userId;
			}
			AppointmentCategory.ANatId = model.ANatId;
			AppointmentCategory.AcatNm = model.AcatNm;
			AppointmentCategory.AcatNmBng = model.AcatNmBng;
			AppointmentCategory.ACatShNm = model.ACatShNm;
			AppointmentCategory.ACatShNmBng = model.ACatShNmBng;
			AppointmentCategory.GoAcr = model.GoAcr;

			await appoitmentCategoryRepository.SaveAsync(AppointmentCategory);
			model.AcatId = AppointmentCategory.AcatId;
			return model;
		}


	    public async Task<List<SelectModel>> GetCategorySelectListByNature(int id)
	    {
	        ICollection<AptCat> aptCats = await appoitmentCategoryRepository.FilterAsync(x => x.IsActive && x.ANatId== id);
	        List<SelectModel> selectModels = aptCats.OrderBy(x=>x.AcatNm).Select(x => new SelectModel
	        {
	            Text = x.AcatNm,
	            Value = x.AcatId
	        }).ToList();
	        return selectModels;
	    }

        public List<AptCatModel> GetAppointmentCategorys(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetCategorySelectModel()
        {
            throw new NotImplementedException();
        }

        //public async Task<List<SelectModel>> GetCategorySelectModel()
        //{
        //    ICollection<AptCat> aptCats = await appoitmentCategoryRepository.FilterAsync(x => x.IsActive &&x.AptNat.ANatNm.Contains("Inside Navy"));
        //    List<SelectModel> selectModels = aptCats.OrderBy(x=>x.AcatNm).Select(x => new SelectModel
        //    {
        //        Text = x.AcatNm,
        //        Value = x.AcatId
        //    }).ToList();
        //    return selectModels;
        //}


    }


  

}
