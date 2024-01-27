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
    public class CategoryService : ICategoryService
    {
        private readonly IBnoisRepository<Category> categoryRepository;
        public CategoryService(IBnoisRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
       

        public List<CategoryModel> GetCategories(int ps, int pn, string qs, out int total)
        {
            IQueryable<Category> categories = categoryRepository.FilterWithInclude(x => x.IsActive && (x.Name.Contains(qs) || String.IsNullOrEmpty(qs)));
            total = categories.Count();
            categories = categories.OrderByDescending(x => x.CategoryId).Skip((pn - 1) * ps).Take(ps);
            List<CategoryModel> models = ObjectConverter<Category, CategoryModel>.ConvertList(categories.ToList()).ToList();
            return models;
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            if (id <= 0)
            {
                return new CategoryModel();
            }
           Category category = await categoryRepository.FindOneAsync(x => x.CategoryId == id);
            if (category == null)
            {
                throw new InfinityNotFoundException("Category not found");
            }
            CategoryModel model = ObjectConverter<Category, CategoryModel>.Convert(category);
            return model;
        }

        public async Task<CategoryModel> SaveCategory(int id, CategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Category data missing");
            }
            bool isExist = categoryRepository.Exists(x => x.Name == model.Name && x.CategoryId != id);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Category category = ObjectConverter<CategoryModel, Category>.Convert(model);
            if (id > 0)
            {
                category = await categoryRepository.FindOneAsync(x => x.CategoryId == id);
                if (category == null)
                {
                    throw new InfinityNotFoundException("Category not found !");
                }

                category.ModifiedDate = DateTime.Now;
                category.ModifiedBy = userId;
            }
            else
            {
                category.IsActive = true;
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = userId;
            }
            category.Name = model.Name;
	        category.ShortName = model.ShortName;
            category.Priority = model.Priority;
            category.Description = model.Description;
            await categoryRepository.SaveAsync(category);
            return model;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Category category = await categoryRepository.FindOneAsync(x => x.CategoryId == id);
            if (category == null)
            {
                throw new InfinityNotFoundException("category not found");
            }
            else
            {
                return await categoryRepository.DeleteAsync(category);
            }
        }

        public async Task<List<SelectModel>> GetCategorySelectModels()
        {
            ICollection<Category> categories = await categoryRepository.FilterAsync(x => x.IsActive);
            List<Category> query = categories.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.CategoryId
            }).ToList();
            return selectModels;
        }


    }
}
