using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class SubjectModel
	{
		public int SubjectId { get; set; }
		public string Name { get; set; }
        public bool GoToBnList { get; set; }
        
        public string Remarks { get; set; }
		public System.DateTime Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Modified { get; set; }
		public string ModifiedBy { get; set; }
		public Nullable<bool> Active { get; set; }
	}
}
