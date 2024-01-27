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
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IBnoisRepository<SubCategory> subCategoryRepository;
	    private readonly IProcessRepository processRepository;
		public SubCategoryService(IBnoisRepository<SubCategory> subCategoryRepository, IProcessRepository processRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.processRepository = processRepository;
        }

        //public List<SubCategoryModel> GetSubCategories(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<SubCategory> subCategories = subCategoryRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.Category.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "Category");
        //    total = subCategories.Count();
        //    subCategories = subCategories.OrderByDescending(x => x.SubCategoryId).Skip((pn - 1) * ps).Take(ps);
        //    List<SubCategoryModel> models = ObjectConverter<SubCategory, SubCategoryModel>.ConvertList(subCategories.ToList()).ToList();
        //    return models;
        //}

        public async Task<SubCategoryModel> GetSubCategory(int id)
        {
            if (id <= 0)
            {
                return new SubCategoryModel();
            }
            SubCategory subCategory = await subCategoryRepository.FindOneAsync(x => x.SubCategoryId == id, new List<string> {"Category"});
            if (subCategory == null)
            {
                throw new InfinityNotFoundException("SubCategory not found");
            }
            SubCategoryModel model = ObjectConverter<SubCategory, SubCategoryModel>.Convert(subCategory);
            return model;
        }
 
        public async Task<SubCategoryModel> SaveSubCategory(int id, SubCategoryModel model)
        { 
            if (model == null)
            {
                throw new InfinityArgumentMissingException("SubCategory data missing");
            }

            bool isExistData = subCategoryRepository.Exists(x => x.CategoryId == model.CategoryId && x.Name == model.Name && x.SubCategoryId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            SubCategory subCategory = ObjectConverter<SubCategoryModel, SubCategory>.Convert(model);
            if (id > 0)
            {
                subCategory = await subCategoryRepository.FindOneAsync(x => x.SubCategoryId == id);
                if (subCategory == null)
                {
                    throw new InfinityNotFoundException("SubCategory not found !");
                }

                subCategory.ModifiedDate = DateTime.Now;
                subCategory.ModifiedBy = userId;
            }
            else
            {
                subCategory.IsActive = true;
                subCategory.CreatedDate = DateTime.Now;
                subCategory.CreatedBy = userId;
            }
            subCategory.Name = model.Name;
            subCategory.CategoryId = model.CategoryId;
	        subCategory.ShortName = model.ShortName;
	        subCategory.Description = model.Name;
	        subCategory.Prefix = model.Prefix;
	        subCategory.Rank = model.Rank;
	        subCategory.Branch = model.Branch;
	        subCategory.SubBranch = model.SubBranch;
	        subCategory.Course = model.Course;
	        subCategory.Medal = model.Medal;
	        subCategory.Award = model.Award;
	        subCategory.Prefix2 = model.Prefix2;
	        subCategory.NmConEx = model.NmConEx;
	        subCategory.Priority = model.Priority;
	        subCategory.BN = model.BN;
	        subCategory.BNVR = model.BNVR;

			
            await subCategoryRepository.SaveAsync(subCategory);
            model.SubCategoryId = subCategory.SubCategoryId;

	        await processRepository.UpdateNamingConvention(-1);
			return model;
        }


        public async Task<bool> DeleteSubCategory(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            SubCategory subCategory = await subCategoryRepository.FindOneAsync(x => x.SubCategoryId == id);
            if (subCategory == null)
            {
                throw new InfinityNotFoundException("SubCategory not found");
            }
            else
            {
                return await subCategoryRepository.DeleteAsync(subCategory);
            }
        }

        public async Task<List<SelectModel>> GetSubCategorySelectModels()
        {
            ICollection<SubCategory> categories = await subCategoryRepository.FilterAsync(x => x.IsActive);
            List<SubCategory> query = categories.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.SubCategoryId
            }).ToList();
            return selectModels;

        }
      
        public async Task<List<SelectModel>> GetSubCategorySelectModelsByCategory(int categoryId)
        {
            ICollection<SubCategory> categories = await subCategoryRepository.FilterAsync(x => x.CategoryId==categoryId);
            List<SubCategory> query = categories.OrderBy(x => x.Name).ToList();
            List<SelectModel> selectModels = query.Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.SubCategoryId
            }).ToList();
            return selectModels;

        }

        public List<SubCategoryModel> GetSubCategories(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
