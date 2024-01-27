using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class SeaServiceViewModel
	{
        public SeaServiceModel SeaService { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> ShipTypes { get; set; }
        
    }
}