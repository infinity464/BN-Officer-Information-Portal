using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class HeirNextOfKinInfoModel
    {
        public int HeirNextOfKinInfoId { get; set; }
        public int RelationId { get; set; }
        public int GenderId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public Nullable<int> HeirTypeId { get; set; }
        public string NameEng { get; set; }
        public string NameBan { get; set; }
        public string FileName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Email { get; set; }
        public int HeirKinType { get; set; }
        public string HeirKinTypeName { get; set; }
        public string ContactNumber { get; set; }
        public string PassportNumber { get; set; }
        public string Pradhikar { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual GenderModel Gender { get; set; }
        public virtual HeirTypeModel HeirType { get; set; }
        public virtual OccupationModel Occupation { get; set; }
        public virtual RelationModel Relation { get; set; }
    }
}