using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class SpouseModel
    {
        public int SpouseId { get; set; }
        public int EmployeeId { get; set; }
        public string BNameEng { get; set; }
        public string BNameBan { get; set; }
        public string ANameEng { get; set; }
        public string ANameBan { get; set; }
        public string NickName { get; set; }
        public string NID { get; set; }
        public Nullable<System.DateTime> MarriageDate { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public string BirthPlace { get; set; }
        public Nullable<int> Age { get; set; }
        public int RelationType { get; set; }
        public int CurrentStatus { get; set; }
        public string FileName { get; set; }
        public string GenFormFileName { get; set; }
        public Nullable<System.DateTime> DeadDate { get; set; }
        public string DeadReason { get; set; }
        public string IdMark { get; set; }
        public string EduQualification { get; set; }
        public string ServiceAddress { get; set; }
        public string Degination { get; set; }
        public bool IsArmedForceExp { get; set; }
        public Nullable<int> RankCategoryId { get; set; }
        public string PNo { get; set; }
        public Nullable<int> RankId { get; set; }
        public string SocialActivity { get; set; }
        public string FatherName { get; set; }
        public string FatherNameBan { get; set; }
        public string FatherPreAddress { get; set; }
        public string FatherPerAddress { get; set; }
        public Nullable<int> FatherOccupationId { get; set; }
        public string FatherOtherInfo { get; set; }
        public Nullable<bool> IsFatherDead { get; set; }
        public string MotherName { get; set; }
        public string MotherNameBan { get; set; }
        public string MotherPreAddress { get; set; }
        public string MotherPerAddress { get; set; }
        public Nullable<int> MotherOccupationId { get; set; }
        public string MotherOtherInfo { get; set; }
        public Nullable<bool> IsMotherDead { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public string RelationTypeName { get; set; }
        public string CurrentStatusName { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual OccupationModel Occupation { get; set; }
        public virtual OccupationModel Occupation1 { get; set; }
        public virtual OccupationModel Occupation2 { get; set; }
        public virtual RankCategoryModel RankCategory { get; set; }
    }
}