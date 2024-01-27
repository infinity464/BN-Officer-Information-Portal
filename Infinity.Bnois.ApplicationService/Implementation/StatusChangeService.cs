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
    public class StatusChangeService : IStatusChangeService
    {
        private readonly IBnoisRepository<StatusChange> statusChangeRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<PhysicalCondition> physicalConditionRepository;
       
        public StatusChangeService(IBnoisRepository<StatusChange> statusChangeRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository,
            IBnoisRepository<PhysicalCondition> physicalConditionRepository)
        {
            this.statusChangeRepository = statusChangeRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.physicalConditionRepository = physicalConditionRepository;
           
        }

        //public List<StatusChangeModel> GetStatusChanges(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<StatusChange> statusChanges = statusChangeRepository.FilterWithInclude(x => x.IsActive
        //        && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee");
        //    total = statusChanges.Count();
        //    statusChanges = statusChanges.OrderByDescending(x => x.StatusChangeId).Skip((pn - 1) * ps).Take(ps);
        //    List<StatusChangeModel> models = ObjectConverter<StatusChange, StatusChangeModel>.ConvertList(statusChanges.ToList()).ToList();


            

        //    return models;
        //}

        public async Task<StatusChangeModel> GetStatusChange(int id)
        {
            if (id <= 0)
            {
                return new StatusChangeModel();
            }
            StatusChange statusChange = await statusChangeRepository.FindOneAsync(x => x.StatusChangeId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (statusChange == null)
            {
                throw new InfinityNotFoundException("Status not found");
            }
            StatusChangeModel model = ObjectConverter<StatusChange, StatusChangeModel>.Convert(statusChange);
            return model;
        }


        //public async Task<StatusChangeModel> SaveStatusChange(int id, StatusChangeModel model)
        //{
            
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Training Result  data missing");
        //    }

        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    StatusChange statusChange = ObjectConverter<StatusChangeModel, StatusChange>.Convert(model);
        //    if (id > 0)
        //    {
        //        statusChange = await statusChangeRepository.FindOneAsync(x => x.StatusChangeId == id);
        //        if (statusChange == null)
        //        {
        //            throw new InfinityNotFoundException("Status not found !");
        //        }

        //        statusChange.ModifiedDate = DateTime.Now;
        //        statusChange.ModifiedBy = userId;
        //    }
        //    else
        //    {
        //        statusChange.IsActive = true;
        //        statusChange.CreatedDate = DateTime.Now;
        //        statusChange.CreatedBy = userId;
        //    }
        //    statusChange.EmployeeId = model.EmployeeId;
        //    statusChange.PreviousId = model.PreviousId;
        //    statusChange.NewId = model.NewId;
        //    statusChange.Date = model.Date;
        //    statusChange.StatusType = model.StatusType;

        //    statusChange.Employee = null;
        //   int result=  await statusChangeRepository.SaveAsync(statusChange);
        //    model.StatusChangeId = statusChange.StatusChangeId;

        //    if (result==1)
        //    {
        //        if (model.Date <= DateTime.Now)
        //        {
        //            if (model.StatusType == 1)
        //            {
        //                var physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
        //                physicalCondition.MedicalCategoryId = model.NewId;
        //                await physicalConditionRepository.SaveAsync(physicalCondition);
        //            }
        //            else if (model.StatusType == 2)
        //            {
        //                var physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
        //                physicalCondition.EyeVisionId = model.NewId;
        //                await physicalConditionRepository.SaveAsync(physicalCondition);


        //            }
        //            else if (model.StatusType == 3)
        //            {
        //                var employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
        //                employeeGeneral.CommissionTypeId = model.NewId;
        //                await employeeGeneralRepository.SaveAsync(employeeGeneral);

        //            }
        //            else if (model.StatusType == 4)
        //            {
        //                var employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
        //                employeeGeneral.BranchId = model.NewId;
        //                await employeeGeneralRepository.SaveAsync(employeeGeneral);

        //            }

        //        }
        //    }

            

        //    return model;
        //}


        public async Task<bool> DeleteStatusChange(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            StatusChange statusChange = await statusChangeRepository.FindOneAsync(x => x.StatusChangeId == id);
            if (statusChange == null)
            {
                throw new InfinityNotFoundException("Status not found");
            }
            else
            {
                var result = await statusChangeRepository.DeleteAsync(statusChange);
             
                return result;
            }
        }

        public async Task<int> GetEmployeeCommissionType(int employeeId)
        {
           
            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }


            return employeeGeneral.CommissionTypeId;
        }

        public async Task<int> GetEmployeeBranch(int employeeId)
        {

            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }


            return employeeGeneral.BranchId;
        }

        public async Task<int> GetEmployeeMedicalCategory(int employeeId)
        {

            PhysicalCondition physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (physicalCondition == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }


            return physicalCondition.MedicalCategoryId;
        }

        public async Task<int> GetEmployeeEyeVision(int employeeId)
        {

            PhysicalCondition physicalCondition = await physicalConditionRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (physicalCondition == null)
            {
                throw new InfinityNotFoundException("Employee not found");
            }


            return physicalCondition.EyeVisionId;
        }

        public List<StatusChangeModel> GetStatusChanges(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }

        public Task<StatusChangeModel> SaveStatusChange(int v, StatusChangeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
