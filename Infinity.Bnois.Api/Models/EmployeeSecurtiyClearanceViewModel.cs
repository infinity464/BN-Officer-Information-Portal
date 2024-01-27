using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using System.Collections.Generic;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeSecurityClearanceViewModel
    {
        public EmployeeSecurityClearanceModel EmployeeSecurityClearance { get; set; }
        public List<SelectModel> SecurityClearanceReasons { get; set; }
    }
}