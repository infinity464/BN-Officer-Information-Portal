using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
  public  class SubCategoryModel
    {
		public int SubCategoryId { get; set; }

	    public int CategoryId { get; set; }

	    public string Name { get; set; }

	    public string Description { get; set; }

	    public string ShortName { get; set; }

	    public bool Prefix { get; set; }

	    public bool Rank { get; set; }

	    public bool Branch { get; set; }

	    public bool SubBranch { get; set; }

	    public bool Course { get; set; }

	    public bool Medal { get; set; }

	    public bool Award { get; set; }

	    public bool Prefix2 { get; set; }

	    public string NmConEx { get; set; }

	    public Nullable<int> Priority { get; set; }

	    public bool BN { get; set; }

	    public bool BNVR { get; set; }

	    public System.DateTime CreatedDate { get; set; }

	    public string CreatedBy { get; set; }

	    public Nullable<System.DateTime> ModifiedDate { get; set; }

	    public string ModifiedBy { get; set; }

	    public bool IsActive { get; set; }

		public virtual CategoryModel Category { get; set; }

    }
}
