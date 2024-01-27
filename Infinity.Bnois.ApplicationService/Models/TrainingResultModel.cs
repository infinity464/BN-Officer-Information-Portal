using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class TrainingResultModel
    {
        public int TrainingResultId { get; set; }
        public Nullable<int> CourseCategoryId { get; set; }
        public Nullable<int> CourseSubCategoryId { get; set; }
        public int TrainingPlanId { get; set; }
        public int EmployeeId { get; set; }
        public int ResultTypeId { get; set; }
        [Required]
        public Nullable<System.DateTime> ResultDate { get; set; }
        public Nullable<double> Percentage { get; set; }
        public Nullable<int> Positon { get; set; }
        public Nullable<int> ResultStatus { get; set; }
        public string FileName { get; set; }
        public string ResultSection { get; set; }
        public string Remarks { get; set; }
        public string Unit { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CourseCategoryModel CourseCategory { get; set; }
        public virtual CourseSubCategoryModel CourseSubCategory { get; set; }
        public virtual ResultTypeModel ResultType { get; set; }
        public virtual TrainingPlanModel TrainingPlan { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}