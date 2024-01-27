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
    public class RetiredEmployeeService : IRetiredEmployeeService
    {
        private readonly IBnoisRepository<RetiredEmployee> retiredEmployeeRepository;
        private readonly IBnoisRepository<RetiredEmpCountry> retiredEmpCountryRepository;
        private readonly IBnoisRepository<RetiredEmpCertificate> retiredEmpCertificateRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;

        public RetiredEmployeeService(IBnoisRepository<RetiredEmployee> retiredEmployeeRepository,
            IBnoisRepository<RetiredEmpCountry> retiredEmpCountryRepository,
            IBnoisRepository<RetiredEmpCertificate> retiredEmpCertificateRepository,
            IBnoisRepository<Employee> employeeRepository)
        {
            this.retiredEmployeeRepository = retiredEmployeeRepository;
            this.retiredEmpCountryRepository = retiredEmpCountryRepository;
            this.retiredEmpCertificateRepository = retiredEmpCertificateRepository;
            this.employeeRepository = employeeRepository;
        }



        public List<EmployeeModel> GetRetiredEmployees(int ps, int pn, string qs, out int total)
        {
            IQueryable<Employee> employees = employeeRepository.FilterWithInclude(x => x.Active && x.EmployeeStatusId==(int)OfficerCurrentStatus.Retired && (x.PNo==(qs) ||
                                                                                                    x.FullNameEng.Contains(qs) ||
                                                                                                   
                                                                                                    String.IsNullOrEmpty(qs)), "Batch", "Gender", "Rank");
            total = employees.Count();
            employees = employees.OrderByDescending(x => x.PNo).Skip((pn - 1) * ps).Take(ps);
            List<EmployeeModel> models = ObjectConverter<Employee, EmployeeModel>.ConvertList(employees.ToList()).ToList();
            return models;
        }



        public async Task<RetiredEmployeeModel> GetRetiredEmployee(int employeeId)
        {

            if (employeeId <= 0)
            {
                return new RetiredEmployeeModel();
            }
            RetiredEmployee retiredEmployee = await retiredEmployeeRepository.FindOneAsync(x => x.EmployeeId == employeeId, new List<string> { "Employee"});
            if (retiredEmployee == null)
            {
                return new RetiredEmployeeModel();
            }
            int[] countryIds = retiredEmpCountryRepository.Where(x => x.RetiredEmployeeId == retiredEmployee.RetiredEmployeeId).Select(x => x.CountryId).ToArray();
            int[] certificateIds = retiredEmpCertificateRepository.Where(x => x.RetiredEmployeeId == retiredEmployee.RetiredEmployeeId).Select(x => x.CertificateId).ToArray();
            RetiredEmployeeModel model = ObjectConverter<RetiredEmployee, RetiredEmployeeModel>.Convert(retiredEmployee);
            model.CountryIds = countryIds;
            model.CertificateIds = certificateIds;
            return model;
        }

        public Task<RetiredEmployeeModel> SaveRetiredEmployee(int v, RetiredEmployeeModel model)
        {
            throw new NotImplementedException();
        }


        //public async Task<RetiredEmployeeModel> SaveRetiredEmployee(int employeeId, RetiredEmployeeModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("Retired Employee  data missing");
        //    }


        //    string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
        //    RetiredEmployee retiredEmployee = ObjectConverter<RetiredEmployeeModel, RetiredEmployee>.Convert(model);
        //    if (model.RetiredEmployeeId > 0)
        //    {
        //        retiredEmployee = await retiredEmployeeRepository.FindOneAsync(x => x.EmployeeId == employeeId);
        //        if (retiredEmployee == null)
        //        {
        //            throw new InfinityNotFoundException("Retired Employee not found !");
        //        }
        //        ICollection<RetiredEmpCountry> retiredEmpCountries = await retiredEmpCountryRepository.FilterAsync(x => x.RetiredEmployeeId == model.RetiredEmployeeId);
        //        retiredEmpCountryRepository.RemoveRange(retiredEmpCountries);

        //        ICollection<RetiredEmpCertificate> retiredEmpCertificates = await retiredEmpCertificateRepository.FilterAsync(x => x.RetiredEmployeeId == model.RetiredEmployeeId);
        //        retiredEmpCertificateRepository.RemoveRange(retiredEmpCertificates);

        //        retiredEmployee.ModifiedDate = DateTime.Now;
        //        retiredEmployee.ModifiedBy = userId;
        //    }
        //    else
        //    {

        //        retiredEmployee.IsActive = true;
        //        retiredEmployee.CreatedDate = DateTime.Now;
        //        retiredEmployee.CreatedBy = userId;

        //    }
        //    if (model.CountryIds != null)
        //    {
        //        retiredEmployee.RetiredEmpCountry = model.CountryIds.Select(x => new RetiredEmpCountry() { CountryId = x }).ToList();
        //    }
        //    if (model.CertificateIds != null)
        //    {
        //        retiredEmployee.RetiredEmpCertificate = model.CertificateIds.Select(x => new RetiredEmpCertificate() { CertificateId = x }).ToList();
        //    }
        //    retiredEmployee.EmployeeId = employeeId;
        //    retiredEmployee.TsNo = model.TsNo;
        //    retiredEmployee.IsVisitAbroad = model.IsVisitAbroad;
        //    retiredEmployee.IsJobAbroad = model.IsJobAbroad;
        //    retiredEmployee.IsPensionIssued = model.IsPensionIssued;
        //    retiredEmployee.BookNo = model.BookNo;
        //    retiredEmployee.IssueDate = model.IssueDate;

        //    await retiredEmployeeRepository.SaveAsync(retiredEmployee);
        //    model.RetiredEmployeeId = retiredEmployee.RetiredEmployeeId;
        //    return model;
        //}
    }
}
