using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class ParentModel
    {
        public long ParentId { get; set; }
        public int RelationType { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string FullNameBan { get; set; }
        public string NickName { get; set; }
        public string OtherName { get; set; }
        public string FamilyTitle { get; set; }
        public string EducationalQualification { get; set; }
        public Nullable<System.DateTime> DoB { get; set; }
        public string PlaceOfBirth { get; set; }
        public bool IsBirthOutSide { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string ReasonOfMigration { get; set; }
        public Nullable<System.DateTime> MigrationDate { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public string NID { get; set; }
        public bool IsNationalityChange { get; set; }
        public Nullable<int> PreviousNationalityId { get; set; }
        public Nullable<System.DateTime> PreviousNationalityDate { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public Nullable<int> ReligionCastId { get; set; }
        public bool IsDead { get; set; }
        public Nullable<System.DateTime> DeadDate { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public bool IsDoingService { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public Nullable<bool> IsRetired { get; set; }
        public string ServiceAddress { get; set; }
        public string PreServiceAddress { get; set; }
        public string NationalId { get; set; }
        public string FileName { get; set; }
        public string YearlyIncome { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddressBan { get; set; }
        public string PermanentAddressBan { get; set; }
        public string OtherAddress { get; set; }
        public string OtherAddressBan { get; set; }
        public bool IsArmedForceExp { get; set; }
        public Nullable<int> RankCategoryId { get; set; }
        public string PNo { get; set; }
        public string RankName { get; set; }
        public string PoliticalIdeology { get; set; }
        public string Dependency { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual NationalityModel Nationality { get; set; }
        public virtual NationalityModel Nationality1 { get; set; }
        public virtual ReligionModel Religion { get; set; }
        public virtual ReligionCastModel ReligionCast { get; set; }
        public virtual OccupationModel Occupation { get; set; }
        public virtual RankCategoryModel RankCategory { get; set; }

    }
}
