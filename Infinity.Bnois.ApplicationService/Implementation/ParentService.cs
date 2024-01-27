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
    public class ParentService : IParentService
    {
        private readonly IBnoisRepository<Parent> parentRepository;
        public ParentService(IBnoisRepository<Parent> parentRepository)
        {
            this.parentRepository = parentRepository;
        }

        public async Task<ParentModel> Parents(int employeeId, int relationType)
        {
            if (employeeId <= 0)
            {
                return new ParentModel();
            }
            Parent parent = await parentRepository.FindOneAsync(x => x.EmployeeId == employeeId && x.RelationType == relationType, new List<string> { "Country", "Nationality", "Religion", "ReligionCast", "Occupation", "RankCategory" });
            if (parent == null)
            {
                return new ParentModel();
            }
            ParentModel model = ObjectConverter<Parent, ParentModel>.Convert(parent);
            return model;
        }

        public async Task<ParentModel> SaveParents(int employeeId, ParentModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Parents data missing!");
            }

            Parent parent = ObjectConverter<ParentModel, Parent>.Convert(model);

            if (parent.ParentId > 0)
            {
                parent = await parentRepository.FindOneAsync(x => x.EmployeeId == employeeId && x.RelationType == model.RelationType);
                if (parent == null)
                {
                    throw new InfinityNotFoundException("Parents data not found!");
                }
                parent.ModifiedDate = DateTime.Now;
                parent.ModifiedBy = model.ModifiedBy;
            }
            else
            {
                parent.EmployeeId = employeeId;
                parent.CreatedBy = model.CreatedBy;
                parent.CreatedDate = DateTime.Now;
                parent.IsActive = true;
            }

            bool exists;
            exists = Enum.IsDefined(typeof(ParentTypes), model.RelationType);
            if (!exists)
            {
                throw new InfinityNotFoundException("Something went wrong in relationship !");
            }
            parent.RelationType = model.RelationType;
            parent.FullName = model.FullName;

            parent.FullNameBan = model.FullNameBan;
            parent.NickName = model.NickName;
            parent.OtherName = model.OtherName;
            parent.FamilyTitle = model.FamilyTitle;
            parent.PresentAddressBan = model.PresentAddressBan;
            parent.PermanentAddressBan = model.PermanentAddressBan;
            parent.OtherAddress = model.OtherAddress;
            parent.OtherAddressBan = model.OtherAddressBan;
            parent.RankName = model.RankName;
            parent.PoliticalIdeology = model.PoliticalIdeology;
            parent.Dependency = model.Dependency;

            parent.DoB = model.DoB;
            parent.EducationalQualification = model.EducationalQualification;
            parent.PreServiceAddress = model.PreServiceAddress;
            parent.PlaceOfBirth = model.PlaceOfBirth;
            parent.IsBirthOutSide = model.IsBirthOutSide;
            parent.CountryId = model.CountryId;
            parent.ReasonOfMigration = model.ReasonOfMigration;
            parent.MigrationDate = model.MigrationDate;

            parent.NationalityId = model.NationalityId;
            parent.NID = model.NID;

            parent.IsNationalityChange = model.IsNationalityChange;
            parent.PreviousNationalityId = model.PreviousNationalityId;
            parent.PreviousNationalityDate = model.PreviousNationalityDate;
            parent.ReligionId = model.ReligionId;
            parent.ReligionCastId = model.ReligionCastId;
            parent.IsDead = model.IsDead;
            parent.OccupationId = model.OccupationId;
            parent.IsDoingService = model.IsDoingService;
            parent.Department = model.Department;
            parent.Designation = model.Designation;
            parent.IsRetired = model.IsRetired;
            parent.ServiceAddress = model.ServiceAddress;
            parent.YearlyIncome = model.YearlyIncome;
            parent.PresentAddress = model.PresentAddress;
            parent.PermanentAddress = model.PermanentAddress;
            parent.IsArmedForceExp = model.IsArmedForceExp;
            parent.RankCategoryId = model.RankCategoryId;
            parent.PNo = model.PNo;
            parent.DeadDate = model.DeadDate;
            await parentRepository.SaveAsync(parent);
            model.ParentId = parent.ParentId;
            return model;
        }

        public async Task<ParentModel> UpdateParent(ParentModel model)
        {
            if (model == null)
            {
                throw new InfinityArgumentMissingException("Parent data missing");
            }

            string userId = ConfigurationResolver.Get().LoggedInUser.UserId.ToString();
            Parent parent = ObjectConverter<ParentModel, Parent>.Convert(model);

            parent = await parentRepository.FindOneAsync(x => x.EmployeeId == model.EmployeeId && x.RelationType == model.RelationType);
            if (parent == null)
            {
                throw new InfinityNotFoundException("Parent not found !");
            }

            if (model.FileName != null)
            {
                parent.FileName = model.FileName;
            }

            parent.ModifiedDate = DateTime.Now;
            parent.ModifiedBy = userId;
            await parentRepository.SaveAsync(parent);
            model.ParentId = parent.ParentId;
            return model;
        }
    }
}

