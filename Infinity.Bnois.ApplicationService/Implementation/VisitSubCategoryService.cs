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
   public class VisitSubCategoryService: IVisitSubCategoryService
    {

        private readonly IBnoisRepository<VisitSubCategory> visitSubCategoryRepository;
        public VisitSubCategoryService(IBnoisRepository<VisitSubCategory> visitSubCategoryRepository)
        {
            this.visitSubCategoryRepository = visitSubCategoryRepository;
        }

        //public List<VisitSubCategoryModel> GetVisitSubCategories(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<VisitSubCategory> visitSubCategories = visitSubCategoryRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.VisitCategory.Name.Contains(qs) || String.IsNullOrEmpty(qs))), "VisitCategory");
        //    total = visitSubCategories.Count();
        //    visitSubCategories = visitSubCategories.OrderByDescending(x => x.VisitSubCategoryId).Skip((pn - 1) * ps).Take(ps);
        //    List<VisitSubCategoryModel> models = ObjectConverter<VisitSubCategory, VisitSubCategoryModel>.ConvertList(visitSubCategories.ToList()).ToList();
        //    return models;
        //}

        public async Task<VisitSubCategoryModel> GetVisitSubCategory(int id)
        {
            if (id <= 0)
            {
                return new VisitSubCategoryModel();
            }
            VisitSubCategory visitSubCategory = await visitSubCategoryRepository.FindOneAsync(x => x.VisitSubCategoryId == id);
            if (visitSubCategory == null)
            {
                throw new InfinityNotFoundException("Visit Sub Category not found");
            }
            VisitSubCategoryModel model = ObjectConverter<VisitSubCategory, VisitSubCategoryModel>.Convert(visitSubCategory);
            return model;
        }

    
        public async Task<VisitSubCategoryModel> SaveVisitSubCategory(int id, VisitSubCategoryModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("VisitSubCategory data missing");
            }

            bool isExistData = visitSubCategoryRepository.Exists(x => x.VisitCategoryId == model.VisitCategoryId && x.Name == model.Name && x.VisitSubCategoryId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            VisitSubCategory visitSubCategory = ObjectConverter<VisitSubCategoryModel, VisitSubCategory>.Convert(model);
            if (id > 0)
            {
                visitSubCategory = await visitSubCategoryRepository.FindOneAsync(x => x.VisitSubCategoryId == id);
                if (visitSubCategory == null)
                {
                    throw new InfinityNotFoundException("Visit Sub Category not found !");
                }

                visitSubCategory.ModifiedDate = DateTime.Now;
                visitSubCategory.ModifiedBy = userId;
            }
            else
            {
                visitSubCategory.IsActive = true;
                visitSubCategory.CreatedDate = DateTime.Now;
                visitSubCategory.CreatedBy = userId;
            }
            visitSubCategory.Name = model.Name;
            visitSubCategory.Remarks = model.Remarks;
            visitSubCategory.VisitCategoryId = model.VisitCategoryId;
            await visitSubCategoryRepository.SaveAsync(visitSubCategory);
            model.VisitSubCategoryId = visitSubCategory.VisitSubCategoryId;
            return model;
        }


        public async Task<bool> DeleteVisitSubCategory(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            VisitSubCategory visitSubCategory = await visitSubCategoryRepository.FindOneAsync(x => x.VisitSubCategoryId == id);
            if (visitSubCategory == null)
            {
                throw new InfinityNotFoundException("Visit Sub Category not found");
            }
            else
            {
                return await visitSubCategoryRepository.DeleteAsync(visitSubCategory);
            }
        }


        public async Task<List<SelectModel>> GetVisitSubCategorySelectModelsByVisitCategory(int id)
        {
            ICollection<VisitSubCategory> visitSubCategories = await visitSubCategoryRepository.FilterAsync(x => x.IsActive && x.VisitCategoryId == id);
            List<SelectModel> selectModels = visitSubCategories.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.VisitSubCategoryId
            }).ToList();
            return selectModels;

        }


        public async Task<List<SelectModel>> GetVisitSubCategorySelectModels()
        {
            ICollection<VisitSubCategory> visitSubCategories = await visitSubCategoryRepository.FilterAsync(x => x.IsActive );
            List<SelectModel> selectModels = visitSubCategories.OrderBy(x=>x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.VisitSubCategoryId
            }).ToList();
            return selectModels;

        }

        public List<VisitSubCategoryModel> GetVisitSubCategories(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
