using System;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class UsedStoreProcedureModel
    {
        public int UsedStoreProcedureId { get; set; }
        public int UsedReportId { get; set; }
        public string StroreProcedureName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual UsedReportModel UsedReport { get; set; }
    }
}