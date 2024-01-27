using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class CertificateModel
    {
        public int CertificateId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}