using Infinity.Bnois;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class GenderService : IGenderService
    {
        private readonly IBnoisRepository<Gender> genderRepository;
        public GenderService(IBnoisRepository<Gender> genderRepository)
        {
            this.genderRepository = genderRepository;
        }

        public List<GenderModel> GetGenders(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<Gender> genders = genderRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = genders.Count();
            genders = genders.OrderByDescending(x => x.GenderId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<GenderModel> models = ObjectConverter<Gender, GenderModel>.ConvertList(genders.ToList()).ToList();
            return models;
        }

        public async Task<GenderModel> GetGender(int genderId)
        {
            if (genderId <= 0)
            {
                return new GenderModel();
            }
            Gender gender = await genderRepository.FindOneAsync(x => x.GenderId == genderId);

            if (gender == null)
            {
                throw new InfinityNotFoundException("Gender not found!");
            }
            GenderModel model = ObjectConverter<Gender, GenderModel>.Convert(gender);
            return model;
        }

        public async Task<GenderModel> SaveGender(int genderId, GenderModel model)
        {
            bool isExist = await genderRepository.ExistsAsync(x => (x.Name == model.Name) && x.GenderId != model.GenderId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Gender data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Gender data missing!");
            }

            Gender gender = ObjectConverter<GenderModel, Gender>.Convert(model);

            if (genderId > 0)
            {
                gender = await genderRepository.FindOneAsync(x => x.GenderId == genderId);
                if (gender == null)
                {
                    throw new InfinityNotFoundException("Gender not found!");
                }
                gender.ModifiedDate = DateTime.Now;
                gender.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                gender.CreatedBy = model.CreatedBy;
                gender.CreatedDate = DateTime.Now;
                gender.IsActive = true;
            }
            gender.Name = model.Name;
            gender.Remarks = model.Remarks;
            await genderRepository.SaveAsync(gender);
            model.GenderId = gender.GenderId;
            return model;
        }

        public async Task<bool> DeleteGender(int genderId)
        {
            if (genderId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Gender gender = await genderRepository.FindOneAsync(x => x.GenderId == genderId);
            if (gender == null)
            {
                throw new InfinityNotFoundException("Gender not found!");
            }
            else
            {
                return await genderRepository.DeleteAsync(gender);
            }
        }

        public async Task<List<SelectModel>> GetGenderSelectModels()
        {
            ICollection<Gender> models = await genderRepository.FilterAsync(x => x.IsActive);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.GenderId
            }).ToList();
        }
        
    }
}
