using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
   public class PunishmentSubCategoryViewModel
    {
        public PunishmentSubCategoryModel PunishmentSubCategory { get; set; }
        public List<SelectModel> PunishmentCategories { get; set; }
    }
}
