using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class PoorCourseResultViewModel
    {
        public PoorCourseResultModel PoorCourseResult { get; set; }
        public List<SelectModel> ResultTypes { get; set; }
    }
}
