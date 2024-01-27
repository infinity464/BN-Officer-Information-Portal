using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
   public class ExtracurricularViewModel
    {
        public ExtracurricularModel Extracurricular { get; set; }
        public List<SelectModel> ExtracurricularTypes { get; set; }
    }
}
