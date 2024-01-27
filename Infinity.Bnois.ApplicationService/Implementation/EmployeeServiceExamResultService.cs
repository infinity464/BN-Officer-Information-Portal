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
    public class EmployeeServiceExamResultService : IEmployeeServiceExamResultService
    {
        private readonly IBnoisRepository<EmployeeServiceExamResult> employeeServiceExamResultRepository;
        public EmployeeServiceExamResultService(IBnoisRepository<EmployeeServiceExamResult> employeeServiceExamResultRepository)
        {
            this.employeeServiceExamResultRepository = employeeServiceExamResultRepository;
        }

        //public List<EmployeeServiceExamResultModel> GetEmployeeServiceExamResults(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeServiceExamResult> employeeServiceExamResults = employeeServiceExamResultRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "ServiceExam","ServiceExamCategory");
        //    total = employeeServiceExamResults.Count();
        //    employeeServiceExamResults = employeeServiceExamResults.OrderByDescending(x => x.EmployeeServiceExamResultId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeServiceExamResultModel> models = ObjectConverter<EmployeeServiceExamResult, EmployeeServiceExamResultModel>.ConvertList(employeeServiceExamResults.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeServiceExamResultModel> GetEmployeeServiceExamResult(int id)
        {
            if (id <= 0)
            {
                return new EmployeeServiceExamResultModel();
            }
            EmployeeServiceExamResult employeeServiceExamResult = await employeeServiceExamResultRepository.FindOneAsync(x => x.EmployeeServiceExamResultId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeeServiceExamResult == null)
            {
                throw new InfinityNotFoundException("Employee Service Exam Result not found");
            }
            EmployeeServiceExamResultModel model = ObjectConverter<EmployeeServiceExamResult, EmployeeServiceExamResultModel>.Convert(employeeServiceExamResult);
            return model;
        }



        public async Task<EmployeeServiceExamResultModel> SaveEmployeeServiceExamResult(int id, EmployeeServiceExamResultModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee Service Exam Result  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();


            bool isExist = await employeeServiceExamResultRepository.ExistsAsync(x => x.EmployeeId ==model.EmployeeId && x.ServiceExamCategoryId==model.ServiceExamCategoryId && x.PassFailResult==true && x.EmployeeServiceExamResultId != id);

            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer already qualified. !");
            }


            EmployeeServiceExamResult employeeServiceExamResult = ObjectConverter<EmployeeServiceExamResultModel, EmployeeServiceExamResult>.Convert(model);
            if (id > 0)
            {
                employeeServiceExamResult = await employeeServiceExamResultRepository.FindOneAsync(x => x.EmployeeServiceExamResultId == id);
                if (employeeServiceExamResult == null)
                {
                    throw new InfinityNotFoundException("Employee Service Exam Result not found !");
                }

                employeeServiceExamResult.ModifiedDate = DateTime.Now;
                employeeServiceExamResult.ModifiedBy = userId;
            }
            else
            {
                employeeServiceExamResult.IsActive = true;
                employeeServiceExamResult.CreatedDate = DateTime.Now;
                employeeServiceExamResult.CreatedBy = userId;
            }
            employeeServiceExamResult.EmployeeId = model.EmployeeId;
            employeeServiceExamResult.ServiceExamCategoryId = model.ServiceExamCategoryId;
            employeeServiceExamResult.ServiceExamId = model.ServiceExamId;
            employeeServiceExamResult.ExamDate = model.ExamDate;
            employeeServiceExamResult.NumberOfSubject = model.NumberOfSubject;
            employeeServiceExamResult.AttTime = model.AttTime;
            employeeServiceExamResult.PassFailResult = model.PassFailResult;
            employeeServiceExamResult.IsExempted = model.IsExempted;
            employeeServiceExamResult.ExemptedDate = model.ExemptedDate;

            employeeServiceExamResult.RankId = model.Employee.RankId;
            employeeServiceExamResult.TransferId = model.Employee.TransferId;

            if (model.IsBackLog)
            {
                employeeServiceExamResult.RankId = model.RankId;
                employeeServiceExamResult.TransferId = model.TransferId;
            }
            //employeeServiceExamResult.Employee = null;
            await employeeServiceExamResultRepository.SaveAsync(employeeServiceExamResult);
            model.EmployeeServiceExamResultId = employeeServiceExamResult.EmployeeServiceExamResultId;
            return model;
        }


        public async Task<bool> DeleteEmployeeServiceExamResult(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeServiceExamResult employeeServiceExamResult = await employeeServiceExamResultRepository.FindOneAsync(x => x.EmployeeServiceExamResultId == id);
            if (employeeServiceExamResult == null)
            {
                throw new InfinityNotFoundException("Employee Service Exam Result not found");
            }
            else
            {
                return await employeeServiceExamResultRepository.DeleteAsync(employeeServiceExamResult);
            }
        }

        public List<EmployeeServiceExamResultModel> GetEmployeeServiceExamResults(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
