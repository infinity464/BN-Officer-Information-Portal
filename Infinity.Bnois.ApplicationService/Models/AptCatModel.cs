using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class AptCatModel
	{
		public int AcatId { get; set; }
		public int ANatId { get; set; }
		public string AcatNm { get; set; }
		public string AcatNmBng { get; set; }
		public string ACatShNm { get; set; }
		public string ACatShNmBng { get; set; }
		public bool GoAcr { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		public bool IsActive { get; set; }

        public virtual AptNatModel AptNat { get; set; }

    }
}
