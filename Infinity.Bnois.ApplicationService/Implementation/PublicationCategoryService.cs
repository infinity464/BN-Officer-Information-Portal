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
   

    public class PublicationCategoryService : IPublicationCategoryService
    {
        private readonly IBnoisRepository<PublicationCategory> publicationCategoryRepository;
        public PublicationCategoryService(IBnoisRepository<PublicationCategory> publicationCategoryRepository)
        {
            this.publicationCategoryRepository = publicationCategoryRepository;
        }

        public List<PublicationCategoryModel> GetPublicationCategories(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<PublicationCategory> publicationCategories = publicationCategoryRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.Name.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = publicationCategories.Count();
            publicationCategories = publicationCategories.OrderByDescending(x => x.PublicationCategoryId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<PublicationCategoryModel> models = ObjectConverter<PublicationCategory, PublicationCategoryModel>.ConvertList(publicationCategories.ToList()).ToList();
            return models;
        }

        public async Task<PublicationCategoryModel> GetPublicationCategory(int  id)
        {
            if (id <= 0)
            {
                return new PublicationCategoryModel();
            }
            PublicationCategory publicationCategory = await publicationCategoryRepository.FindOneAsync(x => x.PublicationCategoryId == id);

            if (publicationCategory == null)
            {
                throw new InfinityNotFoundException("Publication Category not found!");
            }
            PublicationCategoryModel model = ObjectConverter<PublicationCategory, PublicationCategoryModel>.Convert(publicationCategory);
            return model;
        }

        //public async Task<PublicationCategoryModel> SavePublicationCategory(int id, PublicationCategoryModel model)
        //{
        //    bool isExist = await publicationCategoryRepository.ExistsAsync(x => (x.Name == model.Name) && x.PublicationCategoryId != model.PublicationCategoryId);
        //    if (isExist)
        //    {
        //        throw new InfinityInvalidDataException("Publication Category data already exist !");
        //    }
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Publication Category data missing!");
        //    }

        //    PublicationCategory publicationCategory = ObjectConverter<PublicationCategoryModel, PublicationCategory>.Convert(model);

        //    if (id > 0)
        //    {
        //        publicationCategory = await publicationCategoryRepository.FindOneAsync(x => x.PublicationCategoryId == id);
        //        if (publicationCategory == null)
        //        {
        //            throw new InfinityNotFoundException("Publication Category not found!");
        //        }
        //        publicationCategory.ModifiedDate = DateTime.Now;
        //        publicationCategory.ModifiedBy = model.ModifiedBy;
        //    }
        //    else
        //    {
        //        publicationCategory.CreatedBy = model.CreatedBy;
        //        publicationCategory.CreatedDate = DateTime.Now;
        //        publicationCategory.IsActive = true;
        //    }
        //    publicationCategory.Name = model.Name;
        //    publicationCategory.Remarks = model.Remarks;
        //    publicationCategory.GoToTrace = model.GoToTrace;
        //    await publicationCategoryRepository.SaveAsync(publicationCategory);
        //    model.PublicationCategoryId = publicationCategory.PublicationCategoryId;
        //    return model;
        //}

        public async Task<bool> DeletePublicationCategory(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            PublicationCategory publicationCategory = await publicationCategoryRepository.FindOneAsync(x => x.PublicationCategoryId == id);
            if (publicationCategory == null)
            {
                throw new InfinityNotFoundException("Publication Category not found!");
            }
            else
            {
                return await publicationCategoryRepository.DeleteAsync(publicationCategory);
            }
        }

        public async Task<List<SelectModel>> GetPublicationCategorySelectModels()
        {
            ICollection<PublicationCategory> models = await publicationCategoryRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.Name).Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.PublicationCategoryId
            }).ToList();
        }

        //public async Task<List<SelectModel>> GetTracePublicationCategorySelectModels()
        //{
        //    ICollection<PublicationCategory> models = await publicationCategoryRepository.FilterAsync(x => x.IsActive && x.GoToTrace);
        //    return models.OrderBy(x => x.Name).Select(x => new SelectModel()
        //    {
        //        Text = x.Name,
        //        Value = x.PublicationCategoryId
        //    }).ToList();
        //}

        public Task<PublicationCategoryModel> SavePublicationCategory(int id, PublicationCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectModel>> GetTracePublicationCategorySelectModels()
        {
            throw new NotImplementedException();
        }
    }
}