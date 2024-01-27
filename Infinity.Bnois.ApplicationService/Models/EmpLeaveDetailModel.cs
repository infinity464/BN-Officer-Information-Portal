using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class EmpLeaveDetailModel
    {
        public long EmpLeaveDetailId { get; set; }
        public int EmpLeaveId { get; set; }
        public int ReportingOfficeId { get; set; }
        public Nullable<int> NextReportingOfficeId { get; set; }
        public bool ApprovedStatus { get; set; }
        public string ApprovedPNo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
    }
}
