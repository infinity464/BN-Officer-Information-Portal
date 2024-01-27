using System;
using System.Collections.Generic;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
    public class CurrentStatusViewModel
    {
        public dynamic Services { get; set; }
        public Object GrandTotal { get; set; }
        public LeaveSummaryModel LeaveSummaryModel { get;set;}
    }
}