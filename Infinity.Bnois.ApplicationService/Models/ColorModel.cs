using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
   public class ColorModel
    {
	    public int ColorId { get; set; }
	    public string Name { get; set; }
	    public int ColorType { get; set; }
	    public System.DateTime CreatedDate { get; set; }
	    public string CreatedBy { get; set; }
	    public Nullable<System.DateTime> ModifiedDate { get; set; }
	    public string ModifiedBy { get; set; }
	    public bool IsActive { get; set; }
		public string ColorTypeName { get; set; }

    }
}
