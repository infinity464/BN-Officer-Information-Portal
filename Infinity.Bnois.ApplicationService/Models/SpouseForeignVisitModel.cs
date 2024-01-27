using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
    public class SpouseForeignVisitModel
    {
        public int SpouseForeignVisitId { get; set; }
        public int SpouseId { get; set; }
        public int CountryId { get; set; }
        public string Purpose { get; set; }
        public string AccompaniedBy { get; set; }
        public Nullable<System.DateTime> StayFromDate { get; set; }
        public Nullable<System.DateTime> StayToDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual SpouseModel Spouse { get; set; }
    }
}
