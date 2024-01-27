using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class EmployeeSecurityClearanceService: IEmployeeSecurityClearanceService
    {

        private readonly IBnoisRepository<EmployeeSecurityClearance> employeeSecurityClearanceRepository;
        public EmployeeSecurityClearanceService(IBnoisRepository<EmployeeSecurityClearance> employeeSecurityClearanceRepository)
        {
            this.employeeSecurityClearanceRepository = employeeSecurityClearanceRepository;
        }

  

        //public List<EmployeeSecurityClearanceModel> GetEmployeeSecurityClearances(int ps, int pn, string qs, out int total)
        //{

        //    IQueryable<EmployeeSecurityClearance> employeeSecurityClearances = employeeSecurityClearanceRepository
        //        .FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "SecurityClearanceReason");
        //    total = employeeSecurityClearances.Count();
        //    employeeSecurityClearances = employeeSecurityClearances.OrderByDescending(x => x.EmployeeSecurityClearanceId).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeSecurityClearanceModel> models = ObjectConverter<EmployeeSecurityClearance, EmployeeSecurityClearanceModel>.ConvertList(employeeSecurityClearances.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeSecurityClearanceModel> GetEmployeeSecurityClearance(int id)
        {
            if (id == 0)
            {
                return new EmployeeSecurityClearanceModel();
            }
            EmployeeSecurityClearance employeeSecurityClearance = await employeeSecurityClearanceRepository.FindOneAsync(x => x.EmployeeSecurityClearanceId == id, new List<string> { "Employee","Employee.Rank","Employee.Batch" });
            if (employeeSecurityClearance == null)
            {
                throw new InfinityNotFoundException("Employee Security Clearance not found");
            }
            EmployeeSecurityClearanceModel model = ObjectConverter<EmployeeSecurityClearance, EmployeeSecurityClearanceModel>.Convert(employeeSecurityClearance);
            return model;
        }

        public async Task<EmployeeSecurityClearanceModel> SaveEmployeeSecurityClearance(int id, EmployeeSecurityClearanceModel model)
        {

            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee Security Clearance data missing");
            }
            EmployeeSecurityClearance employeeSecurityClearance = ObjectConverter<EmployeeSecurityClearanceModel, EmployeeSecurityClearance>.Convert(model);
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

            if (id > 0)
            {
                employeeSecurityClearance = await employeeSecurityClearanceRepository.FindOneAsync(x => x.EmployeeSecurityClearanceId == id);
                if (employeeSecurityClearance == null)
                {
                    throw new InfinityNotFoundException("Employee Security Clearance not found !");
                }


                employeeSecurityClearance.ModifiedDate = DateTime.Now;
                employeeSecurityClearance.ModifiedBy = userId;
            }
            else
            {
                employeeSecurityClearance.CreatedDate = DateTime.Now;
                employeeSecurityClearance.CreatedBy = userId;
                employeeSecurityClearance.IsActive = true;
            }
            employeeSecurityClearance.EmployeeId = model.EmployeeId;
            employeeSecurityClearance.SecurityClearanceReasonId = model.SecurityClearanceReasonId;
            employeeSecurityClearance.Remarks = model.Remarks;
            employeeSecurityClearance.ClearanceDate =(DateTime) model.ClearanceDate;
            employeeSecurityClearance.Expirydate = model.Expirydate;
            employeeSecurityClearance.RankId = model.Employee.RankId;
            employeeSecurityClearance.TransferId = model.Employee.TransferId;

            if (model.IsBackLog)
            {
                employeeSecurityClearance.RankId = model.RankId;
                employeeSecurityClearance.TransferId = model.TransferId;
            }

            //employeeSecurityClearance.Employee = null;
            await employeeSecurityClearanceRepository.SaveAsync(employeeSecurityClearance);
            model.EmployeeSecurityClearanceId = employeeSecurityClearance.EmployeeSecurityClearanceId;
            return model;
        }

        public async Task<bool> DeleteEmployeeSecurityClearance(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeSecurityClearance employeeSecurityClearance = await employeeSecurityClearanceRepository.FindOneAsync(x => x.EmployeeSecurityClearanceId == id);
            if (employeeSecurityClearance == null)
            {
                throw new InfinityNotFoundException("Employee Security Clearance not found");
            }
            else
            {
                return await employeeSecurityClearanceRepository.DeleteAsync(employeeSecurityClearance);
            }
        }

        public List<EmployeeSecurityClearanceModel> GetEmployeeSecurityClearances(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}