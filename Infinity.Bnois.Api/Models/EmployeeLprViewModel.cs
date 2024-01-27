using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.Api.Models
{
	public class EmployeeLprViewModel
	{
		public EmployeeLprModel EmployeeLpr { get; set; }
		public List<SelectModel> TerminationType { get; set; }
		public List<SelectModel> LprType { get; set; }
		public List<SelectModel> RetirementType { get; set; }
		public List<SelectModel> DurationType { get; set; }
	}
}
