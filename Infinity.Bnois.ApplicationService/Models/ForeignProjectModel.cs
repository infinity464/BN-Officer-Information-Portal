using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ForeignProjectModel
    {
        public int ForeignProjectId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string ProjectName { get; set; }
        public string OrganizationName { get; set; }
        public string AppointmentName { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Purpose { get; set; }
        public string Reference { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}