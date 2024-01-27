using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class PunishmentAccidentViewModel
    {
        public PunishmentAccidentModel PunishmentAccident { get; set; }
        public List<SelectModel> PunishmentCategories { get; set; }
        public List<SelectModel> PunishmentSubCategories { get; set; }
        public List<SelectModel> PunishmentNatures { get; set; }
        public List<SelectModel> AccidentTypes { get; set; }
        public List<SelectModel> PunishmentAccidentTypes { get; set; }
      
    }
}
