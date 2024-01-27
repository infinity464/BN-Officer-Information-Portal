using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class RetiredAgeViewModel
    {
        public RetiredAgeModel RetiredAge { get; set; }
        public List<SelectModel> ListTypes { get; set; }
        public List<SelectModel> Categories { get; set; }
        public List<SelectModel> SubCategories { get; set; }
        public List<SelectModel> Ranks { get; set; }
    }
}