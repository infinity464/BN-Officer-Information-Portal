using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class SpouseViewModel
    {
        public SpouseModel Spouse { get; set; }
        public FileModel File { get; set; }
        public FileModel GenFormFile { get; set; }
        public List<SelectModel> RelationTypes { get; set; }
        public List<SelectModel> CurrentStatus { get; set; }
        public List<SelectModel> Occupations { get; set; }
        public List<SelectModel> RankCategories { get; set; }
    }
}
