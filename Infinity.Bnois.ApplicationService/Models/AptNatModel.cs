using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class AptNatModel
	{
		public int ANatId { get; set; }
		public string ANatNm { get; set; }
		public string ANatNmBng { get; set; }
	    public string ANatShnm { get; set; }
        public string ANatShnmBng { get; set; }
		public bool AptFr { get; set; }
        public bool IsStaffDuty { get; set; }
        public System.DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		public bool IsActive { get; set; }
	}
}
