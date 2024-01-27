using System;
using System.ComponentModel.DataAnnotations;
using Infinity.Bnois.Api.Models;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class TrainingPlanModel
    {
        public int TrainingPlanId { get; set; }
        public int CourseCategoryId { get; set; }
        public int CourseSubCategoryId { get; set; }
        public int CourseId { get; set; }
        public string CourseNo { get; set; }
        public int CountryType { get; set; }
        public int CountryId { get; set; }
        public int InstituteId { get; set; }
        [Required]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> MaxParticipant { get; set; }

        public bool IsPostponed { get; set; }
        public Nullable<System.DateTime> PDate { get; set; }
        public Nullable<System.DateTime> PToDate { get; set; }
        public string Purpose { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string IsClosed { get; set; }
        public bool IsActive { get; set; }
        public int[] RankIds { get; set; }
        public int[] BranchIds { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual CourseModel Course { get; set; }
        public virtual CourseCategoryModel CourseCategory { get; set; }
        public virtual CourseSubCategoryModel CourseSubCategory { get; set; }
        public virtual TrainingInstituteModel TrainingInstitute { get; set; }
      

    }
}