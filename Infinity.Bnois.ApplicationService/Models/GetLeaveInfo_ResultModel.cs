using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Models
{
	public class GetLeaveInfo_ResultModel
	{
		public int Value { get; set; }
		public Nullable<int> Duration { get; set; }
		public int LeaveDuration { get; set; }
	}
}
