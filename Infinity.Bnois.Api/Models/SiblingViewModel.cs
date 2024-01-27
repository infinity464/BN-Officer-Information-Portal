using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class SiblingViewModel
    {
        public SiblingModel Sibling { get; set; }
        public FileModel File { get; set; }
        public List<SelectModel> SiblingTypes { get; set; }
        public List<SelectModel> Occupations { get; set; }
    }
}
