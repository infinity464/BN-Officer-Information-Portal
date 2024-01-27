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
    public class EmployeeTransferFuturePlanService : IEmployeeTransferFuturePlanService
    {
        private readonly IBnoisRepository<EmployeeTransferFuturePlan> employeeTransferFuturePlanRepository;
        public EmployeeTransferFuturePlanService(IBnoisRepository<EmployeeTransferFuturePlan> employeeTransferFuturePlanRepository)
        {
            this.employeeTransferFuturePlanRepository = employeeTransferFuturePlanRepository;
        }

        //public List<EmployeeTransferFuturePlanModel> GetEmployeeTransferFuturePlans(string pNo)
        //{
        //    IQueryable<EmployeeTransferFuturePlan> employeeTransferFuturePlans = employeeTransferFuturePlanRepository.FilterWithInclude(x => x.IsActive && x.Employee.PNo==pNo, "Employee", "Office","Pattern","Country","AptNat","AptCat");
          
        //    List<EmployeeTransferFuturePlanModel> models = ObjectConverter<EmployeeTransferFuturePlan, EmployeeTransferFuturePlanModel>.ConvertList(employeeTransferFuturePlans.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeTransferFuturePlanModel> GetEmployeeTransferFuturePlan(int id)
        {
            if (id <= 0)
            {
                return new EmployeeTransferFuturePlanModel();
            }
            EmployeeTransferFuturePlan employeeTransferFuturePlan = await employeeTransferFuturePlanRepository.FindOneAsync(x => x.EmployeeTransferFuturePlanId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeeTransferFuturePlan == null)
            {
                throw new InfinityNotFoundException("Transfer Future Plan not found");
            }
            EmployeeTransferFuturePlanModel model = ObjectConverter<EmployeeTransferFuturePlan, EmployeeTransferFuturePlanModel>.Convert(employeeTransferFuturePlan);
            return model;
        }

        public async Task<EmployeeTransferFuturePlanModel> SaveEmployeeTransferFuturePlan(int id, EmployeeTransferFuturePlanModel model)
        {
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Transfer Future Plan  data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeTransferFuturePlan employeeTransferFuturePlan = ObjectConverter<EmployeeTransferFuturePlanModel, EmployeeTransferFuturePlan>.Convert(model);
            if (id > 0)
            {
                employeeTransferFuturePlan = await employeeTransferFuturePlanRepository.FindOneAsync(x => x.EmployeeTransferFuturePlanId == id);
                if (employeeTransferFuturePlan == null)
                {
                    throw new InfinityNotFoundException("Transfer Future Plan not found !");
                }

                employeeTransferFuturePlan.ModifiedDate = DateTime.Now;
                employeeTransferFuturePlan.ModifiedBy = userId;
            }
            else
            {
                employeeTransferFuturePlan.IsActive = true;
                employeeTransferFuturePlan.CreatedDate = DateTime.Now;
                employeeTransferFuturePlan.CreatedBy = userId;
            }
            employeeTransferFuturePlan.EmployeeId = model.EmployeeId;
            employeeTransferFuturePlan.AptcatId = model.AptcatId;
            employeeTransferFuturePlan.AptnatId = model.AptnatId;
            employeeTransferFuturePlan.PatternId = model.PatternId;
            employeeTransferFuturePlan.CountryId = model.CountryId;
            employeeTransferFuturePlan.OfficeId = model.OfficeId; 
            employeeTransferFuturePlan.IsMandatory = model.IsMandatory;
            employeeTransferFuturePlan.PlanDate = model.PlanDate;
            employeeTransferFuturePlan.EndDate = model.EndDate;
            employeeTransferFuturePlan.Remarks = model.Remarks;

            await employeeTransferFuturePlanRepository.SaveAsync(employeeTransferFuturePlan);
            model.EmployeeTransferFuturePlanId = employeeTransferFuturePlan.EmployeeTransferFuturePlanId;
            return model;
        }


        public async Task<bool> DeleteEmployeeTransferFuturePlan(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeTransferFuturePlan employeeTransferFuturePlan = await employeeTransferFuturePlanRepository.FindOneAsync(x => x.EmployeeTransferFuturePlanId == id);
            if (employeeTransferFuturePlan == null)
            {
                throw new InfinityNotFoundException("Transfer Future Plan not found");
            }
            else
            {
                return await employeeTransferFuturePlanRepository.DeleteAsync(employeeTransferFuturePlan);
            }
        }

        public List<EmployeeTransferFuturePlanModel> GetEmployeeTransferFuturePlans(string pno)
        {
            throw new NotImplementedException();
        }
    }
}
