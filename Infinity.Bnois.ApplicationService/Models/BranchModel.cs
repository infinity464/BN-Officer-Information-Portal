using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class BranchModel
	{
		 public int BranchId { get; set; }
        public string Name { get; set; }
        public string NameBan { get; set; }
        public string ShortNameBan { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Description { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> Active { get; set; }
	}
}
