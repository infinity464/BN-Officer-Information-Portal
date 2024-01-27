using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class CourseSubCategoryModel
    {
        public int CourseSubCategoryId { get; set; }
        public string Name { get; set; }
        public string NameBan { get; set; }
        public string ShortName { get; set; }
        public string ShortNameBan { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> BnListPriority { get; set; }
        public bool Trace { get; set; }
        public bool ANmCon { get; set; }
        public bool NmRGF { get; set; }
        public bool GoToPromotionBoard { get; set; }
        public string Remarks { get; set; }
        public int CourseCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CourseCategoryModel CourseCategory { get; set; }
    }
}