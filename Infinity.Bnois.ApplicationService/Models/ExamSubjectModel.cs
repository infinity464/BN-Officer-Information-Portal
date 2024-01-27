using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class ExamSubjectModel
    {
        public int ExamSubjectId { get; set; }
        public int ExaminationId { get; set; }
        public int SubjectId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ExaminationModel Examination { get; set; }
        public virtual SubjectModel Subject { get; set; }
    }
}
