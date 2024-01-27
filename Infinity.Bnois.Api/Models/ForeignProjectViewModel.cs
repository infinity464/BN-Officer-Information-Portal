using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class ForeignProjectViewModel
    {
        public ForeignProjectModel ForeignProject { get; set; }
        public List<SelectModel> Countries { get; set; }

        
    }
}