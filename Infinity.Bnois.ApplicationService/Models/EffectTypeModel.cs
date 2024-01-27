using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class EffectTypeModel
	{
		public int EffectId { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }
		public Nullable<System.DateTime> CreateDate { get; set; }
		public string CreateId { get; set; }
		public Nullable<bool> IsActive { get; set; }

	}
}
