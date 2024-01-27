using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.Api.Models
{
    public class OPREntryViewModel
    {
        public EmployeeOprModel EmployeeOpr { get; set; }
        public List<SelectModel> AttachOffices { get; set; }
        public List<SelectModel> BornOffices { get; set; }
        public List<SelectModel> Appointments { get; set; }
        public List<SelectModel> RecommendationTypes { get; set; }
        public List<SelectModel> Suitabilities { get; set; }
        public List<SelectModel> SpecialAptTypes { get; set; }
        public List<SelectModel> Occasions { get; set; }
        public List<SelectModel> Ranks { get; set; }
        public Task<List<OprGradingModel>> OprGradings { get; set; }
    }
}