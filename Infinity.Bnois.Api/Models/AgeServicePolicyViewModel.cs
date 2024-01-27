using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class AgeServicePolicyViewModel
    {
        public AgeServicePolicyModel AgeServicePolicy { get; set; }
        public List<SelectModel> EarlyStatus { get; set; }
        public List<SelectModel> Categories { get; set; }
        public List<SelectModel> SubCategories { get; set; }
        public List<SelectModel> Ranks { get; set; }
    }
}