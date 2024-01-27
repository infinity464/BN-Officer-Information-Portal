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
   public class NationalityService: INationalityService
    {
        private readonly IBnoisRepository<Nationality> nationalityRepository;
        public NationalityService(IBnoisRepository<Nationality> nationalityRepository)
        {
            this.nationalityRepository = nationalityRepository;
        }

        public List<NationalityModel> GetNationalities(int ps, int pn, string qs, out int total)
        {
            IQueryable<Nationality> nationalities = nationalityRepository.FilterWithInclude(x => x.IsActive
                && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = nationalities.Count();
            nationalities = nationalities.OrderByDescending(x => x.NationalityId).Skip((pn - 1) * ps).Take(ps);
            List<NationalityModel> models = ObjectConverter<Nationality, NationalityModel>.ConvertList(nationalities.ToList()).ToList();
            return models;
        }

        public async Task<NationalityModel> GetNationality(int id)
        {
            if (id <= 0)
            {
                return new NationalityModel();
            }
            Nationality nationality = await nationalityRepository.FindOneAsync(x => x.NationalityId == id);
            if (nationality == null)
            {
                throw new InfinityNotFoundException("Nationality not found");
            }
            NationalityModel model = ObjectConverter<Nationality, NationalityModel>.Convert(nationality);
            return model;
        }

        public async Task<NationalityModel> SaveNationality(int id, NationalityModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Nationality data missing");
            }
            bool isExist = nationalityRepository.Exists(x => x.Name == model.Name && x.NationalityId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Nationality already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Nationality nationality = ObjectConverter<NationalityModel, Nationality>.Convert(model);
            if (id > 0)
            {
                nationality = await nationalityRepository.FindOneAsync(x => x.NationalityId == id);
                if (nationality == null)
                {
                    throw new InfinityNotFoundException("Nationality not found !");
                }
                nationality.ModifiedDate = DateTime.Now;
                nationality.ModifiedBy = userId;
            }
            else
            {
                nationality.IsActive = true;
                nationality.CreatedDate = DateTime.Now;
                nationality.CreatedBy = userId;
            }
            nationality.Name = model.Name;
   
            nationality.Remarks = model.Remarks;
            await nationalityRepository.SaveAsync(nationality);
            return model;
        }

        public async Task<bool> DeleteNationality(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Nationality nationality = await nationalityRepository.FindOneAsync(x => x.NationalityId == id);
            if (nationality == null)
            {
                throw new InfinityNotFoundException("Nationality not found");
            }
            else
            {
                return await nationalityRepository.DeleteAsync(nationality);
            }
        }

        public async Task<List<SelectModel>> GetNationalitySelectModels()
        {
            ICollection<Nationality> nationalities = await nationalityRepository.FilterAsync(x => x.IsActive);
            List<SelectModel> selectModels = nationalities.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.NationalityId
            }).ToList();
            return selectModels;
        }

    }
}
