using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class ParentViewModel
    {
        public ParentModel Parent{ get; set; }
        public FileModel File { get; set; }
        public List<SelectModel> Countries { get; set; }
        public List<SelectModel> Nationalities { get; set; }
        public List<SelectModel> Religions { get; set; }
        public List<SelectModel> ReligionCasts { get; set; }
        public List<SelectModel> Occupations { get; set; }
        public List<SelectModel> RankCategories { get; set; }
        
    }
}
