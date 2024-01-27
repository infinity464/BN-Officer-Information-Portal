using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
  public  class ColorViewModel
    {
        public ColorModel Color { get; set; }
        public List<SelectModel> ColorTypes { get; set; }

    }
}
