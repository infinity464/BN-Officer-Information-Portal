using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class DivisionModel
    {
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

    }
}