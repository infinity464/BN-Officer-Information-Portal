using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PreviousMissionModel
    {
        public int PreviousMissionId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}