using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
  public  class PromotionBoardViewModel
    {
        public PromotionBoardModel PromotionBoard { get; set; }
        public List<SelectModel> FromConfirmRanks { get; set; }
        public List<SelectModel> ToActingRanks { get; set; }
    }
}
