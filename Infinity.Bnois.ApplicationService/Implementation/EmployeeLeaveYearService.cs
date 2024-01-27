using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Data;
using Infinity.Bnois.ExceptionHelper;

namespace Infinity.Bnois.ApplicationService.Implementation
{
	public class EmployeeLeaveYearService : IEmployeeLeaveYearService
	{
		private readonly IBnoisRepository<EmployeeLeaveYear> employeeLeaveYearRepository;
		public EmployeeLeaveYearService(IBnoisRepository<EmployeeLeaveYear> employeeLeaveYearRepository)
		{
			this.employeeLeaveYearRepository = employeeLeaveYearRepository;
		}
		
	}
}
