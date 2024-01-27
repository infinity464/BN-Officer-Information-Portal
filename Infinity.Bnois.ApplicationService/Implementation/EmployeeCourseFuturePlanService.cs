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
    public class EmployeeCourseFuturePlanService : IEmployeeCourseFuturePlanService
    {
        private readonly IBnoisRepository<EmployeeCourseFuturePlan> employeeCourseFuturePlanRepository;
        public EmployeeCourseFuturePlanService(IBnoisRepository<EmployeeCourseFuturePlan> employeeCourseFuturePlanRepository)
        {
            this.employeeCourseFuturePlanRepository = employeeCourseFuturePlanRepository;
        }

        //public List<EmployeeCourseFuturePlanModel> GetEmployeeCourseFuturePlans(string pNo)
        //{
        //    IQueryable<EmployeeCourseFuturePlan> employeeCourseFuturePlans = employeeCourseFuturePlanRepository.FilterWithInclude(x => x.IsActive && x.Employee.PNo == pNo , "Employee", "Course","CourseCategory","CourseSubCategory");
        //    List<EmployeeCourseFuturePlanModel> models = ObjectConverter<EmployeeCourseFuturePlan, EmployeeCourseFuturePlanModel>.ConvertList(employeeCourseFuturePlans.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeCourseFuturePlanModel> GetEmployeeCourseFuturePlan(int id)
        {
            if (id <= 0)
            {
                return new EmployeeCourseFuturePlanModel();
            }
            EmployeeCourseFuturePlan employeeCourseFuturePlan = await employeeCourseFuturePlanRepository.FindOneAsync(x => x.EmployeeCoursePlanId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeeCourseFuturePlan == null)
            {
                throw new InfinityNotFoundException("Course Future Plan not found");
            }
            EmployeeCourseFuturePlanModel model = ObjectConverter<EmployeeCourseFuturePlan, EmployeeCourseFuturePlanModel>.Convert(employeeCourseFuturePlan);
            return model;
        }

        public async Task<EmployeeCourseFuturePlanModel> SaveEmployeeCourseFuturePlan(int id, EmployeeCourseFuturePlanModel model)
        {
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Course Future Plan  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeCourseFuturePlan employeeCourseFuturePlan = ObjectConverter<EmployeeCourseFuturePlanModel, EmployeeCourseFuturePlan>.Convert(model);
            if (id > 0)
            {
                employeeCourseFuturePlan = await employeeCourseFuturePlanRepository.FindOneAsync(x => x.EmployeeCoursePlanId == id);
                if (employeeCourseFuturePlan == null)
                {
                    throw new InfinityNotFoundException("Course Future Plan not found !");
                }

                employeeCourseFuturePlan.ModifiedDate = DateTime.Now;
                employeeCourseFuturePlan.ModifiedBy = userId;
            }
            else
            {
                employeeCourseFuturePlan.IsActive = true;
                employeeCourseFuturePlan.CreatedDate = DateTime.Now;
                employeeCourseFuturePlan.CreatedBy = userId;
            }
            employeeCourseFuturePlan.EmployeeId = model.EmployeeId;
            employeeCourseFuturePlan.CourseCategoryId = model.CourseCategoryId;
            employeeCourseFuturePlan.CourseSubCategoryId = model.CourseSubCategoryId;
            employeeCourseFuturePlan.CoureseId = model.CoureseId;
            employeeCourseFuturePlan.IsMandatory = model.IsMandatory;
            employeeCourseFuturePlan.PlanDate = model.PlanDate;
            employeeCourseFuturePlan.EndDate = model.EndDate;
            employeeCourseFuturePlan.Remarks = model.Remarks;

            await employeeCourseFuturePlanRepository.SaveAsync(employeeCourseFuturePlan);
            model.EmployeeCoursePlanId = employeeCourseFuturePlan.EmployeeCoursePlanId;
            return model;
        }


        public async Task<bool> DeleteEmployeeCourseFuturePlan(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeCourseFuturePlan employeeCourseFuturePlan = await employeeCourseFuturePlanRepository.FindOneAsync(x => x.EmployeeCoursePlanId == id);
            if (employeeCourseFuturePlan == null)
            {
                throw new InfinityNotFoundException("Course Future Plan not found");
            }
            else
            {
                return await employeeCourseFuturePlanRepository.DeleteAsync(employeeCourseFuturePlan);
            }
        }

        public List<EmployeeCourseFuturePlanModel> GetEmployeeCourseFuturePlans(string pNo)
        {
            throw new NotImplementedException();
        }
    }
}
