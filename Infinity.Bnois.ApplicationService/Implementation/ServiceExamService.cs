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
   public class ServiceExamService: IServiceExamService
    {

        private readonly IBnoisRepository<ServiceExam> serviceExamRepository;
        public ServiceExamService(IBnoisRepository<ServiceExam> serviceExamRepository)
        {
            this.serviceExamRepository = serviceExamRepository;
        }

        //public List<ServiceExamModel> GetServiceExams(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<ServiceExam> serviceExams = serviceExamRepository.FilterWithInclude(x => x.IsActive
        //        && ((x.Name.Contains(qs) || String.IsNullOrEmpty(qs)) ||
        //        (x.ServiceExamCategory.ExamName.Contains(qs) || String.IsNullOrEmpty(qs))), "ServiceExamCategory","Branch");
        //    total = serviceExams.Count();
        //    serviceExams = serviceExams.OrderByDescending(x => x.ServiceExamId).Skip((pn - 1) * ps).Take(ps);
        //    List<ServiceExamModel> models = ObjectConverter<ServiceExam, ServiceExamModel>.ConvertList(serviceExams.ToList()).ToList();
        //    return models;
        //}

        public async Task<ServiceExamModel> GetServiceExam(int id)
        {
            if (id <= 0)
            {
                return new ServiceExamModel();
            }
            ServiceExam serviceExam = await serviceExamRepository.FindOneAsync(x => x.ServiceExamId == id);
            if (serviceExam == null)
            {
                throw new InfinityNotFoundException("Service Exam not found");
            }
            ServiceExamModel model = ObjectConverter<ServiceExam, ServiceExamModel>.Convert(serviceExam);
            return model;
        }

    
        public async Task<ServiceExamModel> SaveServiceExam(int id, ServiceExamModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Service Exam data missing");
            }

            bool isExistData = serviceExamRepository.Exists(x => x.ServiceExamCategoryId == model.ServiceExamCategoryId && x.Name == model.Name && x.ServiceExamId != id);
            if (isExistData)
            {
                throw new InfinityInvalidDataException("Data already exists !");
            }
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            ServiceExam serviceExam = ObjectConverter<ServiceExamModel, ServiceExam>.Convert(model);
            if (id > 0)
            {
                serviceExam = await serviceExamRepository.FindOneAsync(x => x.ServiceExamId == id);
                if (serviceExam == null)
                {
                    throw new InfinityNotFoundException("Service Exam not found !");
                }

                serviceExam.ModifiedDate = DateTime.Now;
                serviceExam.ModifiedBy = userId;
            }
            else
            {
                serviceExam.IsActive = true;
                serviceExam.CreatedDate = DateTime.Now;
                serviceExam.CreatedBy = userId;
            }
            serviceExam.Name = model.Name;
            serviceExam.ShortName = model.ShortName;
            serviceExam.ServiceExamCategoryId = model.ServiceExamCategoryId;
            serviceExam.BranchId = model.BranchId;
            serviceExam.NOS = model.NOS;
            serviceExam.AttTime = model.AttTime;
            await serviceExamRepository.SaveAsync(serviceExam);
            model.ServiceExamId = serviceExam.ServiceExamId;
            return model;
        }


        public async Task<bool> DeleteServiceExam(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            ServiceExam serviceExam = await serviceExamRepository.FindOneAsync(x => x.ServiceExamId == id);
            if (serviceExam == null)
            {
                throw new InfinityNotFoundException("Service Exam not found");
            }
            else
            {
                return await serviceExamRepository.DeleteAsync(serviceExam);
            }
        }


        public async Task<List<SelectModel>> GetServiceExamSelectModelsByServiceExamCategory(int id)
        {
            ICollection<ServiceExam> serviceExams = await serviceExamRepository.FilterAsync(x => x.IsActive && x.ServiceExamCategoryId == id);
            List<SelectModel> selectModels = serviceExams.OrderBy(x => x.Name).Select(x => new SelectModel
            {
                Text = x.Name,
                Value = x.ServiceExamId
            }).ToList();
            return selectModels;

        }

        public List<ServiceExamModel> GetServiceExams(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
