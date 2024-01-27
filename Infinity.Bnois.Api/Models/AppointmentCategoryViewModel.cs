using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public class AppointmentCategoryViewModel
    {
        public AptCatModel aptCatModel { get; set; }
        public List<AptCatModel> aptCatModels { get; set; }
        public List<SelectModel> AppointmentNatureList { get; set; }

    }
}
