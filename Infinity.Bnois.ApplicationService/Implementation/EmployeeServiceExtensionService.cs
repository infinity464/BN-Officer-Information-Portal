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
    public class EmployeeServiceExtensionService : IEmployeeServiceExtensionService
    {
        private readonly IBnoisRepository<EmployeeServiceExt> employeeServiceExtensionRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        public EmployeeServiceExtensionService(IBnoisRepository<EmployeeServiceExt> employeeServiceExtensionRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository)
        {
            this.employeeServiceExtensionRepository = employeeServiceExtensionRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
        }


        //public List<EmployeeServiceExtensionModel> GetEmployeeServiceExtensions(int ps, int pn, string qs, out int total)
        //{
        //    IQueryable<EmployeeServiceExt> employeeServiceExtensions = employeeServiceExtensionRepository.FilterWithInclude(x => x.IsActive && (x.Employee.PNo == (qs) || x.Employee.FullNameEng.Contains(qs) || String.IsNullOrEmpty(qs)), "Employee", "Employee.Rank", "Employee.Batch");
        //    total = employeeServiceExtensions.Count();
        //    employeeServiceExtensions = employeeServiceExtensions.OrderByDescending(x => x.EmpSvrExtId).ThenBy(x=>x.Employee.PNo).Skip((pn - 1) * ps).Take(ps);
        //    List<EmployeeServiceExtensionModel> models = ObjectConverter<EmployeeServiceExt, EmployeeServiceExtensionModel>.ConvertList(employeeServiceExtensions.ToList()).ToList();
        //    return models;
        //}

        public async Task<EmployeeServiceExtensionModel> GetEmployeeServiceExtension(int id)
        {
            if (id <= 0)
            {
                return new EmployeeServiceExtensionModel()
                {
                    Employee = new EmployeeModel()
                };
            }
            EmployeeServiceExt employeeServiceExtension = await employeeServiceExtensionRepository.FindOneAsync(x => x.EmpSvrExtId == id, new List<string> { "Employee.Rank", "Employee.Batch", "Employee.EmployeeGeneral" });
            if (employeeServiceExtension == null)
            {
                throw new InfinityNotFoundException("Officers Service Extension not found");
            }
            return ObjectConverter<EmployeeServiceExt, EmployeeServiceExtensionModel>.Convert(employeeServiceExtension); ;
        }

        public async Task<EmployeeServiceExtensionModel> SaveEmployeeServiceExtension(int id, EmployeeServiceExtensionModel model)
        {

         
            model.Employee = null;
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Employee Service Extension data missing");
            }


            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            EmployeeServiceExt employeeServiceExtension = ObjectConverter<EmployeeServiceExtensionModel, EmployeeServiceExt>.Convert(model);
            if (id > 0)
            {
                employeeServiceExtension = await employeeServiceExtensionRepository.FindOneAsync(x => x.EmpSvrExtId == id);
                if (employeeServiceExtension == null)
                {
                    throw new InfinityNotFoundException("Employee Service Extension not found !");
                }

                employeeServiceExtension.ModifiedDate = DateTime.Now;
                employeeServiceExtension.ModifiedBy = userId;
            }
            else
            {
                employeeServiceExtension.IsActive = true;
                employeeServiceExtension.CreatedDate = DateTime.Now;
                employeeServiceExtension.CreatedBy = userId;


            }

            employeeServiceExtension.EmployeeId = model.EmployeeId;
            employeeServiceExtension.RetirementDate = model.RetirementDate;
            employeeServiceExtension.ExtDays = model.ExtDays;
            employeeServiceExtension.ExtMonth = model.ExtMonth;
            employeeServiceExtension.ExtLprDate = model.RetirementDate.AddDays(model.ExtDays).AddMonths(model.ExtMonth);
            employeeServiceExtension.Remarks = model.Remarks;

            // Employee Contract End Date Update
            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Officer General Information data missing !");
            }
            if (employeeGeneral.IsContract)
            {
                employeeGeneral.ContractEndDate = employeeServiceExtension.ExtLprDate;
            }
            else
            {
                employeeGeneral.LprDate = employeeServiceExtension.ExtLprDate;
            }


            await employeeServiceExtensionRepository.SaveAsync(employeeServiceExtension);
            await employeeGeneralRepository.SaveAsync(employeeGeneral);
            model.EmpSvrExtId = employeeServiceExtension.EmpSvrExtId;
            return model;
        }

        public async Task<bool> DeleteEmployeeServiceExtension(int id)
        {
            if (id < 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            EmployeeServiceExt employeeServiceExtension = await employeeServiceExtensionRepository.FindOneAsync(x => x.EmpSvrExtId == id);
            if (employeeServiceExtension == null)
            {
                throw new InfinityNotFoundException("Employee Service Extension not found");
            }
            else
            {
                return await employeeServiceExtensionRepository.DeleteAsync(employeeServiceExtension);
            }
        }

        public List<EmployeeServiceExtensionModel> GetEmployeeServiceExtensions(int ps, int pn, string qs, out int total)
        {
            throw new NotImplementedException();
        }
    }
}