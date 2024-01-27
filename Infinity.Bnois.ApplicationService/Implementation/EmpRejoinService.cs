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
    public class EmpRejoinService : IEmpRejoinService
    {

        private readonly IBnoisRepository<EmpRejoin> empRejoinRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<EmployeeStatus> employeeStatusRepository;
        private readonly IProcessRepository processRepository;

        public EmpRejoinService(IBnoisRepository<EmpRejoin> empRejoinRepository,
            IBnoisRepository<Employee> employeeRepository,
            IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository,
            IBnoisRepository<EmployeeGeneral> employeeGeneralRepository,
            IBnoisRepository<EmployeeStatus> employeeStatusRepository, IProcessRepository processRepository)
        {
            this.empRejoinRepository = empRejoinRepository;
            this.employeeRepository = employeeRepository;
            this.ageServicePolicyRepository = ageServicePolicyRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.employeeStatusRepository = employeeStatusRepository;
            this.processRepository = processRepository;
        }



        //public List<EmpRejoinModel> GetEmpRejoins(int ps, int pn, string qs, out int total)
        //{

        //    IQueryable<EmpRejoin> empRejoins = empRejoinRepository
        //        .FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Rank");
        //    total = empRejoins.Count();
        //    empRejoins = empRejoins.OrderByDescending(x => x.RejoinDate).Skip((pn - 1) * ps).Take(ps);
        //    List<EmpRejoinModel> models = ObjectConverter<EmpRejoin, EmpRejoinModel>.ConvertList(empRejoins.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmpRejoinModel> GetEmpRejoin(int id)
        {
            if (id == 0)
            {
                return new EmpRejoinModel();
            }
            EmpRejoin empRejoin = await empRejoinRepository.FindOneAsync(x => x.EmpRejoinId == id, new List<string> { "Employee", "Employee.Rank", "Employee.Batch" });
            if (empRejoin == null)
            {
                throw new InfinityNotFoundException("Employee Rejoin not found");
            }
            EmpRejoinModel model = ObjectConverter<EmpRejoin, EmpRejoinModel>.Convert(empRejoin);
            return model;
        }

        public async Task<EmpRejoinModel> SaveEmpRejoin(int id, EmpRejoinModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer Rejoin data missing !");
            }
            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (employee == null)
            {
                throw new InfinityArgumentMissingException("Officer not found!");
            }
            if (employee.EmployeeStatusId==(int)OfficerCurrentStatus.Active)
            {
                throw new InfinityInvalidDataException("Officer already Active!");
            }
            EmployeeStatus employeeStatus = await employeeStatusRepository.FindOneAsync(x => x.EmployeeStatusId == employee.SLCode);
            if (employeeStatus == null)
            {
                throw new InfinityNotFoundException("Officer Current Status data missing !");
            }
            if (employee.EmployeeStatusId==employeeStatus.GCode && employee.SLCode==employeeStatus.SLCode && employee.SLCode==(int)OfficerCurrentStatus.Rejoin)
            {
                throw new InfinityInvalidDataException("Officer already Rejoined !");
            }

            EmpRejoin empRejoin = ObjectConverter<EmpRejoinModel, EmpRejoin>.Convert(model);
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();

            if (id > 0)
            {
                empRejoin = await empRejoinRepository.FindOneAsync(x => x.EmpRejoinId == id);
                if (empRejoin == null)
                {
                    throw new InfinityNotFoundException("Officer Rejoin data not found !");
                }


                empRejoin.ModifiedDate = DateTime.Now;
                empRejoin.ModifiedBy = userId;
            }
            else
            {
                empRejoin.CreatedDate = DateTime.Now;
                empRejoin.CreatedBy = userId;
                empRejoin.IsActive = true;
            }
            empRejoin.EmployeeId = model.EmployeeId;
            empRejoin.RankId = model.RankId;
            empRejoin.ReasonToJoin = model.ReasonToJoin;

            //empRejoin.Employee = null;

            await empRejoinRepository.SaveAsync(empRejoin);
            model.EmpRejoinId = empRejoin.EmpRejoinId;

            #region---Employee is updating when Rejoin execution is successful---

            employee.EmployeeStatusId = (int)OfficerCurrentStatus.Active;
            employee.SLCode = (int)OfficerCurrentStatus.Rejoin;
            employee.RankId = model.RankId;
            await employeeRepository.SaveAsync(employee);

            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employee.EmployeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Officer General data not found !");
            }

            if (employeeGeneral.CommissionDate != null)
            {
                GetAgeLimitServiceLimitLprDate(employeeGeneral.CategoryId, employeeGeneral.SubCategoryId, employee.RankId, employeeGeneral.CommissionDate?? new DateTime(), employeeGeneral.DoB, out DateTime ageLimit, out DateTime serviceLimit, out DateTime lprDate);

                employeeGeneral.AgeLimit = ageLimit;
                employeeGeneral.ServiceLimit = serviceLimit;
                employeeGeneral.LprDate = lprDate;
                employeeGeneral.RetireDate = lprDate;
            }
            await employeeGeneralRepository.SaveAsync(employeeGeneral);
            #endregion

            await processRepository.UpdateNamingConvention(empRejoin.EmployeeId);
            return model;
        }

        private void GetAgeLimitServiceLimitLprDate(int category, int? subCategory, int rank, DateTime commissionDate, DateTime dateOfBirth, out DateTime ageLimit, out DateTime serviceLimit, out DateTime LprDate)
        {
            AgeServicePolicy ageServicePolicy = ageServicePolicyRepository.FindOne(x => x.CategoryId == category && x.SubCategoryId == subCategory && x.RankId == rank);

            if (ageServicePolicy == null)
            {
                throw new InfinityNotFoundException("Age Service Policy not found!");
            }
            ageLimit = dateOfBirth.AddYears(ageServicePolicy.AgeLimit);
            serviceLimit = commissionDate.AddYears(ageServicePolicy.ServiceLimit);
            LprDate = ageLimit > serviceLimit ? ageLimit : serviceLimit;

            ageLimit = ageLimit.AddDays(-1);
            serviceLimit = serviceLimit.AddDays(-1);
        }

        public async Task<bool> DeleteEmpRejoin(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmpRejoin empRejoin = await empRejoinRepository.FindOneAsync(x => x.EmpRejoinId == id);
            if (empRejoin == null)
            {
                throw new InfinityNotFoundException("Employee Rejoin not found");
            }
            else
            {
                return await empRejoinRepository.DeleteAsync(empRejoin);
            }
        }

        public List<EmpRejoinModel> GetEmpRejoins(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}