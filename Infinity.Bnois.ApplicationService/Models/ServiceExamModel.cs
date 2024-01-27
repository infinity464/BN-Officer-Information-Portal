using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ServiceExamModel
    {
        public int ServiceExamId { get; set; }
        public int ServiceExamCategoryId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int BranchId { get; set; }
        public int NOS { get; set; }
        public byte AttTime { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual BranchModel Branch { get; set; }
        public virtual ServiceExamCategoryModel ServiceExamCategory { get; set; }
    }
}