using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class TerminationTypeModel
    {
        public int TerminationTypeId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}