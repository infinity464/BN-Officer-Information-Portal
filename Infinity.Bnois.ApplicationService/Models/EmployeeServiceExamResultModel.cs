using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeServiceExamResultModel
    {
        public int EmployeeServiceExamResultId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public bool IsBackLog { get; set; }
        public Nullable<int> RankId { get; set; }
        public Nullable<int> TransferId { get; set; }
        public int ServiceExamCategoryId { get; set; }
        public int ServiceExamId { get; set; }
        [Required]
        public Nullable<System.DateTime> ExamDate { get; set; }
        public Nullable<int> NumberOfSubject { get; set; }
        public bool PassFailResult { get; set; }
        public Nullable<int> AttTime { get; set; }
        public bool IsExempted { get; set; }
        public Nullable<System.DateTime> ExemptedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual ServiceExamModel ServiceExam { get; set; }
        public virtual ServiceExamCategoryModel ServiceExamCategory { get; set; }
    }
}