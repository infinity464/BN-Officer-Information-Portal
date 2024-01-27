using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class MedalAwardViewModel
    {
        public MedalAwardModel MedalAward { get; set; }
        public List<SelectModel> Awards { get; set; }
        public List<SelectModel> Medals { get; set; }
        public List<SelectModel> Publications { get; set; }
        public List<SelectModel> PublicationCategories { get; set; }
        public List<SelectModel> MedalAwardTypes { get; set; }
    }
}
