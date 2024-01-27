using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.Configuration;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class EmployeeLprService : IEmployeeLprService
    {
        private readonly IBnoisRepository<EmployeeLpr> employeeLprRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;

        public EmployeeLprService(IBnoisRepository<EmployeeLpr> employeeLprRepository,
            IBnoisRepository<EmployeeGeneral> employeeGeneralRepository,
            IBnoisRepository<Employee> employeeRepository)
        {
            this.employeeLprRepository = employeeLprRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.employeeRepository = employeeRepository;

        }

        public async Task<bool> DeleteEmployeeLpr(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeLpr employeeLpr = await employeeLprRepository.FindOneAsync(x => x.EmpLprId == id);
            if (employeeLpr == null)
            {
                throw new InfinityNotFoundException("LPR/Retirement/Termination not found");
            }
            else
            {
                return await employeeLprRepository.DeleteAsync(employeeLpr);
            }
        }

        public async Task<EmployeeLprModel> GetEmployeeLpr(int id)
        {
            if (id <= 0)
            {
                return new EmployeeLprModel();
            }
            //EmployeeLpr employeeLpr = await employeeLprRepository.FindOneAsync(x => x.EmpLprId == id, new List<string> { "Employee", "Employee1" });
            EmployeeLpr employeeLpr = await employeeLprRepository.FindOneAsync(x => x.EmpLprId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (employeeLpr == null)
            {
                throw new InfinityNotFoundException("Employee Lpr not found");
            }
            EmployeeLprModel model = ObjectConverter<EmployeeLpr, EmployeeLprModel>.Convert(employeeLpr);
            return model;
        }



        public async Task<List<SelectModel>> GetRetirementStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(RetiredStatus)).Cast<RetiredStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        public async Task<List<SelectModel>> GetDurationStatusSelectModels()
        {
            List<SelectModel> selectModels =
                Enum.GetValues(typeof(DurationStatus)).Cast<DurationStatus>()
                    .Select(v => new SelectModel { Text = v.ToString(), Value = Convert.ToInt16(v) })
                    .ToList();
            return selectModels;
        }

        //public List<EmployeeLprModel> GetEmployeeLprs(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeLpr> employeeLprs = employeeLprRepository.FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Employee.Rank", "Employee.Batch", "TerminationType");
        //    total = employeeLprs.Count();
        //    employeeLprs = employeeLprs.OrderByDescending(x => x.EmpLprId).ThenBy(x=>x.Employee.PNo).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeLprModel> models = ObjectConverter<EmployeeLpr, EmployeeLprModel>.ConvertList(employeeLprs.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeLprModel> SaveEmployeeLpr(int id, EmployeeLprModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("LPR/Retirement/Termination  data missing");
            }

            bool isExist = await employeeLprRepository.ExistsAsync(x => x.EmployeeId == model.Employee.EmployeeId && x.EmpLprId != model.EmpLprId);
            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer already exist !");
            }


            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.Employee.EmployeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Officer General Information data not found !");
            }

            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.Employee.EmployeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Officer Information data not found !");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeLpr employeeLpr = ObjectConverter<EmployeeLprModel, EmployeeLpr>.Convert(model);
            if (id > 0)
            {
                employeeLpr = await employeeLprRepository.FindOneAsync(x => x.EmpLprId == id);
                if (employeeLpr == null)
                {
                    throw new InfinityNotFoundException("LPR/Retirement/Termination  not found !");
                }
                employeeLpr.ModifiedDate = DateTime.Now;
                employeeLpr.ModifiedBy = userId;
            }
            else
            {
                employeeLpr.IsActive = true;
                employeeLpr.CreatedDate = DateTime.Now;
                employeeLpr.CreatedBy = userId;
            }
            employeeLpr.EmployeeId = model.Employee.EmployeeId;
            employeeLpr.TerminationTypeId = model.TerminationTypeId;
            employeeLpr.CurrentStatus = model.CurrentStatus;
            employeeLpr.LprDate = model.LprDate;
            employeeLpr.DurationMonth = model.DurationMonth;
            employeeLpr.DurationDay = model.DurationDay;
            employeeLpr.RetireDate = model.RetireDate;
            employeeLpr.TerminationDate = model.TerminationDate;
            employeeLpr.RStatus = model.RStatus;
            employeeLpr.Remarks = model.Remarks;
            //employeeLpr.Employee = null;
            await employeeLprRepository.SaveAsync(employeeLpr);
            model.EmpLprId = employeeLpr.EmpLprId;
            #region---Update Employee and Employee General Update---
            employeeGeneral.LprDate = employeeLpr.LprDate;
            if (employeeGeneral.LprDate <= DateTime.Today)
            {
                employee.EmployeeStatusId = (int)OfficerCurrentStatus.LPR;
                employee.SLCode = (int)OfficerCurrentStatus.LPR;
            }
            employeeGeneral.RetireDate = employeeLpr.RetireDate;
            if (employeeGeneral.RetireDate <= DateTime.Today)
            {
                employee.EmployeeStatusId = (int)OfficerCurrentStatus.Retired;
                employee.SLCode = (int)OfficerCurrentStatus.Retired;
            }
            employeeGeneral.TerminationDate = employeeLpr.TerminationDate;
            if (employeeGeneral.TerminationDate <= DateTime.Today)
            {
                employee.EmployeeStatusId = (int)OfficerCurrentStatus.Terminated;
                employee.SLCode = (int)OfficerCurrentStatus.Terminated;
            }
            await employeeRepository.SaveAsync(employee);
            await employeeGeneralRepository.SaveAsync(employeeGeneral);
            #endregion
            return model;
        }

        public List<EmployeeLprModel> GetEmployeeLprs(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}
