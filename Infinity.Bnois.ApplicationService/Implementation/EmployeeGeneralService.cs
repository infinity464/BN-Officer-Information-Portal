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
    public class userData
    {
        public string UserID { get; set; }
        public string PNo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class EmployeeGeneralService : IEmployeeGeneralService
    {
        private readonly IBnoisRepository<EmployeeGeneral> employeeGeneralRepository;
        private readonly IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository;
        private readonly IBnoisRepository<Employee> employeeRepository;
        private readonly IProcessRepository processRepository;
        private readonly IBnoisRepository<SearchColumn> searchColumnRepository;
        BnoisDbContext db = new BnoisDbContext();

        public EmployeeGeneralService(IBnoisRepository<EmployeeGeneral> employeeGeneralRepository,
            IBnoisRepository<AgeServicePolicy> ageServicePolicyRepository, IProcessRepository processRepository,
            IBnoisRepository<Employee> employeeRepository, IBnoisRepository<SearchColumn> searchColumnRepository)
        {
            this.employeeGeneralRepository = employeeGeneralRepository;
            this.ageServicePolicyRepository = ageServicePolicyRepository;
            this.employeeRepository = employeeRepository;
            this.processRepository = processRepository;
            this.searchColumnRepository = searchColumnRepository;
        }

        public async Task<EmployeeGeneralModel> GetEmployeeGenerals(int employeeId)
        {
            if (employeeId <= 0)

            {
                return new EmployeeGeneralModel();
            }
            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employeeId, new List<string> { "Employee.Rank", "Category", "SubCategory", "CommissionType", "Branch", "SubBranch", "Nationality", "MaritalType", "Religion", "ReligionCast", "Subject", "OfficerStream", "Employee.OfficerType" });
            if (employeeGeneral == null)
            {
                return new EmployeeGeneralModel();
            }
            EmployeeGeneralModel model = ObjectConverter<EmployeeGeneral, EmployeeGeneralModel>.Convert(employeeGeneral);
            return model;
        }


        private void GetAgeLimitServiceLimitLprDate(int category, int? subCategory, int rank, DateTime commissionDate, DateTime dateOfBirth, string OfficerType, out DateTime ageLimit, out DateTime serviceLimit, out DateTime lprDate, out bool result)
        {
            AgeServicePolicy ageServicePolicy = ageServicePolicyRepository.FindOne(x => x.CategoryId == category && x.SubCategoryId == subCategory && x.RankId == rank);

            ageLimit = DateTime.Now;
            serviceLimit = DateTime.Now;
            lprDate = DateTime.Now;
            result = false;
            if (OfficerType.Equals("BN") && ageServicePolicy == null)
            {
                throw new InfinityNotFoundException("Age Service Policy not Set!");
            }

            if (ageServicePolicy != null)
            {
                ageLimit = dateOfBirth.AddYears(ageServicePolicy.AgeLimit);
                serviceLimit = commissionDate.AddYears(ageServicePolicy.ServiceLimit);
                lprDate = ageLimit > serviceLimit ? ageLimit : serviceLimit;

                ageLimit = ageLimit.AddDays(-1);
                serviceLimit = serviceLimit.AddDays(-1);
                result = true;
            }

        }


        //public async Task<EmployeeGeneralModel> SaveEmployeeGeneral(int employeeId, EmployeeGeneralModel model)
        //{
        //    if (model == null)
        //    {
        //        throw new InfinityArgumentMissingException("General Information data missing!");
        //    }

        //    Employee employeeInfo = await employeeRepository.FindOneAsync(x => x.EmployeeId == employeeId,new List<string>{"OfficerType"});
        //    EmployeeGeneral employeeGeneral = ObjectConverter<EmployeeGeneralModel, EmployeeGeneral>.Convert(model);


        //    if (employeeGeneral.EmployeeGeneralId > 0)
        //    {
        //        employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employeeId);
        //        if (employeeGeneral == null)
        //        {
        //            throw new InfinityNotFoundException("Officer's General Information not found!");
        //        }
        //        employeeGeneral.ModifiedDate = DateTime.Now;
        //        employeeGeneral.ModifiedBy = model.ModifiedBy;
        //    }
        //    else
        //    {
        //        employeeGeneral.EmployeeId = employeeId;
        //        employeeGeneral.CreatedBy = model.CreatedBy;
        //        employeeGeneral.CreatedDate = DateTime.Now;
        //        employeeGeneral.IsActive = true;
        //    }


        //    GetAgeLimitServiceLimitLprDate(model.CategoryId, model.SubCategoryId, employeeInfo.RankId, model.CommissionDate ?? new DateTime(),Convert.ToDateTime(model.DoB), employeeInfo.OfficerType.ShortName, out DateTime ageLimit, out DateTime serviceLimit, out DateTime LprDate, out bool result);
        //    if (result)
        //    {
        //        employeeGeneral.AgeLimit = ageLimit;
        //        employeeGeneral.ServiceLimit = serviceLimit;
        //        employeeGeneral.LprDate = LprDate;
        //    }
        //    else
        //    {
        //        employeeGeneral.AgeLimit = model.AgeLimit;
        //        employeeGeneral.ServiceLimit = model.ServiceLimit;
        //        employeeGeneral.LprDate = model.LprDate;
        //    }
        //    employeeGeneral.ShortName = model.ShortName;
        //    employeeGeneral.ShortNameBan = model.ShortNameBan;
        //    employeeGeneral.NickName = model.NickName;
        //    employeeGeneral.NickNameBan = model.NickNameBan;
        //    employeeGeneral.EmployeeId = employeeId;
        //    employeeGeneral.CategoryId = model.CategoryId;
        //    employeeGeneral.SubCategoryId = model.SubCategoryId;
        //    employeeGeneral.CommissionTypeId = model.CommissionTypeId;
        //    employeeGeneral.BranchId = model.BranchId;
        //    employeeGeneral.SubBranchId = model.SubBranchId;
        //    employeeGeneral.SubjectId = model.SubjectId;
        //    employeeGeneral.OfficerStreamId = model.OfficerStreamId;
        //    employeeGeneral.SeniorityDate = model.SeniorityDate;
        //    employeeGeneral.DoB = Convert.ToDateTime(model.DoB);
        //    employeeGeneral.BirthPlace = model.BirthPlace;
        //    employeeGeneral.IsBirthOutside = model.IsBirthOutside;
        //    employeeGeneral.MigrationDate = model.MigrationDate;
        //    employeeGeneral.LieutenantDate = model.LieutenantDate;
        //    employeeGeneral.JoiningDate = Convert.ToDateTime(model.JoiningDate);
        //    employeeGeneral.MigrationReason = model.MigrationReason;
        //    employeeGeneral.CommissionDate = model.CommissionDate;
        //    employeeGeneral.NationalityId = model.NationalityId;
        //    employeeGeneral.MaritalTypeId = model.MaritalTypeId;
        //    employeeGeneral.MarriageDate = model.MarriageDate;
        //    employeeGeneral.ReligionId = model.ReligionId;
        //    employeeGeneral.ReligionCastId = model.ReligionCastId;
        //    employeeGeneral.ContactNo = model.ContactNo;
        //    employeeGeneral.EmailAddress = model.EmailAddress;
        //    employeeGeneral.IsDead = model.IsDead;
        //    employeeGeneral.DeadDate = model.DeadDate;
        //    employeeGeneral.DeadReason = model.DeadReason;
        //    employeeGeneral.LastRLAvailedDate = model.LastRLAvailedDate;     

        //    employeeGeneral.IsContract = model.IsContract;
        //    employeeGeneral.ContractEndDate = model.ContractEndDate;
        //    await employeeGeneralRepository.SaveAsync(employeeGeneral);
        //    model.EmployeeGeneralId = employeeGeneral.EmployeeGeneralId;

        //    if (employeeGeneral.IsDead)
        //    {
        //        Employee employee = await employeeRepository.FindOneAsync(x => x.EmployeeId == employeeGeneral.EmployeeId);
        //        if (employee == null)
        //        {
        //            throw new InfinityNotFoundException("Officer data not found !");
        //        }
        //        employee.EmployeeStatusId = (int)OfficerCurrentStatus.Dead;
        //        await employeeRepository.SaveAsync(employee);
        //    }


        //    await processRepository.UpdateNamingConvention(employeeGeneral.EmployeeId);
        //    return model;
        //}

        public async Task<EmployeeGeneralModel> GetEmployeeGeneralByPNo(string pno)
        {
            Employee employee = await employeeRepository.FindOneAsync(x => x.PNo == pno);
            EmployeeGeneral employeeGeneral = await employeeGeneralRepository.FindOneAsync(x => x.EmployeeId == employee.EmployeeId);
            if (employeeGeneral == null)
            {
                throw new InfinityNotFoundException("Officer General Information data not found !");
            }
            EmployeeGeneralModel model = ObjectConverter<EmployeeGeneral, EmployeeGeneralModel>.Convert(employeeGeneral);
            return model;
        }

        public bool UpdateEmployeeEmailContact(string PNo, string EmailAddress, string ContactNo)
        {
            string UserId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            userData user = db.Database.SqlQuery<userData>("select Id,PNo,Email,PhoneNumber from Administration.AspNetUsers where Id='" + UserId + "'").FirstOrDefault();
            if (PNo == null)
            {
                throw new InfinityNotFoundException("Officer Not Found.");
            }
            if (PNo == user.PNo)
            {
                string query = String.Format(@"update Administration.AspNetUsers set Email = '{0}', PhoneNumber = '{1}' where PNo='{2}'", EmailAddress, ContactNo, PNo);
                bool saved = searchColumnRepository.ExecNoneQuery(query) > 0;
                if(saved == true)
                {
                    var logQuery = $"INSERT INTO [dbo].[UserUpdateLog]([PNo],[PreviousEmail],[Email],[PreviousPhoneNumber],[PhoneNumber],[CreatedBy],[CreatedDate],[UserId])VALUES('"+user.PNo+"','"+user.Email+"','"+EmailAddress+"','"+user.PhoneNumber+"','"+ContactNo+"','"+UserId+ "',GETDATE(),'"+user.UserID+"')";
                    int noOfRowUpdated = db.Database.ExecuteSqlCommand(logQuery);
                }
                return saved;
            }
            return false;

            //int employeeId = employeeRepository.Where(x => x.PNo == PNo).Select(x => x.EmployeeId).FirstOrDefault();
            //EmployeeGeneral employeeGeneral= employeeGeneralRepository.FindOne(x => x.EmployeeId == employeeId);
            //employeeGeneral.EmailAddress = EmailAddress;
            //employeeGeneral.ContactNo = ContactNo;
            //employeeGeneral.ModifiedBy= ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            //employeeGeneral.ModifiedDate = DateTime.Now;


            //employeeGeneralRepository.SaveAsync(employeeGeneral);
            //return true;

        }

        public Task<EmployeeGeneralModel> SaveEmployeeGeneral(int v, EmployeeGeneralModel model)
        {
            throw new NotImplementedException();
        }
    }
}
