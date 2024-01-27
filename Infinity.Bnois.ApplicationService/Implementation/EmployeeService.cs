using Infinity.Bnois.Api;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IBnoisRepository<OfficerType> officerTypeRepository;
        private readonly IBnoisRepository<EmployeeStatus> employeeStatusRepository;
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository;
        private readonly IProcessRepository processRepository;
        BnoisDbContext db = new BnoisDbContext();
        public EmployeeService(IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository, IBnoisRepository<EmployeeGeneral> employeeGeneralRepository, IBnoisRepository<EmployeeStatus> employeeStatusRepository, IBnoisRepository<Employee> employeeRepository, IBnoisRepository<OfficerType> officerTypeRepository, IProcessRepository processRepository)
        {
            this.employeeRepository = employeeRepository;
            this.officerTypeRepository = officerTypeRepository;
            this.employeeStatusRepository = employeeStatusRepository;
            this.processRepository = processRepository;
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.ageServicePolicyRepository = ageServicePolicyRepository;
        }

        public List<EmployeeModel> GetEmployees(int ps, int pn, string qs, out int total)
        {
            IQueryable<Employee> employees = employeeRepository.FilterWithInclude(x => x.Active && (x.PNo == (qs) ||
            x.FullNameEng.Contains(qs) ||
            String.IsNullOrEmpty(qs)), "Batch", "Gender", "Rank");
            total = employees.Count();
            employees = employees.OrderBy(x => x.Seniority).Skip((pn - 1) * ps).Take(ps);
            List<EmployeeModel> models = ObjectConverter<Employee, EmployeeModel>.ConvertList(employees.ToList()).ToList();
            return models;
        }

        public async Task<EmployeeModel> SaveEmployee(int id, EmployeeModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer's data missing!");
            }

            bool isExist = await employeeRepository.ExistsAsync(x => (x.PNo == model.PNo) && x.EmployeeId != model.EmployeeId);

            if (isExist)
            {
                throw new InfinityInvalidDataException("Officer's Personal No. data already exist !");
            }


            Employee employee = ObjectConverter<EmployeeModel, Employee>.Convert(model);

            if (id > 0)
            {
                employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == id);
                if (employee == null)
                {
                    throw new InfinityNotFoundException("Officer not found!");
                }
                EmployeeStatus employeeStatus = await employeeStatusRepository.FindOneAsync(x => x.EmployeeStatusId == employee.EmployeeStatusId);

                employee.EmployeeStatusId = employeeStatus.GCode;
                employee.SLCode = employeeStatus.SLCode;
                employee.ModifiedDate = DateTime.Now;
                employee.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                employee.CreatedBy = model.CreatedBy;
                employee.CreatedDate = DateTime.Now;
                employee.Active = true;
                employee.EmployeeStatusId = (int)OfficerCurrentStatus.Active;
                employee.SLCode = (int)OfficerCurrentStatus.Active;
            }
            employee.PNo = model.PNo;
            employee.BnNo = model.BnNo;
            employee.Name = model.Name;
            employee.FullNameEng = model.Name;
            employee.FullNameBan = model.FullNameBan;
            employee.BatchId = model.BatchId;
            employee.BatchPosition = model.BatchPosition;
            employee.GenderId = model.GenderId;
            employee.RankCategoryId = model.RankCategoryId;
            employee.OfficerTypeId = model.OfficerTypeId;
            employee.CountryId = model.CountryId;
            employee.RankId = model.RankId;
            employee.Notification = model.Notification;
            employee.ExecutionRemarkId = model.ExecutionRemarkId;
            employee.BExecutionDate = model.BExecutionDate;
            employee.BSpRemarks = model.BSpRemarks;




        await employeeRepository.SaveAsync(employee);
            model.EmployeeId = employee.EmployeeId;

            await processRepository.UpdateNamingConvention(employee.EmployeeId);
            return model;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new InfinityArgumentMissingException("Invalid Request");
            }
            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == id);
            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }
            else
            {
                return await employeeRepository.DeleteAsync(employee);
            }
        }

        public async Task<EmployeeModel> GetEmployee(int id)
        {
            if (id <= 0)
            {
                return new EmployeeModel();
            }
            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == id, new List<string> { "Batch", "Gender", "Rank" });

            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }
            EmployeeModel model = ObjectConverter<Employee, EmployeeModel>.Convert(employee);
            return model;
        }


        public async Task<EmployeeModel> GetEmployeeByPO(string pno)
        {
            if (String.IsNullOrEmpty(pno))
            {
                return new EmployeeModel();
            }
            Employee employee = db.Database.SqlQuery<Employee>("Select * from Employee where PNo='" + pno + "'").SingleOrDefault();
            //Employee employee = await employeeRepository.FindOneAsync(x => x.PNo == pno, new List<string> {  "Rank", "Batch", "EmployeeGeneral", "ExecutionRemark" });

            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }
            EmployeeModel model = new EmployeeModel()
            {
                EmployeeId = employee.EmployeeId,
                RankCategoryId = employee.RankCategoryId,
                OfficerTypeId = employee.OfficerTypeId,
                CountryId = employee.CountryId,
                ReferenceId = employee.ReferenceId,
                PNo = employee.PNo,
                BnNo = employee.BnNo,
                FullNameEng = employee.FullNameEng,
                Name = employee.Name,
                FullNameBan = employee.FullNameBan,
                BatchId = employee.BatchId,
                BatchPosition = employee.BatchPosition,
                GenderId = employee.GenderId,
                RankId = employee.RankId,
                EmployeeStatusId = employee.EmployeeStatusId,
                SLCode = employee.SLCode
            };
            return ObjectConverter<Employee, EmployeeModel>.Convert(employee);

        }
        public async Task<EmployeeModel> GetEmployeeByPO()
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string pno = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id='" + UserId + "'").SingleOrDefault();

            if (String.IsNullOrEmpty(pno))
            {
                return new EmployeeModel();
            }
            Employee employee = db.Database.SqlQuery<Employee>("Select * from Employee where PNo='"+ pno + "'").SingleOrDefault();
            //Employee employee = await employeeRepository.FindOneAsync(x => x.PNo == pno, new List<string> { "Rank", "Batch", "EmployeeGeneral", "ExecutionRemark" });

            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }
            //EmployeeModel model= ObjectConverter<Employee, EmployeeModel>.Convert(employee);
            EmployeeModel model = new EmployeeModel()
            {
                EmployeeId = employee.EmployeeId,
                RankCategoryId = employee.RankCategoryId,
                OfficerTypeId = employee.OfficerTypeId,
                CountryId = employee.CountryId,
                ReferenceId = employee.ReferenceId,
                PNo = employee.PNo,
                BnNo = employee.BnNo,
                FullNameEng = employee.FullNameEng,
                Name = employee.Name,
                FullNameBan = employee.FullNameBan,
                BatchId = employee.BatchId,
                BatchPosition = employee.BatchPosition,
                GenderId = employee.GenderId,
                RankId = employee.RankId,
                EmployeeStatusId = employee.EmployeeStatusId,
                SLCode = employee.SLCode
            };
            model.BranchName= db.Database.SqlQuery<string>("select Name from Branch where BranchId=(select BranchId from EmployeeGeneral where EmployeeId=(select EmployeeId from Employee where PNo='" + pno + "'))").SingleOrDefault();
            return model;

        }
        public async Task<List<SelectModel>> GetOfficerTypeSelectModels()
        {
            ICollection<OfficerType> models = await officerTypeRepository.FilterAsync(x => x.OfficerTypeId > 0);
            return models.Select(x => new SelectModel()
            {
                Text = x.Name,
                Value = x.OfficerTypeId
            }).ToList();

        }

        public async Task<List<SelectModel>> GetEmployeeStatusSelectModels()
        {
            ICollection<EmployeeStatus> models = await employeeStatusRepository.FilterAsync(x => x.EmployeeStatusId > 0);
            return models.Select(x => new SelectModel()
            {
                Text = x.StatusTitle,
                Value = x.EmployeeStatusId
            }).ToList();

        }

        public List<EmployeeModel> GetEmployeesByDollarSign(int ps, int pn, string qs, out int total)
        {
            IQueryable<Employee> employees = employeeRepository.FilterWithInclude(x => x.Active && x.HasDollarSign && (x.PNo.Contains(qs) || String.IsNullOrEmpty(qs)), "Batch", "Gender", "Rank");
            total = employees.Count();
            employees = employees.OrderBy(x => x.PNo).Skip((pn - 1) * ps).Take(ps);
            List<EmployeeModel> models = ObjectConverter<Employee, EmployeeModel>.ConvertList(employees.ToList()).ToList();
            return models;
        }

        public async Task<EmployeeModel> UpdateEmployeeDollarSign(EmployeeModel model)
        {
            bool isExist = await employeeRepository.ExistsAsync(x => x.Active && x.EmployeeId == model.EmployeeId);

            if (!isExist)
            {
                throw new InfinityInvalidDataException("Invalid Office PNo!");
            }
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer's data missing!");
            }
            Employee employee = ObjectConverter<EmployeeModel, Employee>.Convert(model);
            employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);
            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }

            if (model.DateOfDollarSign == null)
            {
                throw new InfinityNotFoundException("Date is Required.");
            }

            employee.ModifiedDate = DateTime.Now;
            employee.ModifiedBy = model.ModifiedBy;
            employee.HasDollarSign = model.HasDollarSign;
            employee.Reason = model.Reason;
            employee.DateOfDollarSign = model.DateOfDollarSign;
            await employeeRepository.SaveAsync(employee);
            model.EmployeeId = employee.EmployeeId;
            return model;
        }

        public async Task<bool> DeleteEmployeeDollarSign(int employeeId)
        {
            bool isExist = await employeeRepository.ExistsAsync(x => x.Active && x.EmployeeId == employeeId);

            if (!isExist)
            {
                throw new InfinityInvalidDataException("Invalid Office PNo!");
            }

            Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == employeeId);
            if (employee == null)
            {
                throw new InfinityNotFoundException("Officer not found!");
            }
            employee.ModifiedDate = DateTime.Now;
            employee.ModifiedBy = ConfigurationResolver.Get().LoggedInUser.UserId;
            employee.HasDollarSign = false;
            var rows = await employeeRepository.SaveAsync(employee);
            return rows > 0;
        }

        public async Task<bool> ExecuteSeniorityProcess()
        {
            employeeRepository.ExecWithSqlQuery(String.Format("exec spUpdateSeniorityPosition"));
            return true;
        }
        private void GetAgeLimitServiceLimitLprDate(int EmployeeId, int category, int subCategory, int rank, DateTime commissionDate, DateTime dateOfBirth, out DateTime ageLimit, out DateTime serviceLimit, out DateTime LprDate)
        {
            AgeServicePolicy ageServicePolicy = ageServicePolicyRepository.FindOne(x => x.CategoryId == category && x.SubCategoryId == subCategory && x.RankId == rank);
            if (ageServicePolicy == null)
            {
                throw new InfinityNotFoundException("Age Service Policy not Set for EmployeeId=" + EmployeeId.ToString() + "!");
            }
            ageLimit = dateOfBirth.AddYears(ageServicePolicy.AgeLimit);
            serviceLimit = commissionDate.AddYears(ageServicePolicy.ServiceLimit);
            LprDate = ageLimit > serviceLimit ? ageLimit : serviceLimit;

            ageLimit = ageLimit.AddDays(-1);
            serviceLimit = serviceLimit.AddDays(-1);
        }
        //public async Task<string> UpdateAgeServicePolicy()
        //{
        //    int count = 0;

        //    IQueryable<EmployeeGeneral> employeeGenerals = employeeGeneralRepository.FilterWithInclude(x => x.Employee.RankCategoryId == 3, "Employee.RankCategory");
        //    foreach (var item in employeeGenerals.ToList())
        //    {
        //        EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeGeneralId == item.EmployeeGeneralId);

        //        GetAgeLimitServiceLimitLprDate(employeeGeneral.EmployeeId, item.CategoryId, item.SubCategoryId ?? 0, item.Employee.RankId, item.CommissionDate ?? DateTime.Today, item.DoB, out DateTime ageLimit, out DateTime serviceLimit, out DateTime LprDate);
        //        employeeGeneral.AgeLimit = ageLimit;
        //        employeeGeneral.ServiceLimit = serviceLimit;
        //        employeeGeneral.LprDate = LprDate;
        //        await employeeGeneralRepository.SaveAsync(employeeGeneral);
        //        count = count + 1;
        //    }
        //    return "Age Service Policy is updated for " + count.ToString() + " Officers";
        //}



        public bool ExecuteNamingConvention()
        {
            employeeRepository.ExecWithSqlQuery(String.Format("exec [SpBuildNamingConvention] -1"));
            return true;
        }

        public List<object> GetEmployeeByName(string qs)
        {
            DataTable dataTable = employeeRepository.ExecWithSqlQuery(String.Format("exec [spGetByName] '{0}' ", qs));

            return dataTable.ToJson().ToList();
        }
        public string GetLoginEmployeePno()
        {
            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            string pno = db.Database.SqlQuery<string>("select PNo from Administration.AspNetUsers where Id =" +"'"+ userId + "'").SingleOrDefault();

            return pno;
        }

        public Task<string> UpdateAgeServicePolicy()
        {
            throw new NotImplementedException();
        }
    }
}
