using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeGeneralModel
    {
        public int EmployeeGeneralId { get; set; }
        public int EmployeeId { get; set; }
        public string ShortName { get; set; }
        public string ShortNameBan { get; set; }
        public string NickName { get; set; }
        public string NickNameBan { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public int CommissionTypeId { get; set; }
        public int BranchId { get; set; }
        public Nullable<int> SubBranchId { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> SeniorityDate { get; set; }
        public Nullable<System.DateTime> LieutenantDate { get; set; }
        [Required]
        public Nullable<System.DateTime> DoB { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCerNo { get; set; }
        public Nullable<int> MaritalTypeId { get; set; }
        public Nullable<System.DateTime> MarriageDate { get; set; }
        public Nullable<int> OfficerStreamId { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public Nullable<int> ReligionCastId { get; set; }
        public bool IsBirthOutside { get; set; }
        public Nullable<System.DateTime> MigrationDate { get; set; }
        [Required]
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string MigrationReason { get; set; }
        public Nullable<System.DateTime> CommissionDate { get; set; }
        public bool IsDead { get; set; }
        public Nullable<System.DateTime> DeadDate { get; set; }
        public Nullable<System.DateTime> AgeLimit { get; set; }
        public Nullable<System.DateTime> ServiceLimit { get; set; }
        public Nullable<System.DateTime> LastRLAvailedDate { get; set; }
        public Nullable<System.DateTime> LprDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public bool IsContract { get; set; }
        public string DeadReason { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public bool IsShowLieutenantDate { get; set; }

        public virtual BranchModel Branch { get; set; }
        public virtual CategoryModel Category { get; set; }
        public virtual OfficerStreamModel OfficerStream { get; set; }
        public virtual CommissionTypeModel CommissionType { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual NationalityModel Nationality { get; set; }
        public virtual MaritalTypeModel MaritalType { get; set; }
        public virtual ReligionModel Religion { get; set; }
        public virtual ReligionCastModel ReligionCast { get; set; }
        public virtual SubBranchModel SubBranch { get; set; }
        public virtual SubCategoryModel SubCategory { get; set; }
        public virtual SubjectModel Subject { get; set; }
       
    }
}
