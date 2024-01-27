using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class RankViewModel
    {
        public RankModel Rank { get; set; }
        public List<SelectModel> RankCategories { get; set; }
    }
}
