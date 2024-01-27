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
	public class AppointmentNatureService : IAppointmentNatureService
	{
		private readonly IBnoisRepository<AptNat> appoitmentNatureRepository;
		public AppointmentNatureService(IBnoisRepository<AptNat> appoitmentNatureRepository)
		{
			this.appoitmentNatureRepository = appoitmentNatureRepository;
		}

		public async Task<bool> DeleteAppointmentNature(int id)
		{
			if (id < 0)
			{
				throw new InfinityArgumentMissingException("Invalid Request");
			}
			AptNat appointmentNature = await appoitmentNatureRepository.FindOneAsync(x => x.ANatId == id);
			if (appointmentNature == null)
			{
				throw new InfinityNotFoundException("Appointment Nature not found");
			}
			else
			{
				return await appoitmentNatureRepository.DeleteAsync(appointmentNature);
			}
		}

		public async Task<AptNatModel> GetAppointmentNature(int id)
		{
			if (id <= 0)
			{
				return new AptNatModel();
			}
			AptNat AppointmentNature = await appoitmentNatureRepository.FindOneAsync(x => x.ANatId == id);
			if (AppointmentNature == null)
			{
				throw new InfinityNotFoundException("Appointment Nature not found");
			}
			AptNatModel model = ObjectConverter<AptNat, AptNatModel>.Convert(AppointmentNature);
			return model;
		}

		public List<AptNatModel> GetAppointmentNaturies(int ps, int pn, string qs, out int total)
		{
			IQueryable<AptNat> AppointmentNaturies = appoitmentNatureRepository.FilterWithInclude(x => x.IsActive && (x.ANatNm.Contains(qs)|| x.ANatShnm.Contains(qs) || String.IsNullOrEmpty(qs)));
			total = AppointmentNaturies.Count();
			AppointmentNaturies = AppointmentNaturies.OrderByDescending(x => x.ANatId).Skip((pn - 1) * ps).Take(ps);
			List<AptNatModel> models = ObjectConverter<AptNat, AptNatModel>.ConvertList(AppointmentNaturies.ToList()).ToList();
			return models;
		}

        public async Task<List<SelectModel>> GetNatureSelectList()
        {
            ICollection<AptNat> aptNature = await appoitmentNatureRepository.FilterAsync(x => x.IsActive);
           List<AptNat> query= aptNature.OrderBy(x => x.ANatNm).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.ANatNm,
                Value = x.ANatId
            }).ToList();
            return selectModels;
        }

        public async Task<AptNatModel> SaveAppointmentNature(int id, AptNatModel model)
		{
			if (model == null)
			{
				throw new InfinityArgumentMissingException("Appointment Nature data missing");
			}
			bool isExist = appoitmentNatureRepository.Exists(x => x.ANatNm == model.ANatNm && x.ANatId != id);
			if (isExist)
			{
				throw new InfinityInvalidDataException("Appointment Nature exists !");
			}

			string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
			AptNat AppointmentNature = ObjectConverter<AptNatModel, AptNat>.Convert(model);
			if (id > 0)
			{
				AppointmentNature = await appoitmentNatureRepository.FindOneAsync(x => x.ANatId == id);
				if (AppointmentNature == null)
				{
					throw new InfinityNotFoundException("Appointment Nature not found !");
				}

				AppointmentNature.ModifiedDate = DateTime.Now;
				AppointmentNature.ModifiedBy = userId;
			}
			else
			{
				AppointmentNature.IsActive = true;
				AppointmentNature.CreatedDate = DateTime.Now;
				AppointmentNature.CreatedBy = userId;
			}
			AppointmentNature.ANatNm = model.ANatNm;
			AppointmentNature.ANatNmBng = model.ANatNmBng;
			AppointmentNature.ANatShnm = model.ANatShnm;
			AppointmentNature.ANatShnmBng = model.ANatShnmBng;
			AppointmentNature.AptFr = model.AptFr;
            AppointmentNature.IsStaffDuty = model.IsStaffDuty;
            await appoitmentNatureRepository.SaveAsync(AppointmentNature);
			model.ANatId = AppointmentNature.ANatId;
			return model;
		}
	}
}
