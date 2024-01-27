using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
   public class EmployeeChildrenViewModel
    {
        public EmployeeChildrenModel EmployeeChildren { get; set; }
        public FileModel File { get; set; }
        public FileModel GenFormName { get; set; }
        public List<SelectModel> Occupations { get; set; }
        public List<SelectModel> ChildrenTypes { get; set; }
        public List<SelectModel> Spouses { get; set; }
    }
}
