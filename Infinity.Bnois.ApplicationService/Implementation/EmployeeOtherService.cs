using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Implementation
{
   public class EmployeeOtherService: IEmployeeOtherService
    {
        private readonly IBnoisRepository<EmployeeOther> employeeOtherRepository;
        public EmployeeOtherService(IBnoisRepository<EmployeeOther> employeeOtherRepository)
        {
            this.employeeOtherRepository = employeeOtherRepository;
        }

        public async Task<EmployeeOtherModel> GetEmployeeOthers(int employeeId)
        {
            if (employeeId <= 0)
            {
                return new EmployeeOtherModel();
            }
            EmployeeOther employeeOther = await employeeOtherRepository.FindOneAsync(x => x.EmployeeId == employeeId, new List<string> { "Employee"});

            if (employeeOther == null)
            {
                return new EmployeeOtherModel();
            }
            EmployeeOtherModel model = ObjectConverter<EmployeeOther, EmployeeOtherModel>.Convert(employeeOther);
            return model; ;
        }

        public async Task<EmployeeOtherModel> SaveEmployeeOther(int employeeId, EmployeeOtherModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Officer's Other information data missing!");
            }

            EmployeeOther employeeOther = ObjectConverter<EmployeeOtherModel, EmployeeOther>.Convert(model);

            if (employeeOther.EmployeeOtherId > 0)
            {
                employeeOther = await employeeOtherRepository.FindOneAsync(x => x.EmployeeId == employeeId);
                if (employeeOther == null)
                {
                    throw new InfinityNotFoundException("Officer's Other information not found!");
                }
                employeeOther.ModifiedDate = DateTime.Now;
                employeeOther.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                employeeOther.EmployeeId = employeeId;
                employeeOther.CreatedBy = model.CreatedBy;
                employeeOther.CreatedDate = DateTime.Now;
                employeeOther.IsActive = true;
            }
            employeeOther.NationalId = model.NationalId;
            employeeOther.IdIssueDate = model.IdIssueDate;
            employeeOther.ServiceId = model.ServiceId;
            employeeOther.SerIssueDate = model.SerIssueDate;
            employeeOther.EmployeeId = employeeId;
            employeeOther.PassportNo = model.PassportNo;
            employeeOther.PassIssueDate = model.PassIssueDate;
            employeeOther.ExpiryDate = model.ExpiryDate;
            employeeOther.OldPassportNo = model.OldPassportNo;
            employeeOther.PassIssuePlace = model.PassIssuePlace;
            employeeOther.HasDrivingLicense= model.HasDrivingLicense;
            employeeOther.DrivingLicenseNo = model.DrivingLicenseNo;
            employeeOther.DLIssueDate = model.DLIssueDate;
            employeeOther.DLExpiryDate = model.DLExpiryDate;
            employeeOther.BirthCertificateNo = model.BirthCertificateNo;
            
            employeeOther.IsFreedomFighter = model.IsFreedomFighter;
            employeeOther.CertificateNo = model.CertificateNo;
            employeeOther.SectorNo = model.SectorNo; 
            await employeeOtherRepository.SaveAsync(employeeOther);
            model.EmployeeOtherId = employeeOther.EmployeeOtherId;
            return model;
        }

        public async Task<EmployeeOtherModel> UpdateEmployeeOther(EmployeeOtherModel model)
        {
            string userId = Configuration.ConfigurationResolver.Get().LoggedInUser.UserId;
            EmployeeOther employeeOther = ObjectConverter<EmployeeOtherModel, EmployeeOther>.Convert(model);

            employeeOther = await employeeOtherRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId);

            if (employeeOther == null)
            {
                throw new InfinityNotFoundException("Employee Other not found !");
            }
            if (model.NIdFileName != null)
            {
                employeeOther.NIdFileName = model.NIdFileName;
            }
            if (model.DLFileName != null)
            {
                employeeOther.DLFileName = model.DLFileName;
            }

            if (model.PassportFIleName != null)
            {
                employeeOther.PassportFIleName = model.PassportFIleName;
            }

            employeeOther.ModifiedDate = DateTime.Now;
            employeeOther.ModifiedBy = userId;
            await employeeOtherRepository.SaveAsync(employeeOther);
            model = ObjectConverter<EmployeeOther, EmployeeOtherModel>.Convert(employeeOther);
            return model;
        }
    }
}
