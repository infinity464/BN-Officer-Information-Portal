using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class BraCtryCoursePointModel
    {
        public int BraCtryCoursePointId { get; set; }
        public int TraceSettingId { get; set; }
        public int CourseCategoryId { get; set; }
        public int CourseSubCategoryId { get; set; }
        public Nullable<int> RankCategoryId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> Max { get; set; }
        public Nullable<int> Min { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BranchModel Branch { get; set; }
        public virtual CountryModel Country { get; set; }
        public virtual CourseCategoryModel CourseCategory { get; set; }
        public virtual CourseSubCategoryModel CourseSubCategory { get; set; }
        public virtual RankCategoryModel RankCategory { get; set; }
        public virtual TraceSettingModel TraceSetting { get; set; }
    }
}
