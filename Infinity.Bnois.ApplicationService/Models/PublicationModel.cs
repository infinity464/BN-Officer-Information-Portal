using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class PublicationModel
    {
        public int PublicationId { get; set; }
        public int PublicationCategoryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PublicationCategoryModel PublicationCategory { get; set; }
    }
}