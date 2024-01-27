using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   

    public class MedicalCategoryService : IMedicalCategoryService
    {
        private readonly IBnoisRepository<MedicalCategory> medicalCategoryRepository;
        public MedicalCategoryService(IBnoisRepository<MedicalCategory> medicalCategoryRepository)
        {
            this.medicalCategoryRepository = medicalCategoryRepository;
        }

        public List<MedicalCategoryModel> GetMedicalCategories(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<MedicalCategory> medicalCategories = medicalCategoryRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = medicalCategories.Count();
            medicalCategories = medicalCategories.OrderByDescending(x => x.MedicalCategoryId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<MedicalCategoryModel> models = ObjectConverter<MedicalCategory, MedicalCategoryModel>.ConvertList(medicalCategories.ToList()).ToList();
            return models;
        }

        public async Task<MedicalCategoryModel> GetMedicalCategory(int  medicalCategoryId)
        {
            if (medicalCategoryId <= 0)
            {
                return new MedicalCategoryModel();
            }
            MedicalCategory medicalCategory = await medicalCategoryRepository.FindOneAsync(x => x.MedicalCategoryId == medicalCategoryId);

            if (medicalCategory == null)
            {
                throw new InfinityNotFoundException("Medical Category not found!");
            }
            MedicalCategoryModel model = ObjectConverter<MedicalCategory, MedicalCategoryModel>.Convert(medicalCategory);
            return model;
        }

        public async Task<MedicalCategoryModel> SaveMedicalCategory(int medicalCategoryId, MedicalCategoryModel model)
        {
            bool isExist = await medicalCategoryRepository.ExistsAsync(x => (x.Name == model.Name) && x.MedicalCategoryId != model.MedicalCategoryId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Medical Category data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Medical Category data missing!");
            }

            MedicalCategory medicalCategory = ObjectConverter<MedicalCategoryModel, MedicalCategory>.Convert(model);

            if (medicalCategoryId > 0)
            {
                medicalCategory = await medicalCategoryRepository.FindOneAsync(x => x.MedicalCategoryId == medicalCategoryId);
                if (medicalCategory == null)
                {
                    throw new InfinityNotFoundException("Medical Category not found!");
                }
                medicalCategory.ModifiedDate = DateTime.Now;
                medicalCategory.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                medicalCategory.CreatedBy = model.CreatedBy;
                medicalCategory.CreatedDate = DateTime.Now;
                medicalCategory.IsActive = true;
            }
            medicalCategory.Name = model.Name;
            medicalCategory.Remarks = model.Remarks;
            await medicalCategoryRepository.SaveAsync(medicalCategory);
            model.MedicalCategoryId = medicalCategory.MedicalCategoryId;
            return model;
        }

        public async Task<bool> DeleteMedicalCategory(int medicalCategoryId)
        {
            if (medicalCategoryId <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            MedicalCategory medicalCategory = await medicalCategoryRepository.FindOneAsync(x => x.MedicalCategoryId == medicalCategoryId);
            if (medicalCategory == null)
            {
                throw new InfinityNotFoundException("Medical Category not found!");
            }
            else
            {
                return await medicalCategoryRepository.DeleteAsync(medicalCategory);
            }
        }

        public async Task<List<SelectModel>> GetMedicalCategorySelectModels()
        {
            ICollection<MedicalCategory> models = await medicalCategoryRepository.FilterAsync(x => x.IsActive);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.MedicalCategoryId
            }).ToList();
        }

    }
}