using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class CourseSubCategoryViewModel
    {
        public CourseSubCategoryModel CourseSubCategory { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
    }
}