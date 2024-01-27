using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;


namespace Infinity.Bnois.Api.Models
{
    public class UpazilaViewModel
    {
        public UpazilaModel Upazila { get; set; }
        public List<SelectModel> Districts { get; set; }
        public List<SelectModel> Divisions { get; set; }
    }
}