using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
    public class PreviousLeaveViewModel
    {
        public PreviousLeaveModel PreviousLeave { get; set; }
        public List<SelectModel> LeaveTypes { get; set; }
    }
}
