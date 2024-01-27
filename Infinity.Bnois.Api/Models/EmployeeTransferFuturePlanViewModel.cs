using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class EmployeeTransferFuturePlanViewModel
    {
        public EmployeeTransferFuturePlanModel TransferFuturePlan { get; set; }
        public List<SelectModel> AptNats { get; set; }
        public List<SelectModel> AptCats { get; set; }
        public List<SelectModel> Offices { get; set; }
        public List<SelectModel> Patterns { get; set; }
        public List<SelectModel> Countries { get; set; }
    }
}