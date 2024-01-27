using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ServiceExamCategoryModel
    {
        public int ServiceExamCategoryId { get; set; }
        public string ExamName { get; set; }
        public string ShortName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}