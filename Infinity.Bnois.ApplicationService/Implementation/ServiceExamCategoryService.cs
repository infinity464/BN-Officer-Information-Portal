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
   

    public class ServiceExamCategoryService : IServiceExamCategoryService
    {
        private readonly IBnoisRepository<ServiceExamCategory> serviceExamCategoryRepository;
        public ServiceExamCategoryService(IBnoisRepository<ServiceExamCategory> serviceExamCategoryRepository)
        {
            this.serviceExamCategoryRepository = serviceExamCategoryRepository;
        }

        public List<ServiceExamCategoryModel> GetServiceExamCategories(int pageSize, int pageNumber, string searchText, out int total)
        {
            IQueryable<ServiceExamCategory> serviceExamCategories = serviceExamCategoryRepository
                .FilterWithInclude(x => x.IsActive
                && ((x.ExamName.Contains(searchText)) || String.IsNullOrEmpty(searchText)));
            total = serviceExamCategories.Count();
            serviceExamCategories = serviceExamCategories.OrderByDescending(x => x.ServiceExamCategoryId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            List<ServiceExamCategoryModel> models = ObjectConverter<ServiceExamCategory, ServiceExamCategoryModel>.ConvertList(serviceExamCategories.ToList()).ToList();
            return models;
        }

        public async Task<ServiceExamCategoryModel> GetServiceExamCategory(int  id)
        {
            if (id <= 0)
            {
                return new ServiceExamCategoryModel();
            }
            ServiceExamCategory serviceExamCategory = await serviceExamCategoryRepository.FindOneAsync(x => x.ServiceExamCategoryId == id);

            if (serviceExamCategory == null)
            {
                throw new InfinityNotFoundException("Service Exam Category not found!");
            }
            ServiceExamCategoryModel model = ObjectConverter<ServiceExamCategory, ServiceExamCategoryModel>.Convert(serviceExamCategory);
            return model;
        }

        public async Task<ServiceExamCategoryModel> SaveServiceExamCategory(int id, ServiceExamCategoryModel model)
        {
            bool isExist = await serviceExamCategoryRepository.ExistsAsync(x => (x.ExamName == model.ExamName) && x.ServiceExamCategoryId != model.ServiceExamCategoryId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Service Exam Category data already exist !");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Service Exam Category data missing!");
            }

            ServiceExamCategory serviceExamCategory = ObjectConverter<ServiceExamCategoryModel, ServiceExamCategory>.Convert(model);

            if (id > 0)
            {
                serviceExamCategory = await serviceExamCategoryRepository.FindOneAsync(x => x.ServiceExamCategoryId == id);
                if (serviceExamCategory == null)
                {
                    throw new InfinityNotFoundException("ServiceExam Category not found!");
                }
                serviceExamCategory.ModifiedDate = DateTime.Now;
                serviceExamCategory.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                serviceExamCategory.CreatedBy = model.CreatedBy;
                serviceExamCategory.CreatedDate = DateTime.Now;
                serviceExamCategory.IsActive = true;
            }
            serviceExamCategory.ExamName = model.ExamName;
            serviceExamCategory.ShortName = model.ShortName;

            await serviceExamCategoryRepository.SaveAsync(serviceExamCategory);
            model.ServiceExamCategoryId = serviceExamCategory.ServiceExamCategoryId;
            return model;
        }

        public async Task<bool> DeleteServiceExamCategory(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ServiceExamCategory serviceExamCategory = await serviceExamCategoryRepository.FindOneAsync(x => x.ServiceExamCategoryId == id);
            if (serviceExamCategory == null)
            {
                throw new InfinityNotFoundException("Service Exam Category not found!");
            }
            else
            {
                return await serviceExamCategoryRepository.DeleteAsync(serviceExamCategory);
            }
        }

        public async Task<List<SelectModel>> GetServiceExamCategorySelectModels()
        {
            ICollection<ServiceExamCategory> models = await serviceExamCategoryRepository.FilterAsync(x => x.IsActive);
            return models.OrderBy(x => x.ExamName).Select(x => new SelectModel()
            {
                Text = x.ExamName,
                Value = x.ServiceExamCategoryId
            }).ToList();
        }

    }
}