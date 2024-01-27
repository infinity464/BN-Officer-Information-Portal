using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class UsedReportModel
    {
        public int UsedReportId { get; set; }
        public string ReportName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}