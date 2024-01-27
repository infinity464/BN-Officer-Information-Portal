using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class PreCommissionCourseViewModel
    {
        public PreCommissionCourseModel PreCommissionCourse { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> Medals { get; set; }
    }
}
