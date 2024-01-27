using System;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class PreCommissionCourseModel
    {
        public long PreCommissionCourseId { get; set; }
        public int EmployeeId { get; set; }
        public string BnaNo { get; set; }
        public bool IsAbroad { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> MedalId { get; set; }
        public string Punishment { get; set; }
        public string AppointmentHeld { get; set; }
        public Nullable<double> ModuleD { get; set; }
        public Nullable<double> Total { get; set; }
        public string FinalPosition { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual MedalModel Medal { get; set; }
    }
}
