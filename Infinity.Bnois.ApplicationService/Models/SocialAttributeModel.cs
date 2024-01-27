using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class SocialAttributeModel
    {
        public int SocialAttributeId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsSocialAttribute { get; set; }
        public string SARemarks { get; set; }
        public bool IsCirculationValue { get; set; }
        public string CVRemarks { get; set; }
        public string Hobby { get; set; }
        public bool IsPersonalityPerChar { get; set; }
        public string PPCRemarks { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EmployeeModel Employee { get; set; }
    }
}
