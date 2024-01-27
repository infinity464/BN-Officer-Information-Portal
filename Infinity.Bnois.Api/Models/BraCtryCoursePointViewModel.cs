using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class BraCtryCoursePointViewModel
    {
        public BraCtryCoursePointModel BraCtryCoursePoint { get; set; }
        public List<SelectModel> CourseCategories { get; set; }
        public List<SelectModel> CourseSubCategories { get; set; }
        public List<SelectModel> RankCategories { get; set; }
        public List<SelectModel> Branches { get; set; }
        public List<SelectModel> Countries { get; set; }
    }
}
