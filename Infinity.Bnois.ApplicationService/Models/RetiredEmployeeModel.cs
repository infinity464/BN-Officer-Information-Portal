using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class RetiredEmployeeModel
    {
        public int RetiredEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public string TsNo { get; set; }
        public bool IsVisitAbroad { get; set; }
        public bool IsJobAbroad { get; set; }
        public bool IsPensionIssued { get; set; }
        public string BookNo { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public int[] CountryIds { get; set; }
        public int[] CertificateIds { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}