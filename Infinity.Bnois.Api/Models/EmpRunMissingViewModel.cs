using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class EmpRunMissingViewModel
    {
        public EmpRunMissingModel EmpRunMissing { get; set; }
        public EmpRunMissingModel EmpBackToUnit { get; set; }
        public List<SelectModel> StatusTypes { get; set; }
    }
}