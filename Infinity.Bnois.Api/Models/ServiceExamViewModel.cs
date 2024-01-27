using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
   public class ServiceExamViewModel
    {
        public ServiceExamModel ServiceExam { get; set; }
        public List<SelectModel> ServiceExamCategories { get; set; }
        public List<SelectModel> Branches { get; set; }
    }
}
