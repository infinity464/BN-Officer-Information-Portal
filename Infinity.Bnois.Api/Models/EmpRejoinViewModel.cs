using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using System.Collections.Generic;

namespace Infinity.Bnois.Api.Models
{
    public class EmpRejoinViewModel
    {
        public EmpRejoinModel EmpRejoin { get; set; }
        public List<SelectModel> Ranks { get; set; }
    }
}