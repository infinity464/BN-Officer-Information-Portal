using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeePftViewModel
    {
        public EmployeePftModel EmployeePft { get; set; }
        public List<SelectModel> PftTypes { get; set; }
        public List<SelectModel> PftResults { get; set; }
      
    }
}
