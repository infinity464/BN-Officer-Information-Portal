using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.LeaveBreakDown)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LEAVE_BREAK_DOWN)]
	public class EmployeeLeaveBreakDownController : BaseController
	{
		private readonly IEmployeeLeaveService employeeLeaveService;
		private readonly IEmployeeService employeeService;
		private readonly ILprCalculateInfoService lprCalculateInfoService;
		private readonly IEmployeeGeneralService employeeGeneralService;

		public EmployeeLeaveBreakDownController(IEmployeeGeneralService employeeGeneralService, ILprCalculateInfoService lprCalculateInfoService, 
		    IEmployeeLeaveService employeeLeaveService, IEmployeeService employeeService)
		{
			this.employeeLeaveService = employeeLeaveService;
			this.employeeService = employeeService;
			this.lprCalculateInfoService = lprCalculateInfoService;
			this.employeeGeneralService = employeeGeneralService;
		}



		[HttpGet]
		[Route("get-leave-break-downs")]
		public async Task<IHttpActionResult> GetLeaveBreakDowns(string employeeId)
		{

			LeaveBreakDownViewModel vm = new LeaveBreakDownViewModel();
			vm.Employee = await employeeService.GetEmployeeByPO(employeeId);
			vm.LeaveBreakDowns = await employeeLeaveService.GetLeaveBreakDowns(vm.Employee.EmployeeId);
			vm.LprCalculateInfo = await lprCalculateInfoService.GetLprCalculateByEmpId(vm.Employee.EmployeeId);
			vm.EmployeeGeneral = await employeeGeneralService.GetEmployeeGenerals(vm.Employee.EmployeeId);
			return Ok(new ResponseMessage<LeaveBreakDownViewModel>
			{
				Result = vm
			});
		}
	}
}
