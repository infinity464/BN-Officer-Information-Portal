using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class InstituteViewModel
    {
        public InstituteModel Institute { get; set; }
        public List<SelectModel> BoardTypes { get; set; }
        public List<SelectModel> Boards { get; set; }
    }
}
