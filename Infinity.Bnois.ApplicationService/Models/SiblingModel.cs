using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class SiblingModel
    {
        public int SiblingId { get; set; }
        public Nullable<int> OccupationId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string SpouseName { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> Age { get; set; }
        public int SiblingType { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string SiblingTypeName { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual OccupationModel Occupation { get; set; }
    }
}