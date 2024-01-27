using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
   public class VisitSubCategoryViewModel
    {
        public VisitSubCategoryModel VisitSubCategory { get; set; }
        public List<SelectModel> VisitCategories { get; set; }
    }
}
