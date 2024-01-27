using System;
using Infinity.Bnois.Data;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class CourseModel
	{

		public int CourseId { get; set; }

		public int CourseCategoryId { get; set; }

		public int CourseSubCategoryId { get; set; }
	    public Nullable<int> CountryId { get; set; }

        public string FullName { get; set; }

		public string ShortName { get; set; }

		public string NameInBangla { get; set; }

		public Nullable<int> Priority { get; set; }
	    public bool ANGF { get; set; }

        public string CreatedBy { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public Nullable<System.DateTime> ModifiedDate { get; set; }

		public string ModifiedBy { get; set; }

		public bool IsActive { get; set; }
	    public bool SplQualification { get; set; }

        public virtual Country Country { get; set; }
        public virtual CourseCategoryModel CourseCategory { get; set; }

		public virtual CourseSubCategoryModel CourseSubCategory { get; set; }
	}
}