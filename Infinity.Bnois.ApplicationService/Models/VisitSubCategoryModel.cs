using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class VisitSubCategoryModel
    {
        public int VisitSubCategoryId { get; set; }
        public int VisitCategoryId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual VisitCategoryModel VisitCategory { get; set; }
    }
}