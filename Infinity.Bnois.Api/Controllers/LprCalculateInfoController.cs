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
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
	[RoutePrefix(BnoisRoutePrefix.LprCalculateInfo)]
	[EnableCors("*", "*", "*")]
	[ActionAuthorize(Feature = MASTER_SETUP.LPRCALCULATEINFO)]
	public class LprCalculateInfoController : PermissionController
    {
		private readonly ILprCalculateInfoService lprCalculateInfoService;
		private readonly IEmployeeService employeeService;
		public LprCalculateInfoController(ILprCalculateInfoService lprCalculateInfoService, IEmployeeService employeeService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
			this.lprCalculateInfoService = lprCalculateInfoService;
			this.employeeService = employeeService;
		}
		[HttpGet]
		[Route("get-lprCalculateInfoes")]
		public IHttpActionResult GetCategories(int ps, int pn, string qs)
		{
			int total = 0;
			List<LprCalculateInfoModel> lprCalculateInfoes = lprCalculateInfoService.GetLprCalculates(ps, pn, qs, out total);
		    RoleFeature permission = base.GetFeature(MASTER_SETUP.LPRCALCULATEINFO);
            return Ok(new ResponseMessage<List<LprCalculateInfoModel>>()
			{
				Result = lprCalculateInfoes,
				Total = total, Permission=permission
			});
		}


		[HttpGet]
		[Route("get-lprCalculateInfo")]
		public async Task<IHttpActionResult> GetLprCalculate(int id)
		{
			LprCalculateInfoViewModel vm = new LprCalculateInfoViewModel();
			vm.lprCalculate = await lprCalculateInfoService.GetLprCalculate(id);
			//vm.lprCalculate.Employee = await employeeService.GetEmployee(id);
			return Ok(new ResponseMessage<LprCalculateInfoViewModel>
			{
				Result = vm
			});
		}

		[HttpPost]
		[ModelValidation]
		[Route("save-lprCalculateInfo")]
		public async Task<IHttpActionResult> SaveLprCalculate([FromBody] LprCalculateInfoModel model)
		{
			return Ok(new ResponseMessage<LprCalculateInfoModel>
			{
				Result = await lprCalculateInfoService.SaveLprCalculate(0, model)
			});
		}

		[HttpPut]
		[Route("update-lprCalculateInfo/{id}")]
		public async Task<IHttpActionResult> UpdateCategory(int id, [FromBody] LprCalculateInfoModel model)
		{
			return Ok(new ResponseMessage<LprCalculateInfoModel>
			{
				Result = await lprCalculateInfoService.SaveLprCalculate(id, model)
			});
		}

		[HttpDelete]
		[Route("delete-lprCalculateInfo/{id}")]
		public async Task<IHttpActionResult> DeleteCategory(int id)
		{
			return Ok(new ResponseMessage<bool>
			{
				Result = await lprCalculateInfoService.DeleteLprCalculate(id)
			});
		}
	}
}
