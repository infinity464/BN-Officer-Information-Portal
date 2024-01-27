using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class SubCategoryViewModel
    {
        public SubCategoryModel SubCategory { get; set; }
        public List<SelectModel> SubCategories { get; set; }
    }
}
