using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Models;

namespace Infinity.Bnois.Api.Models
{
	public class LeavePolicyViewModel
	{
		public LeavePolicyModel LeavePolicy { get; set; }
		public List<SelectModel> CommissionType { get; set; }
		public List<SelectModel> LeaveType { get; set; }
		public List<SelectModel> EffectType { get; set; }
		public List<SelectModel> DurationType { get; set; }
	}
}
