using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeServiceExtensionModel
    {
        public int EmpSvrExtId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public System.DateTime RetirementDate { get; set; }
        public int ExtMonth { get; set; }
        public int ExtDays { get; set; }
        public System.DateTime ExtLprDate { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}