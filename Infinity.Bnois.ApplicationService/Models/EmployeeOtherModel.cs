using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class EmployeeOtherModel
    {
        public int EmployeeOtherId { get; set; }
        public int EmployeeId { get; set; }
        public string NationalId { get; set; }
        public Nullable<System.DateTime> IdIssueDate { get; set; }
        public string ServiceId { get; set; }
        public Nullable<System.DateTime> SerIssueDate { get; set; }
        public string PassportNo { get; set; }
        public string OldPassportNo { get; set; }
        public Nullable<System.DateTime> PassIssueDate { get; set; }
        public string PassIssuePlace { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string PassportFIleName { get; set; }

        public bool HasDrivingLicense { get; set; }
        public string DrivingLicenseNo { get; set; }
        public Nullable<System.DateTime> DLIssueDate { get; set; }
        public Nullable<System.DateTime> DLExpiryDate { get; set; }
        public string BirthCertificateNo { get; set; }
        public bool IsFreedomFighter { get; set; }
        public string CertificateNo { get; set; }
        public string SectorNo { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string NIdFileName { get; set; }
        public string DLFileName { get; set; }

        public string NationalIdImageUrl { get; set; }
        public string DrivingLicenseImageUrl { get; set; }
        public string PassportImageUrl { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}
