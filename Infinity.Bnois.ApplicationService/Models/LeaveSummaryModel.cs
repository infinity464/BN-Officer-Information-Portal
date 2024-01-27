using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class LeaveSummaryModel
    {
        public int PlAvailable { get; set; }
        public int TotalPlAvailable { get; set; }
        public DateTime? LastRlAvailed { get; set; }
        public DateTime? RlDue { get; set; }
        public int CasualLeave { get; set; }
        public int SickLeave { get; set; }
        public int MedicalLeave { get; set; }
        public int TotalFurloughLeave { get; set; }
        public int RecreationLeaveDue { get; set; }
        public int MaternyLeave { get; set; }
        public int TerminalLeave { get; set; }
        public int SurveyLeave { get; set; }
        public int WoundLeave { get; set; }
        public bool IsTrue { get; set; }
    }
}
