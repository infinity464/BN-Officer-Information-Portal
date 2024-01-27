using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
  public  class MedalViewModel
    {
        public MedalModel Medal { get; set; }
        public List<SelectModel> MedalTypes { get; set; }

    }
}
