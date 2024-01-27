using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmployeeCourseFuturePlanModel
    {
        public int EmployeeCoursePlanId { get; set; }
        public int EmployeeId { get; set; }
        public int CourseCategoryId { get; set; }
        public Nullable<int> CourseSubCategoryId { get; set; }
        public Nullable<int> CoureseId { get; set; }
        public bool IsMandatory { get; set; }
        public Nullable<System.DateTime> PlanDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CourseModel Course { get; set; }
        public virtual CourseCategoryModel CourseCategory { get; set; }
        public virtual CourseSubCategoryModel CourseSubCategory { get; set; }
        public virtual EmployeeModel Employee { get; set; }
    }
}