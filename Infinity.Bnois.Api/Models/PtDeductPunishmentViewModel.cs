using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class PtDeductPunishmentViewModel
    {
        public PtDeductPunishmentModel PtDeductPunishment { get; set; }
        public List<SelectModel> PunishmentCategories { get; set; }
        public List<SelectModel> PunishmentSubCategories { get; set; }
        public List<SelectModel> PunishmentNatures { get; set; }
    }
}
