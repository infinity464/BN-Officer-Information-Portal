using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Models;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.EmployeeLpr)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.EMPLOYEE_LPR)]
	public class EmployeeLprController : PermissionController
    {
		private readonly IEmployeeLprService employeeLprService;
		private readonly ITerminationTypeService terminationTypeService;

		public EmployeeLprController(IEmployeeLprService employeeLprService, ITerminationTypeService terminationTypeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.employeeLprService = employeeLprService;		
			this.terminationTypeService = terminationTypeService;		
		}

		[HttpGet]
		[Route("get-employee-lprs")]
		public IHttpActionResult GetEmployeeLprs(int ps, int pn, string qs)
		{
			int total = 0;
			List<EmployeeLprModel> models = employeeLprService.GetEmployeeLprs(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.EMPLOYEE_LPR);
            return Ok(new ResponseMessage<List<EmployeeLprModel>>()
			{
				Result = models,
				Total = total, Permission=permission
			});
		}

		[HttpGet]
		[Route("get-employee-lpr")]
		public async Task<IHttpActionResult> GetEmployeeLpr(int id)
		{
			EmployeeLprViewModel vm = new EmployeeLprViewModel();
			vm.EmployeeLpr = await employeeLprService.GetEmployeeLpr(id);
			vm.TerminationType = await terminationTypeService.GetTerminationTypeSelectModels();
			vm.RetirementType = await employeeLprService.GetRetirementStatusSelectModels();
			vm.DurationType = await employeeLprService.GetDurationStatusSelectModels();
			return Ok(new ResponseMessage<EmployeeLprViewModel>
			{
				Result = vm
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-employee-lpr")]
		public async Task<IHttpActionResult> SaveEmployeeLpr([FromBody] EmployeeLprModel model)
		{
			return Ok(new ResponseMessage<EmployeeLprModel>
			{
				Result = await employeeLprService.SaveEmployeeLpr(0, model)
			});
		}

		[HttpPut]
		[Route("update-employee-lpr/{id}")]
		public async Task<IHttpActionResult> UpdateEmployeeLpr(int id, [FromBody] EmployeeLprModel model)
		{
			return Ok(new ResponseMessage<EmployeeLprModel>
			{
				Result = await employeeLprService.SaveEmployeeLpr(id, model)

			});
		}

		[HttpDelete]
		[Route("delete-employee-lpr/{id}")]
		public async Task<IHttpActionResult> DeleteAchievement(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await employeeLprService.DeleteEmployeeLpr(id)
			});
		}

	}
}
