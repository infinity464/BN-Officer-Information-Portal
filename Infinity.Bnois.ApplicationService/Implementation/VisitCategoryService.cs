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
   

    public class VisitCategoryService : IVisitCategoryService
    {
        private readonly IBnoisRepository<VisitCategory> visitCategoryRepository;
        public VisitCategoryService(IBnoisRepository<VisitCategory> visitCategoryRepository)
        {
            this.visitCategoryRepository = visitCategoryRepository;
        }

        public List<VisitCategoryModel> GetVisitCategories(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<VisitCategory> visitCategories = visitCategoryRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = visitCategories.Count();
            visitCategories = visitCategories.OrderByDescending(x => x.VisitCategoryId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<VisitCategoryModel> models = ObjectConverter<VisitCategory, VisitCategoryModel>.ConvertList(visitCategories.ToList()).ToList();
            return models;
        }

        public async Task<VisitCategoryModel> GetVisitCategory(int  id)
        {
            if (id <= 0)
            {
                return new VisitCategoryModel();
            }
            VisitCategory visitCategory = await visitCategoryRepository.FindOneAsync(x => x.VisitCategoryId == id);

            if (visitCategory == null)
            {
                throw new InfinityNotFoundException("Visit Category not found!");
            }
            VisitCategoryModel model = ObjectConverter<VisitCategory, VisitCategoryModel>.Convert(visitCategory);
            return model;
        }

        public async Task<VisitCategoryModel> SaveVisitCategory(int id, VisitCategoryModel model)
        {
            bool isExist = await visitCategoryRepository.ExistsAsync(x => (x.Name == model.Name) && x.VisitCategoryId != model.VisitCategoryId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Visit Category data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Visit Category data missing!");
            }

            VisitCategory visitCategory = ObjectConverter<VisitCategoryModel, VisitCategory>.Convert(model);

            if (id > 0)
            {
                visitCategory = await visitCategoryRepository.FindOneAsync(x => x.VisitCategoryId == id);
                if (visitCategory == null)
                {
                    throw new InfinityNotFoundException("Visit Category not found!");
                }
                visitCategory.ModifiedDate = DateTime.Now;
                visitCategory.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                visitCategory.CreatedBy = model.CreatedBy;
                visitCategory.CreatedDate = DateTime.Now;
                visitCategory.IsActive = true;
            }
            visitCategory.Name = model.Name;
            visitCategory.Remarks = model.Remarks;

            await visitCategoryRepository.SaveAsync(visitCategory);
            model.VisitCategoryId = visitCategory.VisitCategoryId;
            return model;
        }

        public async Task<bool> DeleteVisitCategory(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            VisitCategory visitCategory = await visitCategoryRepository.FindOneAsync(x => x.VisitCategoryId == id);
            if (visitCategory == null)
            {
                throw new InfinityNotFoundException("Visit Category not found!");
            }
            else
            {
                return await visitCategoryRepository.DeleteAsync(visitCategory);
            }
        }

        public async Task<List<SelectModel>> GetVisitCategorySelectModels()
        {
            ICollection<VisitCategory> models = await visitCategoryRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x=>x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.VisitCategoryId
            }).ToList();
        }

    }
}