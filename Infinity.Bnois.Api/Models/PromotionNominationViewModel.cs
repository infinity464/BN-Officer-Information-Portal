using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class PromotionNominationViewModel
    {
        public PromotionNominationModel PromotionNomination { get; set; }
        public List<PromotionNominationModel> PromotionNominations { get; set; }
        public List<SelectModel> ExecutionRemarks { get; set; }
        public List<SelectModel> PromotionBoards { get; set; }
        public List<SelectModel> Ranks { get; set; }
    }
}
