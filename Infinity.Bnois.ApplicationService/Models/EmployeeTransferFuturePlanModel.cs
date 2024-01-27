using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeTransferFuturePlanModel
    {
        public int EmployeeTransferFuturePlanId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> AptnatId { get; set; }
        public Nullable<int> AptcatId { get; set; }
        public Nullable<int> PatternId { get; set; }
        public int OfficeId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public bool IsMandatory { get; set; }
        public Nullable<System.DateTime> PlanDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual AptCatModel AptCat { get; set; }
        public virtual AptNatModel AptNat { get; set; }
        public virtual CountryModel Country { get; set; }
        public virtual EmployeeModel Employee { get; set; }
        public virtual OfficeModel Office { get; set; }
        public virtual PatternModel Pattern { get; set; }
    }
}