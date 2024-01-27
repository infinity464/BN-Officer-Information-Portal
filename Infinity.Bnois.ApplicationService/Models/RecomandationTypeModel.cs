using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class RecomandationTypeModel
    {
        public int RecomandationTypeId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}